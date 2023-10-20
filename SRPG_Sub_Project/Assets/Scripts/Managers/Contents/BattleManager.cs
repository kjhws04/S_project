using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Define.GameStep StepType = Define.GameStep.Unknown;
    public List<Transform> _unit = new List<Transform>();
    public List<Stat> _p1UnitList = new List<Stat>();
    public List<Stat> _p1UnitCheckList = new List<Stat>();
    public List<Stat> _p2UnitList = new List<Stat>();
    public List<Stat> _p3UnitList = new List<Stat>();

    public bool win;
    public bool lose;

    public float _findTimer = 0.1f;

    UserData _userData;

    private void Awake()
    {
        SoonsoonData.Instance.SAM = this;
    }

    // <summary>
    // Unit의 피아 구분을 시작하는 함수 (초기화, 배열 저장) & 외부에서 불러서 사용
    // </summary>
    public void UnitSetting(List<Transform> unit,List<Stat> _p1Unit, List<Stat> _p2Unit, List<Stat> _p3Unit)
    {
        win = false;
        _p1UnitList.Clear();
        _p1UnitCheckList.Clear();
        _p2UnitList.Clear();
        _p3UnitList.Clear();

        _unit = unit;
        _p1UnitList = _p1Unit;
        _p2UnitList = _p2Unit;
        _p3UnitList = _p3Unit;

        SetUnitList();
    }
    
    // <summary>
    // Stat과 Tag를 부여하는 함수
    // </summary>
    void SetUnitList()
    {
        for (int i = 0; i < _unit.Count; i++)
        {
            for (int j = 0; j < _unit[i].childCount; j++)
            {
                switch (i)
                {
                    case 0:
                        _p1UnitList.Add(_unit[i].GetChild(j).GetComponent<Stat>());
                        _p1UnitCheckList.Add(_unit[i].GetChild(j).GetComponent<Stat>());
                        _unit[i].GetChild(j).gameObject.tag = "P1";
                        _p1UnitList[j].BattleStart();
                        break;
                    case 1:
                        //_p2UnitList.Add(_unit[i].GetChild(j).GetComponent<Stat>()); //적군 데이터는 따로 관리
                        _unit[i].GetChild(j).gameObject.tag = "P2";
                        _p2UnitList[j].BattleStart();
                        break;
                    case 2:
                        _p3UnitList.Add(_unit[i].GetChild(j).GetComponent<Stat>()); //3군 데이터 예정
                        _unit[i].GetChild(j).gameObject.tag = "P3";
                        _p3UnitList[j].BattleStart();
                        break;
                }
            }
        }
    }

    // <summary>
    // Tag에 따른 적군 배열을 받아, 타겟으로 설정하는 함수
    // </summary>
    public Stat GetTarget(Stat unit)
    {
        Stat tUnit = null;

        List<Stat> tList = new List<Stat>();
        switch (unit.tag)
        {
            case "P1": tList = _p2UnitList; break;
            case "P2": tList = _p1UnitCheckList; break;
        }

        float tsDis = float.MaxValue;

        for (var i = 0; i < tList.Count; i++)
        {
            if (tList[i] == null)
                Debug.Log("에러1");
            float tDis = ((Vector3)tList[i].transform.position - (Vector3)unit.transform.position).sqrMagnitude;

            if (tList[i].gameObject.activeInHierarchy)
            {
                if (tList[i]._unitState != Stat.UnitState.death)
                {
                    if (tDis < tsDis)
                    {
                        tUnit = tList[i];
                        tsDis = tDis;
                    }
                }
            }
        }

        return tUnit;
    }
    
    // <summary>
    // 진형 배열을 받아 승패를 결정하는 함수
    // </summary>
    #region Result
    public void Win() //TODO
    {
        if (!win)
        {
            Managers.Battle.StepType = Define.GameStep.Result;
            _userData = Managers.Game.GetUserData().GetComponent<UserData>();
            _userData.StageCount++;
            Debug.Log("Win");
            win = true;
        }
    }
    public void Lose()
    {
        if (!lose)
        {
            Managers.Battle.StepType = Define.GameStep.Result;
            Debug.Log("Lose");
            lose = true;
        }
    }
    #endregion
}
