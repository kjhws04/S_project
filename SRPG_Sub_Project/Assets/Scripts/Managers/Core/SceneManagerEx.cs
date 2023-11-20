using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene{get { return GameObject.FindObjectOfType<BaseScene>(); } }

    // <summary>
    // Aync계열 함수 사용과, 구조상의 Init(), Clear() 작업을 위해 따로 LoadScene() 함수를 사용
    // </summary>
    public void LoadScene(Define.Scene type)
    {
        Managers.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    // <summary>
    // LoadScene에서 필요한 매기변수(type)을 string값으로 받기 위한 함수
    // </summary>
    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear(); //씬 이동시, 자동으로 Clear() 함수를 사용
    }
}
