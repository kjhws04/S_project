using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager
{
    // <summary>
    // ���ʸ����� ���ҽ��� �ε�
    // </summary>
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int idx = name.LastIndexOf('/');
            if (idx >= 0)
                name = name.Substring(idx + 1);

            // GameObject Ÿ���� ���ҽ��� Ǯ�� �ִ��� Ȯ���غ�
            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
                return go as T;
        }
        // �� ���� Ÿ���� ���ҽ����� ���� �ε�
        return Resources.Load<T>(path);
    }

    // <summary>
    // �������� �ε��ؼ� instanceȭ ��Ű�� �Լ�
    // </summary>
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject org = Load<GameObject>($"Prefabs/{path}");
        if (org == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        // Poolable ������Ʈ�� ������ �ִ� ��� Ǯ���� ������
        if (org.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(org, parent).gameObject;

        // Poolable ������Ʈ�� ���� ��� ���� �ν��Ͻ�ȭ
        GameObject go = Object.Instantiate(org, parent);
        go.name = org.name;
        return go;
    }

    // Sprite �ε�
    public Sprite SpriteLoad(string path)
    {
        Sprite org = Load<Sprite>($"Card/{path}");
        if (org == null)
        {
            Debug.Log($"Failed to load image : {path}");
            return null;
        }

        return org;
    }

    // ĳ���� ������ �ε�
    public GameObject SaveCharData(string path)
    {
        GameObject go = Load<GameObject>($"Prefabs/Character/{path}"); 
        if (go == null)
        {
            Debug.Log($"Failed to load data : {path}");
            return null;
        }
        return go;
    }

    // ���� ������ �ε�
    public WeaponStat SaveWeaponData(string path)
    {
        WeaponStat ws = Load<WeaponStat>($"Data/Weapon/{path}");
        if (ws == null)
        {
            Debug.Log($"Failed to load data : {path}");
            return null;
        }
        return ws;
    }

    // <summary>
    // ������Ʈ�� destroy�ϴ� �Լ�
    // </summary>
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        // Poolable ������Ʈ�� �ִ� ��� Ǯ�� ��ȯ
        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        // �� ���� ��� �Ϲ������� �ı�
        Object.Destroy(go);
    }
}
