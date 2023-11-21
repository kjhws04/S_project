using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using System;

/*
    [가중치 랜덤 뽑기]

    - 제네릭을 통해 아이템의 타입을 지정해 객체화하여 사용한다.
    - 중복되는 아이템이 없도록 딕셔너리로 구현하였다.
    - 가중치가 0보다 작은 경우 예외를 호출한다.

    - double SumOfWeights : 전체 아이템의 가중치 합(읽기 전용 프로퍼티)

    - void Add(T, double) : 새로운 아이템-가중치 쌍을 추가한다.
    - void Add(params (T, double)[]) : 새로운 아이템-가중치 쌍을 여러 개 추가한다.
    - void Remove(T) : 대상 아이템을 목록에서 제거한다.
    - void ModifyWeight(T, double) : 대상 아이템의 가중치를 변경한다.
    - void ReSeed(int) : 랜덤 시드를 재설정한다.

    - T GetRandomPick() : 현재 아이템 목록에서 가중치를 계산하여 랜덤으로 항목 하나를 뽑아온다.
    - T GetRandomPick(double) : 이미 계산된 확률 값을 매개변수로 넣어, 해당되는 항목 하나를 뽑아온다.
    - double GetWeight(T) : 대상 아이템의 가중치를 얻어온다.
    - double GetNormalizedWeight(T) : 대상 아이템의 정규화된 가중치를 얻어온다.

    - ReadonlyDictionary<T, double> GetItemDictReadonly() : 전체 아이템 목록을 읽기전용 컬렉션으로 받아온다.
    - ReadonlyDictionary<T, double> GetNormalizedItemDictReadonly()
      : 전체 아이템의 가중치 총합이 1이 되도록 정규화된 아이템 목록을 읽기전용 컬렉션으로 받아온다.
*/
// <summary>
// 가충치 랜덤 뽑기
// </summary>

public class Gacha<T>
{
    // 전체 아이템의 가중치 합 (읽기 전용)
    public double SumOfWeights
    {
        get
        {
            CalculateSumIfDirty();
            return _sumOfWeights;
        }
    }

    private System.Random randomInstance;
    private readonly Dictionary<T, double> itemWeightDict;
    //확률이 정규화된 아이템 목록
    private readonly Dictionary<T, double> normalizedItemWeightDict;
    private readonly Dictionary<T, bool> isChar;

    //가중치 합이 계산되지 않은 상태인지 여부
    private bool isDirty;
    private double _sumOfWeights;

    // [[생성자]]
    // <summary>
    // Gacha 클래스의 매개변수가 없는 생성자
    // 난수 생성기를 초기화하고, 아이템과 가중치를 저장할 딕셔너리들을 초기
    // 초기 상태에서는 가중치의 합이 계산되지 않은 상태(isDirty = true)
    // </summary>
    public Gacha()
    {
        randomInstance = new System.Random();
        itemWeightDict = new Dictionary<T, double>();  // 아이템과 가중치를 저장할 딕셔너리 초기화
        normalizedItemWeightDict = new Dictionary<T, double>(); // 정규화된 가중치를 저장할 딕셔너리 초기화
        isChar = new Dictionary<T, bool>();  // 각 아이템이 캐릭터인지 여부를 저장할 딕셔너리 초기화 (true = char, flase = weapon)
        isDirty = true; // 가중치 합 계산 필요
        _sumOfWeights = 0.0; // 가중치의 합
    }

    // <summary>
    // Gacha 클래스의 랜덤 시드를 받아와 초기화
    // 매개변수로 받은 랜덤 시드를 사용하여 난수 생성기를 초기화하고, 아이템과 가중치를 저장할 딕셔너리들을 초기화
    // 초기 상태에서는 가중치의 합이 계산되지 않은 상태(isDirty = true)
    // </summary>
    public Gacha(int randomSeed)
    {
        randomInstance = new System.Random(randomSeed);
        itemWeightDict = new Dictionary<T, double>();  // 아이템과 가중치를 저장할 딕셔너리 초기화
        normalizedItemWeightDict = new Dictionary<T, double>(); // 정규화된 가중치를 저장할 딕셔너리 초기화
        isChar = new Dictionary<T, bool>(); // 각 아이템이 캐릭터인지 여부를 저장할 딕셔너리 초기화 (true = char, flase = weapon)
        isDirty = true;
        _sumOfWeights = 0.0; // 가중치의 합
    }

    // <summary>
    // 아이템과 가중치를 추가하는 메서드 Gacha를 생성하고, Add로 추가해서 사용
    // 새로운 아이템과 해당 아이템의 가중치를 받아 딕셔너리에 추가
    // 추가 후에는 가중치의 합이 다시 계산되어야 함 (isDirty)
    // </summary>
    public void Add(T item, double weight, bool _isChar)
    {
        CheckDuplicatedItem(item); // 중복된 아이템이 이미 존재하는지 확인
        CheckValidWeight(weight); // 가중치가 유효한지 확인

        itemWeightDict.Add(item, weight); // 아이템과 가중치를 딕셔너리에 추가
        isChar.Add(item, _isChar); // 아이템이 캐릭터인지 여부를 딕셔너리에 추가
        isDirty = true;
    }

    // <summary>
    // 여러 개의 아이템과 가중치 쌍을 한 번에 추가하는 메서드
    // 각 아이템과 가중치 쌍을 받아와서 반복문을 통해 딕셔너리에 추가
    // 추가 후에는 가중치의 합이 다시 계산되어야 함 (isDirty)
    // </summary>
    public void Add(params (T item, double weight, bool _isChar)[] pairs)
    {
        foreach (var pair in pairs)
        {
            CheckDuplicatedItem(pair.item); // 중복된 아이템이 이미 존재하는지 확인
            CheckValidWeight(pair.weight); // 가중치가 유효한지 확인

            itemWeightDict.Add(pair.item, pair.weight); // 아이템과 가중치를 딕셔너리에 추가
            isChar.Add(pair.item, pair._isChar); // 아이템이 캐릭터인지 여부를 딕셔너리에 추가
        }
        isDirty = true; //가중치 계산 필요
    }

    // [[공용 함수]]
    // <summary>
    // 딕셔너리에서 특정 아이템을 제거하는 메서드
    // 제거 후에는 가중치의 합이 다시 계산되어야 함
    // </summary>
    public void Remove(T item)
    {
        CheckNotExitedItem(item); // 제거하려는 아이템이 목록에 존재하지 않으면 예외 발생

        itemWeightDict.Remove(item); // 딕셔너리에서 아이템 제거
        isDirty = true; // 가중치의 합이 다시 계산되어야 함
    }

    // <summary>
    // 특정 아이템의 가중치를 수정하는 메서드
    // 수정 후에는 가중치의 합이 다시 계산되어야 함
    // </summary>
    public void ModifyWeight(T item, double weight)
    {
        CheckNotExitedItem(item); // 가중치를 수정하려는 아이템이 목록에 존재하지 않으면 예외 발생
        CheckValidWeight(weight); // 수정된 가중치가 유효한지 확인

        itemWeightDict[item] = weight; // 딕셔너리에서 해당 아이템의 가중치 수정
        isDirty = true; // 가중치의 합이 다시 계산되어야 함을 표시
    }

    // <summary>
    // 특정 아이템이 캐릭터인지 여부를 반환하는 메서드
    // </summary>
    public bool CheckIsChar(T item)
    {
        return isChar[item]; // 해당 아이템이 캐릭터인지 여부 반환
    }

    // <summary>
    // 랜덤 시드를 재설정하는 메서드
    // </summary>
    public void ReSeed(int seed)
    {
        randomInstance = new System.Random(seed); // 새로운 랜덤 시드를 사용하여 초기화
    }

    // [[Getter]] 함수
    // <summary>
    // 랜덤으로 아이템을 뽑는 메서드
    // 난수를 생성하여 가중치를 계산하고, 해당 가중치에 맞게 아이템을 반환
    // </summary>
    public T GetRandomPick()
    {
        // 랜덤 계산
        double chance = randomInstance.NextDouble(); // 0.0부터 1.0 사이의 난수 생성
        chance *= SumOfWeights; // 생성된 난수에 전체 가중치를 곱하여 실제 가중치로 변환

        return GetRandomPick(chance); // 변환된 가중치를 사용하여 아이템을 뽑아옴
    }

    // <summary>
    // 주어진 가중치에 따라 아이템을 뽑는 메서드
    // 매개변수로 받은 가중치에 따라 아이템을 계산하고 반환
    // </summary>
    public T GetRandomPick(double randomValue)
    {
        if (randomValue < 0.0) randomValue = 0.0; // 가중치가 음수이면 0으로 설정
        if (randomValue > SumOfWeights) randomValue = SumOfWeights - 0.00000001; // 가중치가 전체 합을 초과하면 최대값으로 설정

        double current = 0.0;
        foreach (var pair in itemWeightDict)
        {
            current += pair.Value;

            if (randomValue < current)
            {
                return pair.Key; // 현재 가중치가 주어진 가중치보다 크면 해당 아이템 반환
            }
        }

        // 여기까지 도달하면 오류 상황으로 간주하고 예외 발생
        throw new Exception($"Unreachable - [Random Value : {randomValue}, Current Value : {current}]");
    }

    // <summary>
    // 특정 아이템(T item)의 가중치를 반환하는 메서드
    // </summary>
    // <returns>해당 아이템의 가중치</returns>
    public double GetWeight(T item)
    {
        return itemWeightDict[item];
    }

    // <summary>
    // 특정 아이템의(T item) 정규화된 가중치를 반환하는 메서드
    // 가중치의 합이 1이 되도록 정규화된 값을 반환
    // </summary>
    public double GetNormalizedWeight(T item)
    {
        CalculateSumIfDirty(); // 가중치의 합이 계산되지 않은 경우 계산
        return normalizedItemWeightDict[item];  // 정규화된 가중치 반환
    }

    // <summary>
    // 현재 아이템 목록을 읽기 전용 딕셔너리로 반환하는 메서드
    // </summary>
    // <returns>아이템과 가중치를 포함한 읽기 전용 딕셔너리</returns>
    public ReadOnlyDictionary<T, double> GetItemDictReadonly()
    {
        return new ReadOnlyDictionary<T, double>(itemWeightDict); // 읽기 전용 딕셔너리 반환
    }

    // <summary>
    // 가중치의 합이 1이 되도록 정규화된 아이템 목록을 읽기 전용 딕셔너리로 반환하는 메서드
    // </summary>
    // <returns>정규화된 아이템 목록을 포함한 읽기 전용 딕셔너리</returns>
    public ReadOnlyDictionary<T, double> GetNormalizedItemDictReadonly() 
    {
        CalculateSumIfDirty(); // 가중치의 합이 계산되지 않은 경우 계산
        return new ReadOnlyDictionary<T, double>(normalizedItemWeightDict);  // 정규화된 딕셔너리 반환
    }

    //보호 함수
    // <summary>
    // 가중치의 합을 계산하고, 필요한 경우 정규화된 딕셔너리를 업데이트하는 메서드
    // </summary>
    private void CalculateSumIfDirty()
    {
        if (!isDirty) return; // 가중치의 합이 이미 계산된 경우 아무 작업도 수행하지 않음
        isDirty = false; // 가중치의 합을 r산하고 있는 상태로 표시

        _sumOfWeights = 0.0f; // 가중치의 합 초기화
        foreach (var pair in itemWeightDict)
        {
            _sumOfWeights += pair.Value;  // 각 아이템의 가중치를 더하여 전체 가중치의 합을 계산
        }

        // 정규화된 딕셔너리 업데이트
        UpdateNormalizedDict();
    }

    // <summary>
    // 가중치가 정규화된 딕셔너리를 업데이트하는 메서드입니다.
    // 각 아이템의 가중치를 전체 가중치로 나누어 정규화된 값을 계산하여 딕셔너리에 저장합니다.
    // </summary>
    private void UpdateNormalizedDict()
    {
        normalizedItemWeightDict.Clear(); // 기존의 정규화된 딕셔너리를 비움
        foreach (var pair in itemWeightDict)
        {
            normalizedItemWeightDict.Add(pair.Key, pair.Value / _sumOfWeights);  // 정규화된 값을 딕셔너리에 추가
        }
    }

    // <summary>
    // 중복된 아이템이 이미 존재하는지 확인하는 메서드
    // 만약 중복된 아이템이 존재하면 디버그 로그를 출력
    // </summary>
    private void CheckDuplicatedItem(T item)
    {
        if (itemWeightDict.ContainsKey(item))
            Debug.Log($"이미 {item} 아이템이 존재합니다.");
    }

    // <summary>
    // 목록에 존재하지 않는 아이템을 확인하는 메서드
    // 만약 목록에 존재하지 않는 아이템이라면 디버그 로그를 출력
    // </summary>
    private void CheckNotExitedItem(T item)
    {
        if (!itemWeightDict.ContainsKey(item))
            Debug.Log($"이미 {item} 아이템이 목록에 존재하지 않습니다.");
    }

    // <summary>
    // 가중치 값이 유효한지 확인하는 메서드
    // 만약 가중치 값이 0 이하이면 디버그 로그를 출력
    // </summary>
    private void CheckValidWeight(in double weight)
    {
        if (weight <= 0f)
            Debug.Log("가중치 값은 0보다 커야 합니다.");
    }
}
