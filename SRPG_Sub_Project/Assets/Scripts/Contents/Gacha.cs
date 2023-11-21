using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using System;

/*
    [����ġ ���� �̱�]

    - ���׸��� ���� �������� Ÿ���� ������ ��üȭ�Ͽ� ����Ѵ�.
    - �ߺ��Ǵ� �������� ������ ��ųʸ��� �����Ͽ���.
    - ����ġ�� 0���� ���� ��� ���ܸ� ȣ���Ѵ�.

    - double SumOfWeights : ��ü �������� ����ġ ��(�б� ���� ������Ƽ)

    - void Add(T, double) : ���ο� ������-����ġ ���� �߰��Ѵ�.
    - void Add(params (T, double)[]) : ���ο� ������-����ġ ���� ���� �� �߰��Ѵ�.
    - void Remove(T) : ��� �������� ��Ͽ��� �����Ѵ�.
    - void ModifyWeight(T, double) : ��� �������� ����ġ�� �����Ѵ�.
    - void ReSeed(int) : ���� �õ带 �缳���Ѵ�.

    - T GetRandomPick() : ���� ������ ��Ͽ��� ����ġ�� ����Ͽ� �������� �׸� �ϳ��� �̾ƿ´�.
    - T GetRandomPick(double) : �̹� ���� Ȯ�� ���� �Ű������� �־�, �ش�Ǵ� �׸� �ϳ��� �̾ƿ´�.
    - double GetWeight(T) : ��� �������� ����ġ�� ���´�.
    - double GetNormalizedWeight(T) : ��� �������� ����ȭ�� ����ġ�� ���´�.

    - ReadonlyDictionary<T, double> GetItemDictReadonly() : ��ü ������ ����� �б����� �÷������� �޾ƿ´�.
    - ReadonlyDictionary<T, double> GetNormalizedItemDictReadonly()
      : ��ü �������� ����ġ ������ 1�� �ǵ��� ����ȭ�� ������ ����� �б����� �÷������� �޾ƿ´�.
*/
// <summary>
// ����ġ ���� �̱�
// </summary>

public class Gacha<T>
{
    // ��ü �������� ����ġ �� (�б� ����)
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
    //Ȯ���� ����ȭ�� ������ ���
    private readonly Dictionary<T, double> normalizedItemWeightDict;
    private readonly Dictionary<T, bool> isChar;

    //����ġ ���� ������ ���� �������� ����
    private bool isDirty;
    private double _sumOfWeights;

    // [[������]]
    // <summary>
    // Gacha Ŭ������ �Ű������� ���� ������
    // ���� �����⸦ �ʱ�ȭ�ϰ�, �����۰� ����ġ�� ������ ��ųʸ����� �ʱ�
    // �ʱ� ���¿����� ����ġ�� ���� ������ ���� ����(isDirty = true)
    // </summary>
    public Gacha()
    {
        randomInstance = new System.Random();
        itemWeightDict = new Dictionary<T, double>();  // �����۰� ����ġ�� ������ ��ųʸ� �ʱ�ȭ
        normalizedItemWeightDict = new Dictionary<T, double>(); // ����ȭ�� ����ġ�� ������ ��ųʸ� �ʱ�ȭ
        isChar = new Dictionary<T, bool>();  // �� �������� ĳ�������� ���θ� ������ ��ųʸ� �ʱ�ȭ (true = char, flase = weapon)
        isDirty = true; // ����ġ �� ��� �ʿ�
        _sumOfWeights = 0.0; // ����ġ�� ��
    }

    // <summary>
    // Gacha Ŭ������ ���� �õ带 �޾ƿ� �ʱ�ȭ
    // �Ű������� ���� ���� �õ带 ����Ͽ� ���� �����⸦ �ʱ�ȭ�ϰ�, �����۰� ����ġ�� ������ ��ųʸ����� �ʱ�ȭ
    // �ʱ� ���¿����� ����ġ�� ���� ������ ���� ����(isDirty = true)
    // </summary>
    public Gacha(int randomSeed)
    {
        randomInstance = new System.Random(randomSeed);
        itemWeightDict = new Dictionary<T, double>();  // �����۰� ����ġ�� ������ ��ųʸ� �ʱ�ȭ
        normalizedItemWeightDict = new Dictionary<T, double>(); // ����ȭ�� ����ġ�� ������ ��ųʸ� �ʱ�ȭ
        isChar = new Dictionary<T, bool>(); // �� �������� ĳ�������� ���θ� ������ ��ųʸ� �ʱ�ȭ (true = char, flase = weapon)
        isDirty = true;
        _sumOfWeights = 0.0; // ����ġ�� ��
    }

    // <summary>
    // �����۰� ����ġ�� �߰��ϴ� �޼��� Gacha�� �����ϰ�, Add�� �߰��ؼ� ���
    // ���ο� �����۰� �ش� �������� ����ġ�� �޾� ��ųʸ��� �߰�
    // �߰� �Ŀ��� ����ġ�� ���� �ٽ� ���Ǿ�� �� (isDirty)
    // </summary>
    public void Add(T item, double weight, bool _isChar)
    {
        CheckDuplicatedItem(item); // �ߺ��� �������� �̹� �����ϴ��� Ȯ��
        CheckValidWeight(weight); // ����ġ�� ��ȿ���� Ȯ��

        itemWeightDict.Add(item, weight); // �����۰� ����ġ�� ��ųʸ��� �߰�
        isChar.Add(item, _isChar); // �������� ĳ�������� ���θ� ��ųʸ��� �߰�
        isDirty = true;
    }

    // <summary>
    // ���� ���� �����۰� ����ġ ���� �� ���� �߰��ϴ� �޼���
    // �� �����۰� ����ġ ���� �޾ƿͼ� �ݺ����� ���� ��ųʸ��� �߰�
    // �߰� �Ŀ��� ����ġ�� ���� �ٽ� ���Ǿ�� �� (isDirty)
    // </summary>
    public void Add(params (T item, double weight, bool _isChar)[] pairs)
    {
        foreach (var pair in pairs)
        {
            CheckDuplicatedItem(pair.item); // �ߺ��� �������� �̹� �����ϴ��� Ȯ��
            CheckValidWeight(pair.weight); // ����ġ�� ��ȿ���� Ȯ��

            itemWeightDict.Add(pair.item, pair.weight); // �����۰� ����ġ�� ��ųʸ��� �߰�
            isChar.Add(pair.item, pair._isChar); // �������� ĳ�������� ���θ� ��ųʸ��� �߰�
        }
        isDirty = true; //����ġ ��� �ʿ�
    }

    // [[���� �Լ�]]
    // <summary>
    // ��ųʸ����� Ư�� �������� �����ϴ� �޼���
    // ���� �Ŀ��� ����ġ�� ���� �ٽ� ���Ǿ�� ��
    // </summary>
    public void Remove(T item)
    {
        CheckNotExitedItem(item); // �����Ϸ��� �������� ��Ͽ� �������� ������ ���� �߻�

        itemWeightDict.Remove(item); // ��ųʸ����� ������ ����
        isDirty = true; // ����ġ�� ���� �ٽ� ���Ǿ�� ��
    }

    // <summary>
    // Ư�� �������� ����ġ�� �����ϴ� �޼���
    // ���� �Ŀ��� ����ġ�� ���� �ٽ� ���Ǿ�� ��
    // </summary>
    public void ModifyWeight(T item, double weight)
    {
        CheckNotExitedItem(item); // ����ġ�� �����Ϸ��� �������� ��Ͽ� �������� ������ ���� �߻�
        CheckValidWeight(weight); // ������ ����ġ�� ��ȿ���� Ȯ��

        itemWeightDict[item] = weight; // ��ųʸ����� �ش� �������� ����ġ ����
        isDirty = true; // ����ġ�� ���� �ٽ� ���Ǿ�� ���� ǥ��
    }

    // <summary>
    // Ư�� �������� ĳ�������� ���θ� ��ȯ�ϴ� �޼���
    // </summary>
    public bool CheckIsChar(T item)
    {
        return isChar[item]; // �ش� �������� ĳ�������� ���� ��ȯ
    }

    // <summary>
    // ���� �õ带 �缳���ϴ� �޼���
    // </summary>
    public void ReSeed(int seed)
    {
        randomInstance = new System.Random(seed); // ���ο� ���� �õ带 ����Ͽ� �ʱ�ȭ
    }

    // [[Getter]] �Լ�
    // <summary>
    // �������� �������� �̴� �޼���
    // ������ �����Ͽ� ����ġ�� ����ϰ�, �ش� ����ġ�� �°� �������� ��ȯ
    // </summary>
    public T GetRandomPick()
    {
        // ���� ���
        double chance = randomInstance.NextDouble(); // 0.0���� 1.0 ������ ���� ����
        chance *= SumOfWeights; // ������ ������ ��ü ����ġ�� ���Ͽ� ���� ����ġ�� ��ȯ

        return GetRandomPick(chance); // ��ȯ�� ����ġ�� ����Ͽ� �������� �̾ƿ�
    }

    // <summary>
    // �־��� ����ġ�� ���� �������� �̴� �޼���
    // �Ű������� ���� ����ġ�� ���� �������� ����ϰ� ��ȯ
    // </summary>
    public T GetRandomPick(double randomValue)
    {
        if (randomValue < 0.0) randomValue = 0.0; // ����ġ�� �����̸� 0���� ����
        if (randomValue > SumOfWeights) randomValue = SumOfWeights - 0.00000001; // ����ġ�� ��ü ���� �ʰ��ϸ� �ִ밪���� ����

        double current = 0.0;
        foreach (var pair in itemWeightDict)
        {
            current += pair.Value;

            if (randomValue < current)
            {
                return pair.Key; // ���� ����ġ�� �־��� ����ġ���� ũ�� �ش� ������ ��ȯ
            }
        }

        // ������� �����ϸ� ���� ��Ȳ���� �����ϰ� ���� �߻�
        throw new Exception($"Unreachable - [Random Value : {randomValue}, Current Value : {current}]");
    }

    // <summary>
    // Ư�� ������(T item)�� ����ġ�� ��ȯ�ϴ� �޼���
    // </summary>
    // <returns>�ش� �������� ����ġ</returns>
    public double GetWeight(T item)
    {
        return itemWeightDict[item];
    }

    // <summary>
    // Ư�� ��������(T item) ����ȭ�� ����ġ�� ��ȯ�ϴ� �޼���
    // ����ġ�� ���� 1�� �ǵ��� ����ȭ�� ���� ��ȯ
    // </summary>
    public double GetNormalizedWeight(T item)
    {
        CalculateSumIfDirty(); // ����ġ�� ���� ������ ���� ��� ���
        return normalizedItemWeightDict[item];  // ����ȭ�� ����ġ ��ȯ
    }

    // <summary>
    // ���� ������ ����� �б� ���� ��ųʸ��� ��ȯ�ϴ� �޼���
    // </summary>
    // <returns>�����۰� ����ġ�� ������ �б� ���� ��ųʸ�</returns>
    public ReadOnlyDictionary<T, double> GetItemDictReadonly()
    {
        return new ReadOnlyDictionary<T, double>(itemWeightDict); // �б� ���� ��ųʸ� ��ȯ
    }

    // <summary>
    // ����ġ�� ���� 1�� �ǵ��� ����ȭ�� ������ ����� �б� ���� ��ųʸ��� ��ȯ�ϴ� �޼���
    // </summary>
    // <returns>����ȭ�� ������ ����� ������ �б� ���� ��ųʸ�</returns>
    public ReadOnlyDictionary<T, double> GetNormalizedItemDictReadonly() 
    {
        CalculateSumIfDirty(); // ����ġ�� ���� ������ ���� ��� ���
        return new ReadOnlyDictionary<T, double>(normalizedItemWeightDict);  // ����ȭ�� ��ųʸ� ��ȯ
    }

    //��ȣ �Լ�
    // <summary>
    // ����ġ�� ���� ����ϰ�, �ʿ��� ��� ����ȭ�� ��ųʸ��� ������Ʈ�ϴ� �޼���
    // </summary>
    private void CalculateSumIfDirty()
    {
        if (!isDirty) return; // ����ġ�� ���� �̹� ���� ��� �ƹ� �۾��� �������� ����
        isDirty = false; // ����ġ�� ���� r���ϰ� �ִ� ���·� ǥ��

        _sumOfWeights = 0.0f; // ����ġ�� �� �ʱ�ȭ
        foreach (var pair in itemWeightDict)
        {
            _sumOfWeights += pair.Value;  // �� �������� ����ġ�� ���Ͽ� ��ü ����ġ�� ���� ���
        }

        // ����ȭ�� ��ųʸ� ������Ʈ
        UpdateNormalizedDict();
    }

    // <summary>
    // ����ġ�� ����ȭ�� ��ųʸ��� ������Ʈ�ϴ� �޼����Դϴ�.
    // �� �������� ����ġ�� ��ü ����ġ�� ������ ����ȭ�� ���� ����Ͽ� ��ųʸ��� �����մϴ�.
    // </summary>
    private void UpdateNormalizedDict()
    {
        normalizedItemWeightDict.Clear(); // ������ ����ȭ�� ��ųʸ��� ���
        foreach (var pair in itemWeightDict)
        {
            normalizedItemWeightDict.Add(pair.Key, pair.Value / _sumOfWeights);  // ����ȭ�� ���� ��ųʸ��� �߰�
        }
    }

    // <summary>
    // �ߺ��� �������� �̹� �����ϴ��� Ȯ���ϴ� �޼���
    // ���� �ߺ��� �������� �����ϸ� ����� �α׸� ���
    // </summary>
    private void CheckDuplicatedItem(T item)
    {
        if (itemWeightDict.ContainsKey(item))
            Debug.Log($"�̹� {item} �������� �����մϴ�.");
    }

    // <summary>
    // ��Ͽ� �������� �ʴ� �������� Ȯ���ϴ� �޼���
    // ���� ��Ͽ� �������� �ʴ� �������̶�� ����� �α׸� ���
    // </summary>
    private void CheckNotExitedItem(T item)
    {
        if (!itemWeightDict.ContainsKey(item))
            Debug.Log($"�̹� {item} �������� ��Ͽ� �������� �ʽ��ϴ�.");
    }

    // <summary>
    // ����ġ ���� ��ȿ���� Ȯ���ϴ� �޼���
    // ���� ����ġ ���� 0 �����̸� ����� �α׸� ���
    // </summary>
    private void CheckValidWeight(in double weight)
    {
        if (weight <= 0f)
            Debug.Log("����ġ ���� 0���� Ŀ�� �մϴ�.");
    }
}
