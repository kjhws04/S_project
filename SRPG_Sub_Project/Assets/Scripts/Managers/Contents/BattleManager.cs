using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Define.GameStep StepType = Define.GameStep.Unknown;
    public List<Transform> _unit = new List<Transform>();
    public List<Stat> _p1UnitList = new List<Stat>();
    public List<Stat> _p2UnitList = new List<Stat>();
    public List<Stat> _p3UnitList = new List<Stat>();

    public bool win;
    public bool lose;

    public float _findTimer = 0.1f;

    private void Awake()
    {
        SoonsoonData.Instance.SAM = this;
    }

    public void UnitSetting(List<Transform> unit,List<Stat> _p1Unit, List<Stat> _p2Unit, List<Stat> _p3Unit)
    {
        _p1UnitList.Clear();
        _p2UnitList.Clear();
        _p3UnitList.Clear();

        _unit = unit;
        _p1UnitList = _p1Unit;
        _p2UnitList = _p2Unit;
        _p3UnitList = _p3Unit;

        SetUnitList();
    }

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
                        _unit[i].GetChild(j).gameObject.tag = "P1";
                        _p1UnitList[j].BattleStart();
                        break;
                    case 1:
                        _p2UnitList.Add(_unit[i].GetChild(j).GetComponent<Stat>());
                        _unit[i].GetChild(j).gameObject.tag = "P2";
                        _p2UnitList[j].BattleStart();
                        break;
                    case 2:
                        _p3UnitList.Add(_unit[i].GetChild(j).GetComponent<Stat>());
                        _unit[i].GetChild(j).gameObject.tag = "P3";
                        _p3UnitList[j].BattleStart();
                        break;
                }
            }
        }
    }

    public Stat GetTarget(Stat unit)
    {
        Stat tUnit = null;

        List<Stat> tList = new List<Stat>();
        switch (unit.tag)
        {
            case "P1": tList = _p2UnitList; break;
            case "P2": tList = _p1UnitList; break;
        }

        float tsDis = float.MaxValue;

        for (var i = 0; i < tList.Count; i++)
        {
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

    public void Win() //TODO
    {
        if (!win)
        {
            Debug.Log("Win");
            win = true;
        }
    }
    public void Lose()
    {
        if (!lose)
        {
            Debug.Log("Lose");
            lose = true;
        }
    }
}
