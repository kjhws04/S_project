using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Val>
{
    Dictionary<Key, Val> MakeDic();
}

public class DataManager
{
    public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();

    public void Init()
    {
        StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDic();
    }

    Loader LoadJson<Loader, Key, Val>(string path) where Loader : ILoader<Key, Val>
    {
        TextAsset text = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(text.text);
    }
}
