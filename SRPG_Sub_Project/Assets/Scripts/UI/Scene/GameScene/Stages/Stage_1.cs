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

        GameSetting(stage); // 게임을 세팅하는 부분
        Managers.Stage.LoadMap(stage); // 맵을 세팅하는 부분
        Managers.Battle.StepType = Define.GameStep.Setting; //캐릭터 세팅 step (게임 step에 따라 동작)
        #endregion
    }

    public override void Clear()
    {

    }

    // <summary>
    // 1. 초기 게임을 세팅하는 부분 : 게임 타입 결정 (노말, 보스)
    // </summary>
    void GameSetting(int stage)
    {
        Managers.Stage.StageSetting(_p2UnitList, stage);
        switch (Managers.Stage.gameType) // 게임 타입에 따라 다른 설정 수행
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
    // 1. 초기 게임을 세팅하는 부분 : 적의 pos 위치를 세팅 (노말)
    // </summary>
    void EnemyPositionSetting()
    {
        PositionSetting(_p2UnitList, _unit[1].gameObject, 2, 3);
    }

    // <summary>
    // 1. 초기 게임을 세팅하는 부분 : 적의 pos,scale 위치를 세팅 (보스)
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
    // 1. 초기 게임을 세팅하는 부분 : EnemyPositionSetting()에서 x값을 이용해 for문으로 캐릭터의 위치 세팅
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
    // 2. 게임을 시작하는 부분 : 아래의 주석으로 설명 (초기화 완료, 버튼으로 시작)
    // </summary>
    //Alie Setting & Start
    public void StartBtn()
    {
        if (Managers.Battle._p1UnitList == null) // 설정된 아군이 없는 경우
            return;

        Managers.Sound.Play("BGM_02", Define.Sound.Bgm); // bgm 세팅

        settingBase.SetActive(false); // 팝업창 닫음
        startBtn.SetActive(false); // 팝업창 닫음

        #region Choose Alie Character
        AlieCharSetting();
        #endregion

        #region All Unit Setting
        Managers.Battle.UnitSetting(_unit, Managers.Battle._p1UnitList, _p2UnitList, _p3UnitList);
        AliePositionSetting();
        #endregion
        Managers.Battle.StepType = Define.GameStep.Battle; //게임시작
    }
    
    // <summary>
    // 3. 아군 포지션 세팅 : Battle 메니저의 저장된 list를 받아 옴
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
    // 3. 아군 포지션 세팅 : 받은 list의 포지션을 직접 세팅
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
    // 캐릭터의 stat을 카피하는 함수
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
        switch (Managers.Stage.gameType) // 게임 타입에 따라 다른 설정 수행
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