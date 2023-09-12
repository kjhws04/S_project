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
    //public double itemWeightDict;
    //public int istMore4Stars;

    //public Gacha(double _itemWeigthDict, int _istMore4Stars)
    //{
    //    itemWeightDict = _itemWeigthDict;
    //    istMore4Stars = _istMore4Stars;
    //}

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

    //����ġ ���� ������ ���� �������� ����
    private bool isDirty;
    private double _sumOfWeights;

    //������
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

    //Add �Լ�
    //���ο� ������ ����ġ �� �߰�
    public void Add(T item, double weight)
    {
        CheckDuplicatedItem(item);
        CheckValidWeight(weight);

        itemWeightDict.Add(item, weight);
        isDirty = true;
    }

    //���ο� ������ ����ġ �ֵ� �߰�
    public void Add(params (T item, double weight)[] pairs)
    {
        foreach (var pair in pairs)
        {
            CheckDuplicatedItem(pair.item);
            CheckValidWeight(pair.weight);

            itemWeightDict.Add(pair.item, pair.weight);
        }
    }

    //���� �Լ�
    //��Ͽ��� ��� ������ ����
    public void Remove(T item)
    {
        CheckNotExitedItem(item);

        itemWeightDict.Remove(item);
        isDirty = true;
    }

    // ��� �������� ����ġ ����
    public void ModifyWeight(T item, double weight)
    {
        CheckNotExitedItem(item);
        CheckValidWeight(weight);

        itemWeightDict[item] = weight;
        isDirty = true;
    }

    // ���� �õ� �缳��
    public void ReSeed(int seed)
    {
        randomInstance = new System.Random(seed);
    }

    //Getter �Լ�
    // ���� �̱�
    public T GetRandomPick()
    {
        // ���� ���
        double chance = randomInstance.NextDouble(); //TODO
        chance *= SumOfWeights;

        return GetRandomPick(chance);
    }

    // ���� ���� ���� ������ �̱�
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

    //��� �������� ����ġ Ȯ��
    public double GetWeight(T item)
    {
        return itemWeightDict[item];
    }

    // ��� �������� ����ȭ�� ����ġ Ȯ��
    public double GetNormalizedWeight(T item)
    {
        CalculateSumIfDirty();
        return normalizedItemWeightDict[item];
    }

    // ������ ��� Ȯ�� (�б� ����)
    public ReadOnlyDictionary<T, double> GetItemDictReadonly()
    {
        return new ReadOnlyDictionary<T, double>(itemWeightDict);
    }

    // ����ġ ���� 1�� �ǵ��� ����ȭ�� ������ ��� Ȯ�� (�б� ����)
    public ReadOnlyDictionary<T, double> GetNormalizedItemDictReadonly()
    {
        CalculateSumIfDirty();
        return new ReadOnlyDictionary<T, double>(normalizedItemWeightDict);
    }

    //��ȣ �Լ�
    // ��� �������� ����ġ �� ����� ����
    private void CalculateSumIfDirty()
    {
        if (!isDirty) return;
        isDirty = false;

        _sumOfWeights = 0.0f;
        foreach (var pair in itemWeightDict)
        {
            _sumOfWeights += pair.Value;
        }

        //����ȭ ��ųʸ��� ������Ʈ
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
            Debug.Log($"�̹� {item} �������� �����մϴ�.");
    }

    private void CheckNotExitedItem(T item)
    {
        if (!itemWeightDict.ContainsKey(item))
            Debug.Log($"�̹� {item} �������� ��Ͽ� �������� �ʽ��ϴ�.");
    }

    private void CheckValidWeight(in double weight)
    {
        if (weight <= 0f)
            Debug.Log("����ġ ���� 0���� Ŀ�� �մϴ�.");
    }
}
