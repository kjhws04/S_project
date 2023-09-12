using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int idx = name.LastIndexOf('/');
            if (idx >= 0)
                name = name.Substring(idx + 1);

            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
                return go as T;
        }
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject org = Load<GameObject>($"Prefabs/{path}");
        if (org == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        if (org.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(org, parent).gameObject;

        GameObject go = Object.Instantiate(org, parent);
        go.name = org.name;
        return go;
    }

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

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }
        Object.Destroy(go);
    }
}
