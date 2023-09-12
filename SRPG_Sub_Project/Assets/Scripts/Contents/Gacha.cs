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
    //public double itemWeightDict;
    //public int istMore4Stars;

    //public Gacha(double _itemWeigthDict, int _istMore4Stars)
    //{
    //    itemWeightDict = _itemWeigthDict;
    //    istMore4Stars = _istMore4Stars;
    //}

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

    //가중치 합이 계산되지 않은 상태인지 여부
    private bool isDirty;
    private double _sumOfWeights;

    //생성자
    public Gacha()
    {
        randomInstance = new System.Random();
        itemWeightDict = new Dictionary<T, double>();
        normalizedItemWeightDict = new Dictionary<T, double>();
        isDirty = true;
        _sumOfWeights = 0.0;
    }

    public Gacha(int randomSeed)
    {
        randomInstance = new System.Random(randomSeed);
        itemWeightDict = new Dictionary<T, double>();
        normalizedItemWeightDict = new Dictionary<T, double>();
        isDirty = true;
        _sumOfWeights = 0.0;
    }

    //Add 함수
    //새로운 아이템 가중치 쌍 추가
    public void Add(T item, double weight)
    {
        CheckDuplicatedItem(item);
        CheckValidWeight(weight);

        itemWeightDict.Add(item, weight);
        isDirty = true;
    }

    //새로운 아이템 가중치 쌍들 추가
    public void Add(params (T item, double weight)[] pairs)
    {
        foreach (var pair in pairs)
        {
            CheckDuplicatedItem(pair.item);
            CheckValidWeight(pair.weight);

            itemWeightDict.Add(pair.item, pair.weight);
        }
    }

    //공용 함수
    //목록에서 대상 아이템 제거
    public void Remove(T item)
    {
        CheckNotExitedItem(item);

        itemWeightDict.Remove(item);
        isDirty = true;
    }

    // 대상 아이템의 가중치 수정
    public void ModifyWeight(T item, double weight)
    {
        CheckNotExitedItem(item);
        CheckValidWeight(weight);

        itemWeightDict[item] = weight;
        isDirty = true;
    }

    // 랜덤 시드 재설정
    public void ReSeed(int seed)
    {
        randomInstance = new System.Random(seed);
    }

    //Getter 함수
    // 랜덤 뽑기
    public T GetRandomPick()
    {
        // 랜덤 계산
        double chance = randomInstance.NextDouble(); //TODO
        chance *= SumOfWeights;

        return GetRandomPick(chance);
    }

    // 직접 랜덤 값을 지정해 뽑기
    public T GetRandomPick(double randomValue)
    {
        if (randomValue < 0.0) randomValue = 0.0;
        if (randomValue > SumOfWeights) randomValue = SumOfWeights - 0.00000001;

        double current = 0.0;
        foreach (var pair in itemWeightDict)
        {
            current += pair.Value;

            if (randomValue < current)
            {
                return pair.Key;
            }
        }

        throw new Exception($"Unreachable - [Random Value : {randomValue}, Current Value : {current}]");
        //return itemPairList[itemPairList.Count - 1].item; // Last Item
    }

    //대상 아이템의 가중치 확인
    public double GetWeight(T item)
    {
        return itemWeightDict[item];
    }

    // 대상 아이템의 정규화된 가중치 확인
    public double GetNormalizedWeight(T item)
    {
        CalculateSumIfDirty();
        return normalizedItemWeightDict[item];
    }

    // 아이템 목록 확인 (읽기 전용)
    public ReadOnlyDictionary<T, double> GetItemDictReadonly()
    {
        return new ReadOnlyDictionary<T, double>(itemWeightDict);
    }

    // 가중치 합이 1이 되도록 정규화된 아이템 목록 확인 (읽기 전용)
    public ReadOnlyDictionary<T, double> GetNormalizedItemDictReadonly()
    {
        CalculateSumIfDirty();
        return new ReadOnlyDictionary<T, double>(normalizedItemWeightDict);
    }

    //보호 함수
    // 모든 아이템의 가중치 합 계산해 놓기
    private void CalculateSumIfDirty()
    {
        if (!isDirty) return;
        isDirty = false;

        _sumOfWeights = 0.0f;
        foreach (var pair in itemWeightDict)
        {
            _sumOfWeights += pair.Value;
        }

        //정규화 딕셔너리도 업데이트
        UpdateNormalizedDict();
    }

    private void UpdateNormalizedDict()
    {
        normalizedItemWeightDict.Clear();
        foreach (var pair in itemWeightDict)
        {
            normalizedItemWeightDict.Add(pair.Key, pair.Value / _sumOfWeights);
        }
    }

    private void CheckDuplicatedItem(T item)
    {
        if (itemWeightDict.ContainsKey(item))
            Debug.Log($"이미 {item} 아이템이 존재합니다.");
    }

    private void CheckNotExitedItem(T item)
    {
        if (!itemWeightDict.ContainsKey(item))
            Debug.Log($"이미 {item} 아이템이 목록에 존재하지 않습니다.");
    }

    private void CheckValidWeight(in double weight)
    {
        if (weight <= 0f)
            Debug.Log("가중치 값은 0보다 커야 합니다.");
    }
}
