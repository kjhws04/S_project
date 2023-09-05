using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    #region Contents
    #endregion

    #region Core
    CombatManager _combat = new CombatManager();

    public static CombatManager Combat { get { return Instance._combat; } }
    #endregion

    void Start()
    {
        Init();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            ManagersInit();
        }
    }

    public static void ManagersInit()
    {
    }

    public static void Clear()
    { 
    
    }
}
