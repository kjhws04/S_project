using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene{get { return GameObject.FindObjectOfType<BaseScene>(); } }

    // <summary>
    // Aync�迭 �Լ� ����, �������� Init(), Clear() �۾��� ���� ���� LoadScene() �Լ��� ���
    // </summary>
    public void LoadScene(Define.Scene type)
    {
        Managers.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    // <summary>
    // LoadScene���� �ʿ��� �ű⺯��(type)�� string������ �ޱ� ���� �Լ�
    // </summary>
    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear(); //�� �̵���, �ڵ����� Clear() �Լ��� ���
    }
}
