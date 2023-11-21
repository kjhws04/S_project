using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    #region Pool
    class Pool
    {
        public GameObject Original { get; private set; } // 풀링할 원본 오브젝트
        public Transform Root { get; set; } // 생성된 object를 담을 부모 root의 transform

        Stack<Poolable> _poolStack = new Stack<Poolable>(); // 재사용 가능한 오브젝트들을 담을 스택

        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";
            // 지정된 개수만큼 미리 오브잭트를 생성한 후 풀에 추가
            for (int i = 0; i < count; i++)
            {
                Push(Create());
            }
        }

        // 새로운 Poolable 오브젝트 생성
        Poolable Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }

        // 풀에 Poolable 오브젝트 추가
        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);

            _poolStack.Push(poolable);
        }

        // 풀에서 Poolable 오브젝트 가져오기
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

    // <summary>
    // 오브잭트 풀을 생성, 초기화
    // </summary>
    public void CreatePool(GameObject org, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(org, count);
        pool.Root.parent = _root;

        _pool.Add(org.name, pool); // 풀을 _pool 딕셔너리에 추가
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
