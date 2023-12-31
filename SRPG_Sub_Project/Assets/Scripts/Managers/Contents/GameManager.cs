using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    GameObject _userData;

    public GameObject GetUserData() { return _userData; }

    public void Init()
    {
        UserData();
        Application.targetFrameRate = 60;
    }

    //유저의 데이터 생성
    public void UserData()
    {
        GameObject go = GameObject.Find("@UserData");
        if (go == null)
        {
            go = new GameObject { name = "@UserData" };
            go.AddComponent<UserData>();
        }
        GameObject.DontDestroyOnLoad(go);

        _userData = go; 
    }
}
