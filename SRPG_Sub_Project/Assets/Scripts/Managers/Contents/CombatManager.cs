using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager
{
    public bool isCritical = false;
    public bool isBlock = false;
    public bool isMiss = false;

    // <summary>
    // �߰� ���� (�����ڰ� �ǰ��ں��� �ӵ� ������ 5�̻��̶�� �߰� �߻�)
    // </summary>
    public bool IsChase(Stat _AttackChar, Stat _DefenceChar)
    {
        if (_AttackChar.Spd - _DefenceChar.Spd >= 5)
            return true;
        return false;
    }

    // <summary>
    // ����� ���� ü�°� ���õ� ������
    // </summary>
    public int Combat(Stat _AttackChar, Stat _DefenceChar)
    {
        int damage = FinalDamage(_AttackChar, _DefenceChar);
        if (damage >= _DefenceChar.CurrentHp)
            damage = _DefenceChar.CurrentHp; //����� ���� ü�º��� ������ ���ݷ��� ���� ü�°� ����. 

        #region DebugZone
        //Debug.Log($"���� ������ : {damage}, ���� ü�� : {_DefenceChar.CurrentHp - damage}");
        //Debug.Log($"���ݷ� : {AttackDamage(_AttackChar, _DefenceChar)}");

        //Debug.Log($"�ʻ� ������ : {CriticalCalculate(_AttackChar, _DefenceChar)}%");
        //Debug.Log($"�Ʊ� �ʻ� : {CriticalPercentage(_AttackChar)}, ����ʻ� ȸ�� : {CriticalAvoidPercentage(_DefenceChar)}");

        //Debug.Log($"���� ������ : {AccuracyCalculate(_AttackChar, _DefenceChar)}%");
        //Debug.Log($"�Ʊ� ���� : {AccuracyPercentage(_AttackChar)}, ��� ȸ�� Ȯ�� : {AvoidPercentage(_DefenceChar)}");
        #endregion

        return Mathf.FloorToInt(damage);
    }

    // <summary>
    // ����� ���� ü�°� ������� ������ ������ (���߽� ���� else ȸ��)
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
    // ���� �ʻ� ���� ������ ��� (ġ��Ÿ�� * 3)
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
    // Ÿ�Ժ� ����ġ ���� ���� ���� ��� (���� - ����or����)
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
        //��� ����
        isBlock = (cur <= 0);
        cur = isBlock ? 0 : cur;

        return Mathf.FloorToInt(cur);
    }

    // <summary>
    // ���� Ÿ�� ���� ������ ��� (�ٷ�or���� + �������� ����)
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
    // ���� ��� (�ٷ�or���� (+ �������� ����))
    // </summary>
    public int AttackDamageCalculate(int damage)
    {
        return Mathf.FloorToInt(damage);
    }

    // <summary>
    // ���� ��� (����Ȯ�� - ȸ��Ȯ��)
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
    // ���� Ȯ�� (�⺻ ���� 70 + ���*2 + ���/2 //+ ���� + ��Ÿ//)
    // </summary>
    public int AccuracyPercentage(Stat _AttackChar)
    {
        return Mathf.FloorToInt(70f + (_AttackChar.Tec * 2) + Mathf.FloorToInt(_AttackChar.Luk / 2)/* + _AttackChar.MainWeapon.increaseVal*/);
    }

    // <summary>
    // ȸ�� Ȯ�� (�ӵ�*2 + ���/2 //+ ���� + ��Ÿ// ) //E
    // </summary>
    public int AvoidPercentage(Stat _DefenceChar)
    {
        return Mathf.FloorToInt((_DefenceChar.Spd * 2) + Mathf.FloorToInt(_DefenceChar.Luk / 2)/* + _AttackChar.MainWeapon.increaseVal*/);
    }

    // <summary>
    // �ʻ� ��� (�ʻ�Ȯ�� - �ʻ�ȸ��Ȯ��)
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
    // �ʻ� Ȯ�� (���/2 + //���� + ��Ÿ//)
    // </summary>
    public int CriticalPercentage(Stat _AttackChar)
    {
        return Mathf.FloorToInt(Mathf.FloorToInt(_AttackChar.Tec / 2)/* + _AttackChar.MainWeapon.increaseVal*/);
    }

    // <summary>
    // �ʻ� ȸ�� Ȯ�� (��� + //���� + ��Ÿ//) //E
    // </summary>
    public int CriticalAvoidPercentage(Stat _DefenceChar)
    {
        return Mathf.FloorToInt(_DefenceChar.Luk /* + _AttackChar.MainWeapon.increaseVal*/);
    }

    // <summary>
    // ���� ȿ�� ���ϱ� (����1 + ����2)
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
    // �ΰ� ȿ�� ���ϱ� (ex) �нú�, ��Ƽ��, ������ ���)
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
    // ���� ����
    // </summary>
    public int RandomVal()
    {
        int rand = Random.Range(0, 100);
        return Mathf.FloorToInt(rand);
    }
}
