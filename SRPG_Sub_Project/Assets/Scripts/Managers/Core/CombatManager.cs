using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager
{
    #region Attacker Info
    Define.AttackType _type; //물리 or 마법
    int _damage; //근력or 지능
    int _tec; //기술
    int _speed; //속도
    int _luck; //행운
    int[] _damWeapon = new int[2]; //데미지 보정 무기
    int[] _damEtc = new int[2]; //데미지 기타
    int[] _accWeapon = new int[2]; //명중 보정 무기
    int[] _accEtc = new int[2]; //명중 보정 기타
    int[] _cirWeappon = new int[2]; //치명 보정 무기
    int[] _criEtc = new int[2]; //치명 보정 기타
    #endregion

    #region Defencer Info
    int E_curHp; //남은 체력
    int E_accSpeed; //속도
    int E_luck; //행운
    int E_defence; //수비
    int E_Mdefence; //마방
    int[] E_accAvoWeapon = new int[2]; //회피 보정 무기
    int[] E_accAvoEtc = new int[2]; //회피 보정 기타
    int[] E_cirAvoWeappon = new int[2]; //치명 회피 보정 무기
    int[] E_cirAviEtc = new int[2]; //치명 회피 보정 기타
    #endregion

    private void Start()
    {
    }

    public void Init()
    {
        #region test
        //_type = Define.AttackType.Magic; //물리 or 마법
        //_damage = 30; //근력or 지능
        //_tec = 13; //기술
        //_speed = 12; //속도
        //_luck = 40; //행운
        //_damWeapon[0] = 3; //데미지 보정 무기
        //_damEtc[0] = 1;
        //_accWeapon[0] = 20; //명중 보정 무기
        //_accEtc[0] = 5; //명중 보정 기타
        //_cirWeappon[0] = 50; //치명 보정 무기
        //_criEtc[0] = 5; //치명 보정 기타
        //E_curHp = 40; //남은 체력
        //E_accSpeed = 7; //속도
        //E_luck = 2; //행운
        //E_defence = 3; //수비
        //E_Mdefence = 30; //마방
        //E_accAvoWeapon[0] = 10; //회피 보정 무기
        //E_accAvoEtc[0] = 0; //회피 보정 기타
        //E_cirAvoWeappon[0] = 10; ; //치명 회피 보정 무기
        //E_cirAviEtc[0] = 0; //치명 회피 보정 기타

        //int cur;
        //cur = Combat(E_curHp, _tec, _luck, _accWeapon, _accEtc, E_accSpeed, E_luck, E_accAvoWeapon, E_accAvoEtc, 
        //       _cirWeappon, _criEtc, E_cirAvoWeappon, E_cirAviEtc, _type, E_defence, E_Mdefence, _damage, _damWeapon, _damEtc);
        #endregion
    }

    // <summary>
    // 대상의 남은 체력과 관련된 데미지
    // </summary>
    public int Combat(int E_curHp, int _tec, int _luck, int[] _accWeapon, int[] _accEtc, int E_accSpeed, int E_luck, int[] E_accAvoWeapon, int[] E_accAvoEtc,
                      int[] _cirWeappon, int[] _criEtc, int[] E_cirAvoWeappon, int[] E_cirAviEtc,
                      Define.AttackType E_type, int E_defence, int E_Mdefence, int _damage, int[] _damWeapon, int[] _damEtc)
    {
        int damage = FinalDamage(_tec, _luck, _accWeapon, _accEtc, E_accSpeed, E_luck, E_accAvoWeapon, E_accAvoEtc,
                      _cirWeappon, _criEtc, E_cirAvoWeappon, E_cirAviEtc,
                      E_type, E_defence, E_Mdefence, _damage, _damWeapon, _damEtc);
        if (damage >= E_curHp)
            damage = E_curHp; //대상의 남은 체력보다 높으면 공격력은 남은 체력과 같음. 

        #region DebugZone
        //Debug.Log($"최종 데미지 : {damage}, 남은 체력 : {E_curHp - damage}");
        //Debug.Log($"공격력 : {AttackDamage(E_type, E_defence, E_Mdefence, _damage, _damWeapon, _damEtc)}");

        //Debug.Log($"필살 성공률 : {CriticalCalculate(_tec, _cirWeappon, _criEtc, E_luck, E_cirAvoWeappon, E_cirAviEtc)}%");
        //Debug.Log($"아군 필살 : {CriticalPercentage(_tec, _cirWeappon, _criEtc)}, 상대필살 회피 : {CriticalAvoidPercentage(E_luck, E_cirAvoWeappon, E_cirAviEtc)}");

        //Debug.Log($"명중 성공률 : {AccuracyCalculate(_tec, _luck, _accWeapon, _accEtc, E_accSpeed, E_luck, E_accAvoWeapon, E_accAvoEtc)}%");
        //Debug.Log($"아군 명중 : {AccuracyPercentage(_tec, _luck, _accWeapon, _accEtc)}, 상대 회피 확률 : {AvoidPercentage(E_accSpeed, E_luck, E_accAvoWeapon, E_accAvoEtc)}");
        #endregion

        return Mathf.FloorToInt(damage);
    }

    // <summary>
    // 대상의 남은 체력과 상관없는 순수한 데미지 (명중시 공격 else 회피)
    // </summary>
    public int FinalDamage(int _tec, int _luck, int[] _accWeapon, int[] _accEtc, int E_accSpeed, int E_luck, int[] E_accAvoWeapon, int[] E_accAvoEtc,
                      int[] _cirWeappon, int[] _criEtc, int[] E_cirAvoWeappon, int[] E_cirAviEtc,
                      Define.AttackType E_type, int E_defence, int E_Mdefence, int _damage, int[] _damWeapon, int[] _damEtc)
    {
        int damage = 0;
        if (AccuracyCalculate(_tec, _luck, _accWeapon, _accEtc, E_accSpeed, E_luck, E_accAvoWeapon, E_accAvoEtc) >= RandomVal())
        {
            damage = FinalDamageCalculate(_tec, _cirWeappon, _criEtc, E_luck, E_cirAvoWeappon, E_cirAviEtc,
                              E_type, E_defence, E_Mdefence, _damage, _damWeapon, _damEtc);
        }
        else
        {
            damage = 0;
        }
        return damage;
    }

    // <summary>
    // 최종 필살 포함 데미지 계산 (치명타시 * 3)
    // </summary>
    public int FinalDamageCalculate(int _tec, int[] _cirWeappon, int[] _criEtc, int E_luck, int[] E_cirAvoWeappon, int[] E_cirAviEtc,
                      Define.AttackType E_type, int E_defence, int E_Mdefence, int _damage, int[] _damWeapon, int[] _damEtc)
    {
        int cur = 0;
        if (CriticalCalculate(_tec, _cirWeappon, _criEtc, E_luck, E_cirAvoWeappon, E_cirAviEtc) >= RandomVal())
        {
            cur = AttackDamage(E_type, E_defence, E_Mdefence, _damage, _damWeapon, _damEtc) * 3; //cri 
        }
        else
        {
            cur = AttackDamage(E_type, E_defence, E_Mdefence, _damage, _damWeapon, _damEtc);
        }
        return Mathf.FloorToInt(cur);
    }

    // <summary>
    // 최종 공격 계산 (공격 - 수비or마방)
    // </summary>
    public int AttackDamage(Define.AttackType E_type, int E_defence, int E_Mdefence, int _damage, int[] _weapon, int[] _etc)
    {
        int cur = 0;
        int d_cur = 0;
        switch (E_type)
        {
            case Define.AttackType.Physical:
                d_cur = E_defence;
                cur = AttackTypeCalculate(E_type, _damage, _weapon, _etc) - d_cur;
                break;
            case Define.AttackType.Magic:
                d_cur = E_Mdefence;
                cur = AttackTypeCalculate(E_type, _damage, _weapon, _etc) - d_cur;
                break;
        }
        if (cur <= 0)
            cur = 0;
        return Mathf.FloorToInt(cur);
    }

    // <summary>
    // 공격 타입 계산 (근력or지능)
    // </summary>
    public int AttackTypeCalculate(Define.AttackType _type, int _damage, int[] _weapon, int[] _etc)
    {
        int cur = 0;
        switch (_type)
        {
            case Define.AttackType.Physical:
                cur += AttackDamageCalculate(_damage, _weapon, _etc);
                break;
            case Define.AttackType.Magic:
                cur += AttackDamageCalculate(_damage, _weapon, _etc);
                break;
        }
        return Mathf.FloorToInt(cur);
    }

    // <summary>
    // 공격 계산 (근력or지능 + 무기위력 + 기타)
    // </summary>
    public int AttackDamageCalculate(int _damage, int[] _weapon, int[] _etc)
    {
        return Mathf.FloorToInt(_damage + WeaponEffect(_weapon) + EtcEffect(_etc));
    }

    // <summary>
    // 명중 계산 (명중확률 - 회피확률)
    // </summary>
    public int AccuracyCalculate(int _tec, int _luck, int[] _weapon, int[] _etc, int E_speed, int E_luck, int[] E_weapon, int[] E_etc)
    {
        int cur = 0;
        cur = Mathf.FloorToInt(AccuracyPercentage(_tec, _luck, _weapon, _etc) -
                               AvoidPercentage(E_speed, E_luck, E_weapon, E_etc));
        if (cur >= 100)
            cur = 100;
        else if (cur <= 0)
            cur = 0;
        return cur;
    }

    // <summary>
    // 명중 확률 (기술*2 + 행운/2 + 무기 + 기타)
    // </summary>
    public int AccuracyPercentage(int _tec, int _luck, int[] _weapon, int[] _etc)
    {
        return Mathf.FloorToInt((_tec * 2) + Mathf.FloorToInt(_luck / 2) + WeaponEffect(_weapon) + EtcEffect(_etc));
    }

    // <summary>
    // 회피 확률 (속도*2 + 행운/2 + 무기 + 기타 ) //E
    // </summary>
    public int AvoidPercentage(int _speed, int _luck, int[] _weapon, int[] _etc)
    {
        return Mathf.FloorToInt((_speed * 2) + Mathf.FloorToInt(_luck / 2) + WeaponEffect(_weapon) + EtcEffect(_etc));
    }

    // <summary>
    // 필살 계산 (필살확률 - 필살회피확률)
    // </summary>
    public int CriticalCalculate(int _tec, int[] _cirWeappon, int[] _criEtc, int E_luck, int[] E_avoWeappon, int[] E_aviEtc)
    {
        int cur = 0;
        cur = Mathf.FloorToInt(CriticalPercentage(_tec, _cirWeappon, _criEtc) -
                               CriticalAvoidPercentage(E_luck, E_avoWeappon, E_aviEtc));
        if (cur >= 100)
            cur = 100;
        else if (cur <= 0)
            cur = 0;
        return cur;
    }

    // <summary>
    // 필살 확률 (기술/2 + 무기 + 기타)
    // </summary>
    public int CriticalPercentage(int _tec, int[] _weappon, int[] _etc)
    {
        return Mathf.FloorToInt(Mathf.FloorToInt(_tec / 2) + WeaponEffect(_weappon) + EtcEffect(_etc));
    }

    // <summary>
    // 필살 회피 확률 (행운 + 무기 + 기타) //E
    // </summary>
    public int CriticalAvoidPercentage(int _luck, int[] _weappon, int[] _etc)
    {
        return Mathf.FloorToInt(_luck + WeaponEffect(_weappon) + EtcEffect(_etc));
    }

    // <summary>
    // 무기 효과 더하기 (무기1 + 무기2)
    // </summary>
    public int WeaponEffect(int[] _weapon)
    {
        int cur = 0;
        for (int i = 0; i < _weapon.Length; i++)
        {
            cur += _weapon[i];
        }
        Mathf.FloatToHalf(cur);
        return cur;
    }

    // <summary>
    // 부가 효과 더하기 (ex) 패시브, 액티브, 아이템 등등)
    // </summary>
    public int EtcEffect(int[] _etc)
    {
        int cur = 0;
        for (int i = 0; i < _etc.Length; i++)
        {
            cur += _etc[i];
        }
        Mathf.FloatToHalf(cur);
        return cur;
    }

    // <summary>
    // 랜덤 계산기
    // </summary>
    public int RandomVal()
    {
        int rand = Random.Range(0, 100);
        return Mathf.FloorToInt(rand);
    }
}
