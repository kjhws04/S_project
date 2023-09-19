using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    // <summary>
    // Component�� ������ get�ϰ�, ������ add�ϴ� �Լ� (����� ������Ʈ)
    // </summary>
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }

    // <summary>
    // �ڽ� ������Ʈ�� T Component ��� �Լ� (�ֻ��� �θ�, �̸�[������ ����], �����[�ڽ��� �ڽı��� ã�� ������]) ���� : ����Ƽ ���� obj
    // </summary>
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);

                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }

    // <summary>
    // �ڽ� ������Ʈ�� GameObject ���� ��� �Լ� (�ֻ��� �θ�, �̸�[������ ����], �����[�ڽ��� �ڽı��� ã�� ������]) ���� : ����Ƽ ���� obj
    // </summary>
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;
        return transform.gameObject;
    }
    
    // <summary>
    // Dic���� �����̸��� �ش��ϴ� ������ (Stat)�̱�
    // </summary>
    public static Stat GetStatData(Dictionary<string, Stat> _charData, string _charName)
    {
        if (_charData.ContainsKey(_charName))
            return _charData[_charName];
        else
        {
            Debug.Log($"UserData�� {_charName}�� �������� �ʽ��ϴ�.");
            return null;
        }
    }
}
