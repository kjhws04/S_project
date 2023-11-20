using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stage_1 : BaseScene
{
    UserData _userData;

    public GameObject[] _placementSlot;

    #region Unit Team List
    public List<Transform> _unit = new List<Transform>();
    public List<Stat> _p1UnitList = new List<Stat>();
    public List<Stat> _p2UnitList = new List<Stat>();
    public List<Stat> _p3UnitList = new List<Stat>();
    #endregion

    public GameObject settingBase;
    public GameObject startBtn;

    private void Start()
    {
        Init();
        Util.ChangeResolution();
        Managers.Sound.Play("BGM_02", Define.Sound.Bgm, 0.75f);
    }

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.BattleField;

        #region Stage Setting
        _userData = Managers.Game.GetUserData().GetComponent<UserData>();
        int stage = _userData.Stage;

        GameSetting(stage); // ������ �����ϴ� �κ�
        Managers.Stage.LoadMap(stage); // ���� �����ϴ� �κ�
        Managers.Battle.StepType = Define.GameStep.Setting; //ĳ���� ���� step (���� step�� ���� ����)
        #endregion
    }

    public override void Clear()
    {

    }

    // <summary>
    // 1. �ʱ� ������ �����ϴ� �κ� : ���� Ÿ�� ���� (�븻, ����)
    // </summary>
    void GameSetting(int stage)
    {
        Managers.Stage.StageSetting(_p2UnitList, stage);
        switch (Managers.Stage.gameType) // ���� Ÿ�Կ� ���� �ٸ� ���� ����
        {
            case Define.GameType.NomalStage:
                EnemyPositionSetting();
                break;
            case Define.GameType.BossStage:
                BossPositionSetting(_p2UnitList, _unit[1].gameObject);
                break;
        }
    }

    // <summary>
    // 1. �ʱ� ������ �����ϴ� �κ� : ���� pos ��ġ�� ���� (�븻)
    // </summary>
    void EnemyPositionSetting()
    {
        PositionSetting(_p2UnitList, _unit[1].gameObject, 2, 3);
    }

    // <summary>
    // 1. �ʱ� ������ �����ϴ� �κ� : ���� pos,scale ��ġ�� ���� (����)
    // </summary>
    void BossPositionSetting(List<Stat> unitList, GameObject target)
    {
        for (int i = 0; i < unitList.Count; i++)
        {
            unitList[i].transform.SetParent(target.transform);
            unitList[i].transform.position = new Vector3(3, 0, unitList[i].transform.position.z);
            unitList[i].transform.localScale = new Vector3(2, 2, 2);
        }
    }

    // <summary>
    // 1. �ʱ� ������ �����ϴ� �κ� : EnemyPositionSetting()���� x���� �̿��� for������ ĳ������ ��ġ ����
    // </summary>
    void PositionSetting(List<Stat> unitList, GameObject target, int startX, int endX)
    {
        for (int i = 0; i < unitList.Count; i++)
        {
            unitList[i].transform.SetParent(target.transform);
        }

        int index = 0;
        int unitCount = Mathf.Min(6, unitList.Count);

        for (int i = startX; i <= endX; i++)
        {
            for (int j = 1; j >= -1 && index < unitCount; j--)
            {
                unitList[index].transform.position = new Vector3(i, j, unitList[index].transform.position.z);
                index++;
            }
        }
    }

    // <summary>
    // 2. ������ �����ϴ� �κ� : �Ʒ��� �ּ����� ���� (�ʱ�ȭ �Ϸ�, ��ư���� ����)
    // </summary>
    //Alie Setting & Start
    public void StartBtn()
    {
        if (Managers.Battle._p1UnitList == null) // ������ �Ʊ��� ���� ���
            return;

        Managers.Sound.Play("BGM_02", Define.Sound.Bgm); // bgm ����

        settingBase.SetActive(false); // �˾�â ����
        startBtn.SetActive(false); // �˾�â ����

        #region Choose Alie Character
        AlieCharSetting();
        #endregion

        #region All Unit Setting
        Managers.Battle.UnitSetting(_unit, Managers.Battle._p1UnitList, _p2UnitList, _p3UnitList);
        AliePositionSetting();
        #endregion
        Managers.Battle.StepType = Define.GameStep.Battle; //���ӽ���
    }
    
    // <summary>
    // 3. �Ʊ� ������ ���� : Battle �޴����� ����� list�� �޾� ��
    // </summary>
    void AlieCharSetting()
    {
        _p1UnitList.Clear();

        _p1UnitList = Managers.Battle._p1UnitList;

        for (int i = 0; i < _p1UnitList.Count; i++)
        {
            GameObject unit = Managers.Resource.Instantiate($"Character/{_p1UnitList[i].Name}");
            unit.transform.SetParent(_unit[0]);
            Stat _tempStat = unit.GetComponent<Stat>();

            if (_userData._userCharData.ContainsKey(_p1UnitList[i].Name))
            {
                CopyFrom(_tempStat, _userData._userCharData[_p1UnitList[i].Name]);
            }
        }
    }

    // <summary>
    // 3. �Ʊ� ������ ���� : ���� list�� �������� ���� ����
    // </summary>
    public void AliePositionSetting()
    {
        int unitCount = Mathf.Min(6, _p1UnitList.Count);
        Dictionary<int, Vector3> positions = new Dictionary<int, Vector3>
        {
            {1, new Vector3(-3, 1, 0)},
            {2, new Vector3(-3, 0, 0)},
            {3, new Vector3(-3, -1, 0)},
            {4, new Vector3(-2, 1, 0)},
            {5, new Vector3(-2, 0, 0)},
            {6, new Vector3(-2, -1, 0)}
        };

        for (int i = 0; i < unitCount; i++)
        {
            if (positions.ContainsKey(_p1UnitList[i].SettingNum))
            {
                _p1UnitList[i].transform.position = positions[_p1UnitList[i].SettingNum];
            }
        }
    }

    // <summary>
    // ĳ������ stat�� ī���ϴ� �Լ�
    // </summary>
    public void CopyFrom(Stat _copy, Stat _stat)
    {
        _copy.modelImg = _stat.modelImg;
        _copy.cardImage = _stat.cardImage;
        _copy.proflieImg = _stat.proflieImg;

        _copy.attackType = _stat.attackType;
        _copy.Level = _stat.Level;
        _copy.Name = _stat.Name;

        _copy.CurClass = _stat.CurClass;
        _copy.WeaponClass = _stat.WeaponClass;
        _copy.MainWeapon = _stat.MainWeapon;

        _copy.Hp = _stat.Hp;
        _copy.Str = _stat.Str;
        _copy.Int = _stat.Int;
        _copy.Tec = _stat.Tec;
        _copy.Spd = _stat.Spd;
        _copy.Def = _stat.Def;
        _copy.MDef = _stat.MDef;
        _copy.Luk = _stat.Luk;
        _copy.Wei = _stat.Wei;

        _copy._unitFR = _stat._unitFR;
        _copy._unitAR = _stat._unitAR;
        _copy._unitAS = _stat._unitAS;
    }

    #region Buttons
    public void BtnMain()
    {
        Managers.Battle.StepType = Define.GameStep.Unknown;
        switch (Managers.Stage.gameType) // ���� Ÿ�Կ� ���� �ٸ� ���� ����
        {
            case Define.GameType.NomalStage:
                Managers.Scene.LoadScene(Define.Scene.BattleField);
                break;
            case Define.GameType.BossStage:
                Managers.Scene.LoadScene(Define.Scene.Main);
                break;
        }
    }
    #endregion
}