using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    #region Contents
    BattleManager _battle = new BattleManager();
    CombatManager _combat = new CombatManager();
    GameManager _game = new GameManager();
    MissionManager _mission = new MissionManager();
    StageManager _stage = new StageManager();

    public static BattleManager Battle { get { return Instance._battle; } }
    public static CombatManager Combat { get { return Instance._combat; } }
    public static GameManager Game { get { return Instance._game; } }
    public static MissionManager Mission { get { return Instance._mission; } }
    public static StageManager Stage { get { return Instance._stage; } }
    #endregion

    #region Core
    DataManager _data = new DataManager();
    FireBaseManager _fire = new FireBaseManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();

    public static DataManager Data { get { return Instance._data; } }
    public static FireBaseManager Fire { get { return Instance._fire; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }
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

    // <summary>
    // 각 Manager별, 초기화 부분
    // </summary>
    public static void ManagersInit()
    {
        s_instance._game.Init();

        //s_instance._fire.Init(); //LoginScene에서 init()
        s_instance._data.Init();
        s_instance._pool.Init();
        s_instance._sound.Init();
        s_instance._mission.Init();
    }

    public static void Clear()
    {
        Sound.Clear();
        Scene.Clear();
        UI.Clear();

        //pooling 된 go를 사용할 수 있으니, 마지막에 위치
        Pool.Clear();
    }
}
