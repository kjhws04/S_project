using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    // <summary>
    // Component가 있으면 get하고, 없으면 add하는 함수 (적용될 오브잭트)
    // </summary>
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }

    // <summary>
    // 자식 오브잭트의 T Component 뱉는 함수 (최상위 부모, 이름[비교하지 않음], 재귀적[자식의 자식까지 찾을 것인지]) 조건 : 유니티 엔진 obj
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
    // 자식 오브잭트의 GameObject 만을 뱉는 함수 (최상위 부모, 이름[비교하지 않음], 재귀적[자식의 자식까지 찾을 것인지]) 조건 : 유니티 엔진 obj
    // </summary>
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;
        return transform.gameObject;
    }
    
    // <summary>
    // Dic에서 유닛이름에 해당하는 데이터 (Stat)뽑기
    // </summary>
    public static Stat GetStatData(Dictionary<string, Stat> _charData, string _charName)
    {
        if (_charData.ContainsKey(_charName))
            return _charData[_charName];
        else
        {
            Debug.Log($"UserData에 {_charName}은 존재하지 않습니다.");
            return null;
        }
    }
}
