using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    #region Pool
    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        Stack<Poolable> _poolStack = new Stack<Poolable>();

        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";

            for (int i = 0; i < count; i++)
            {
                Push(Create());
            }
        }

        Poolable Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);

            _poolStack.Push(poolable);
        }

        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (_poolStack.Count > 0)
                poolable = _poolStack.Pop();
            else
                poolable = Create();

            poolable.gameObject.SetActive(true);

            //DontDestroyOnLoad 해제 용도
            if (parent == null)
                poolable.transform.parent = Managers.Scene.CurrentScene.transform;
            poolable.transform.parent = parent;
            poolable.isUsing = true;

            return poolable;
        }
    }
    #endregion

    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    Transform _root;

    public void Init()
    {
        if (_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    // <summary>
    // Pool에 집어 넣는 기능
    // </summary>
    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;
        if (_pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
         }

        _pool[name].Push(poolable);
    }

    // <summary>
    // Pool에 있는 걸 꺼내는 기능
    // </summary>
    public Poolable Pop(GameObject org, Transform parent = null)
    {
        if (_pool.ContainsKey(org.name) == false)
            CreatePool(org);
        return _pool[org.name].Pop(parent);
    }

    public void CreatePool(GameObject org, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(org, count);
        pool.Root.parent = _root;

        _pool.Add(org.name, pool);
    }

    // <summary>
    // Original을 반환 (Pool에 있는지 확인)
    // </summary>
    public GameObject GetOriginal(string name)
    {
        if (_pool.ContainsKey(name) == false)
            return null;
        return _pool[name].Original;
    }

    // <summary>
    // 저장된 Pool을 Clear하는 함수
    // </summary>
    public void Clear()
    {
        foreach (Transform Child in _root)
        {
            GameObject.Destroy(Child.gameObject);
        }
        _pool.Clear();
    }
}
