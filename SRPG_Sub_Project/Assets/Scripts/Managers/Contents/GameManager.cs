using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject _userData;

    public GameObject GetUserData() { return _userData; }

    public void Init()
    {
        UserData();
    }

    //������ ������ ����
    public void UserData()
    {
        GameObject go = GameObject.Find("@UserData");
        if (go == null)
        {
            go = new GameObject { name = "@UserData" };
            go.AddComponent<UserData>();
        }
        DontDestroyOnLoad(go);

        _userData = go;
    }
}
