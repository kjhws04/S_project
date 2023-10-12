using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager
{
    public bool isCritical = false;
    public bool isBlock = false;
    public bool isMiss = false;

    // <summary>
    // 추격 여부 (공격자가 피격자보다 속도 스텟이 5이상이라면 추격 발생)
    // </summary>
    public bool IsChase(Stat _AttackChar, Stat _DefenceChar)
    {
        if (_AttackChar.Spd - _DefenceChar.Spd >= 5)
            return true;
        return false;
    }

    // <summary>
    // 대상의 남은 체력과 관련된 데미지
    // </summary>
    public int Combat(Stat _AttackChar, Stat _DefenceChar)
    {
        int damage = FinalDamage(_AttackChar, _DefenceChar);
        if (damage >= _DefenceChar.CurrentHp)
            damage = _DefenceChar.CurrentHp; //대상의 남은 체력보다 높으면 공격력은 남은 체력과 같음. 

        #region DebugZone
        //Debug.Log($"최종 데미지 : {damage}, 남은 체력 : {_DefenceChar.CurrentHp - damage}");
        //Debug.Log($"공격력 : {AttackDamage(_AttackChar, _DefenceChar)}");

        //Debug.Log($"필살 성공률 : {CriticalCalculate(_AttackChar, _DefenceChar)}%");
        //Debug.Log($"아군 필살 : {CriticalPercentage(_AttackChar)}, 상대필살 회피 : {CriticalAvoidPercentage(_DefenceChar)}");

        //Debug.Log($"명중 성공률 : {AccuracyCalculate(_AttackChar, _DefenceChar)}%");
        //Debug.Log($"아군 명중 : {AccuracyPercentage(_AttackChar)}, 상대 회피 확률 : {AvoidPercentage(_DefenceChar)}");
        #endregion

        return Mathf.FloorToInt(damage);
    }

    // <summary>
    // 대상의 남은 체력과 상관없는 순수한 데미지 (명중시 공격 else 회피)
    // </summary>
    public int FinalDamage(Stat _AttackChar, Stat _DefenceChar)
    {
        int damage = 0;
        if (AccuracyCalculate(_AttackChar, _DefenceChar) >= RandomVal())
        {
            damage = FinalDamageCalculate(_AttackChar, _DefenceChar);
            isMiss = false;
        }
        else
        {
            damage = 0;
            isMiss = true;
        }
        return damage;
    }

    // <summary>
    // 최종 필살 포함 데미지 계산 (치명타시 * 3)
    // </summary>
    public int FinalDamageCalculate(Stat _AttackChar, Stat _DefenceChar)
    {
        int cur = 0;
        if (CriticalCalculate(_AttackChar, _DefenceChar) >= RandomVal())
        {
            cur = AttackDamage(_AttackChar, _DefenceChar) * 3; //cri 
            isCritical = true;
        }
        else
        {
            cur = AttackDamage(_AttackChar, _DefenceChar);
            isCritical = false;
        }
        return Mathf.FloorToInt(cur);
    }

    // <summary>
    // 타입별 방어수치 포함 최종 공격 계산 (공격 - 수비or마방)
    // </summary>
    public int AttackDamage(Stat _AttackChar, Stat _DefenceChar)
    {
        int cur = 0;
        int d_cur = 0;
        switch (_AttackChar.attackType)
        {
            case Weapon.WeaponType.AD:
                d_cur = _DefenceChar.Def;
                cur = AttackTypeCalculate(_AttackChar) - d_cur;
                break;
            case Weapon.WeaponType.AP:
                d_cur = _DefenceChar.MDef;
                cur = AttackTypeCalculate(_AttackChar) - d_cur;
                break;
        }
        //방어 여부
        isBlock = (cur <= 0);
        cur = isBlock ? 0 : cur;

        return Mathf.FloorToInt(cur);
    }

    // <summary>
    // 공격 타입 포함 데미지 계산 (근력or지능 + 무기위력 포함)
    // </summary>
    public int AttackTypeCalculate(Stat _AttackChar)
    {
        int cur = 0;
        switch (_AttackChar.attackType)
        {
            case Weapon.WeaponType.AD:
                cur += AttackDamageCalculate(_AttackChar.Str);
                break;
            case Weapon.WeaponType.AP:
                cur += AttackDamageCalculate(_AttackChar.Int);
                break;
        }
        return Mathf.FloorToInt(cur);
    }

    // <summary>
    // 공격 계산 (근력or지능 (+ 무기위력 포함))
    // </summary>
    public int AttackDamageCalculate(int damage)
    {
        return Mathf.FloorToInt(damage);
    }

    // <summary>
    // 명중 계산 (명중확률 - 회피확률)
    // </summary>
    public int AccuracyCalculate(Stat _AttackChar, Stat _DefenceChar)
    {
        int cur = 0;
        cur = Mathf.FloorToInt(AccuracyPercentage(_AttackChar) -
                               AvoidPercentage(_DefenceChar));
        if (cur >= 100)
            cur = 100;
        else if (cur <= 0)
            cur = 0;
        return cur;
    }

    // <summary>
    // 명중 확률 (기본 보정 70 + 기술*2 + 행운/2 //+ 무기 + 기타//)
    // </summary>
    public int AccuracyPercentage(Stat _AttackChar)
    {
        return Mathf.FloorToInt(70f + (_AttackChar.Tec * 2) + Mathf.FloorToInt(_AttackChar.Luk / 2)/* + _AttackChar.MainWeapon.increaseVal*/);
    }

    // <summary>
    // 회피 확률 (속도*2 + 행운/2 //+ 무기 + 기타// ) //E
    // </summary>
    public int AvoidPercentage(Stat _DefenceChar)
    {
        return Mathf.FloorToInt((_DefenceChar.Spd * 2) + Mathf.FloorToInt(_DefenceChar.Luk / 2)/* + _AttackChar.MainWeapon.increaseVal*/);
    }

    // <summary>
    // 필살 계산 (필살확률 - 필살회피확률)
    // </summary>
    public int CriticalCalculate(Stat _AttackChar, Stat _DefenceChar)
    {
        int cur = 0;
        cur = Mathf.FloorToInt(CriticalPercentage(_AttackChar) - CriticalAvoidPercentage(_DefenceChar));
        if (cur >= 100)
            cur = 100;
        else if (cur <= 0)
            cur = 0;
        return cur;
    }

    // <summary>
    // 필살 확률 (기술/2 + //무기 + 기타//)
    // </summary>
    public int CriticalPercentage(Stat _AttackChar)
    {
        return Mathf.FloorToInt(Mathf.FloorToInt(_AttackChar.Tec / 2)/* + _AttackChar.MainWeapon.increaseVal*/);
    }

    // <summary>
    // 필살 회피 확률 (행운 + //무기 + 기타//) //E
    // </summary>
    public int CriticalAvoidPercentage(Stat _DefenceChar)
    {
        return Mathf.FloorToInt(_DefenceChar.Luk /* + _AttackChar.MainWeapon.increaseVal*/);
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
