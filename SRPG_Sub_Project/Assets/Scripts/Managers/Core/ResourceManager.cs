using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager
{
    // <summary>
    // 제너릭으로 리소스를 로드
    // </summary>
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int idx = name.LastIndexOf('/');
            if (idx >= 0)
                name = name.Substring(idx + 1);

            // GameObject 타입의 리소스는 풀에 있는지 확인해봄
            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
                return go as T;
        }
        // 그 외의 타입은 리소스에서 직접 로드
        return Resources.Load<T>(path);
    }

    // <summary>
    // 프리펩을 로드해서 instance화 시키는 함수
    // </summary>
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject org = Load<GameObject>($"Prefabs/{path}");
        if (org == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        // Poolable 컴포넌트를 가지고 있는 경우 풀에서 가져옴
        if (org.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(org, parent).gameObject;

        // Poolable 컴포넌트가 없는 경우 직접 인스턴스화
        GameObject go = Object.Instantiate(org, parent);
        go.name = org.name;
        return go;
    }

    // Sprite 로드
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

    // 캐릭터 데이터 로드
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

    // 무기 데이터 로드
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
    // 오브잭트를 destroy하는 함수
    // </summary>
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        // Poolable 컴포넌트가 있는 경우 풀에 반환
        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        // 그 외의 경우 일반적으로 파괴
        Object.Destroy(go);
    }
}
