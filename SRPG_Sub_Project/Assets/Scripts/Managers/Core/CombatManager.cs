using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager
{
    #region Attacker Info
    Define.AttackType _type; //���� or ����
    int _damage; //�ٷ�or ����
    int _tec; //���
    int _speed; //�ӵ�
    int _luck; //���
    int[] _damWeapon = new int[2]; //������ ���� ����
    int[] _damEtc = new int[2]; //������ ��Ÿ
    int[] _accWeapon = new int[2]; //���� ���� ����
    int[] _accEtc = new int[2]; //���� ���� ��Ÿ
    int[] _cirWeappon = new int[2]; //ġ�� ���� ����
    int[] _criEtc = new int[2]; //ġ�� ���� ��Ÿ
    #endregion

    #region Defencer Info
    int E_curHp; //���� ü��
    int E_accSpeed; //�ӵ�
    int E_luck; //���
    int E_defence; //����
    int E_Mdefence; //����
    int[] E_accAvoWeapon = new int[2]; //ȸ�� ���� ����
    int[] E_accAvoEtc = new int[2]; //ȸ�� ���� ��Ÿ
    int[] E_cirAvoWeappon = new int[2]; //ġ�� ȸ�� ���� ����
    int[] E_cirAviEtc = new int[2]; //ġ�� ȸ�� ���� ��Ÿ
    #endregion

    private void Start()
    {
    }

    public void Init()
    {
        #region test
        //_type = Define.AttackType.Magic; //���� or ����
        //_damage = 30; //�ٷ�or ����
        //_tec = 13; //���
        //_speed = 12; //�ӵ�
        //_luck = 40; //���
        //_damWeapon[0] = 3; //������ ���� ����
        //_damEtc[0] = 1;
        //_accWeapon[0] = 20; //���� ���� ����
        //_accEtc[0] = 5; //���� ���� ��Ÿ
        //_cirWeappon[0] = 50; //ġ�� ���� ����
        //_criEtc[0] = 5; //ġ�� ���� ��Ÿ
        //E_curHp = 40; //���� ü��
        //E_accSpeed = 7; //�ӵ�
        //E_luck = 2; //���
        //E_defence = 3; //����
        //E_Mdefence = 30; //����
        //E_accAvoWeapon[0] = 10; //ȸ�� ���� ����
        //E_accAvoEtc[0] = 0; //ȸ�� ���� ��Ÿ
        //E_cirAvoWeappon[0] = 10; ; //ġ�� ȸ�� ���� ����
        //E_cirAviEtc[0] = 0; //ġ�� ȸ�� ���� ��Ÿ

        //int cur;
        //cur = Combat(E_curHp, _tec, _luck, _accWeapon, _accEtc, E_accSpeed, E_luck, E_accAvoWeapon, E_accAvoEtc, 
        //       _cirWeappon, _criEtc, E_cirAvoWeappon, E_cirAviEtc, _type, E_defence, E_Mdefence, _damage, _damWeapon, _damEtc);
        #endregion
    }

    // <summary>
    // ����� ���� ü�°� ���õ� ������
    // </summary>
    public int Combat(int E_curHp, int _tec, int _luck, int[] _accWeapon, int[] _accEtc, int E_accSpeed, int E_luck, int[] E_accAvoWeapon, int[] E_accAvoEtc,
                      int[] _cirWeappon, int[] _criEtc, int[] E_cirAvoWeappon, int[] E_cirAviEtc,
                      Define.AttackType E_type, int E_defence, int E_Mdefence, int _damage, int[] _damWeapon, int[] _damEtc)
    {
        int damage = FinalDamage(_tec, _luck, _accWeapon, _accEtc, E_accSpeed, E_luck, E_accAvoWeapon, E_accAvoEtc,
                      _cirWeappon, _criEtc, E_cirAvoWeappon, E_cirAviEtc,
                      E_type, E_defence, E_Mdefence, _damage, _damWeapon, _damEtc);
        if (damage >= E_curHp)
            damage = E_curHp; //����� ���� ü�º��� ������ ���ݷ��� ���� ü�°� ����. 

        #region DebugZone
        //Debug.Log($"���� ������ : {damage}, ���� ü�� : {E_curHp - damage}");
        //Debug.Log($"���ݷ� : {AttackDamage(E_type, E_defence, E_Mdefence, _damage, _damWeapon, _damEtc)}");

        //Debug.Log($"�ʻ� ������ : {CriticalCalculate(_tec, _cirWeappon, _criEtc, E_luck, E_cirAvoWeappon, E_cirAviEtc)}%");
        //Debug.Log($"�Ʊ� �ʻ� : {CriticalPercentage(_tec, _cirWeappon, _criEtc)}, ����ʻ� ȸ�� : {CriticalAvoidPercentage(E_luck, E_cirAvoWeappon, E_cirAviEtc)}");

        //Debug.Log($"���� ������ : {AccuracyCalculate(_tec, _luck, _accWeapon, _accEtc, E_accSpeed, E_luck, E_accAvoWeapon, E_accAvoEtc)}%");
        //Debug.Log($"�Ʊ� ���� : {AccuracyPercentage(_tec, _luck, _accWeapon, _accEtc)}, ��� ȸ�� Ȯ�� : {AvoidPercentage(E_accSpeed, E_luck, E_accAvoWeapon, E_accAvoEtc)}");
        #endregion

        return Mathf.FloorToInt(damage);
    }

    // <summary>
    // ����� ���� ü�°� ������� ������ ������ (���߽� ���� else ȸ��)
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
    // ���� �ʻ� ���� ������ ��� (ġ��Ÿ�� * 3)
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
    // ���� ���� ��� (���� - ����or����)
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
    // ���� Ÿ�� ��� (�ٷ�or����)
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
    // ���� ��� (�ٷ�or���� + �������� + ��Ÿ)
    // </summary>
    public int AttackDamageCalculate(int _damage, int[] _weapon, int[] _etc)
    {
        return Mathf.FloorToInt(_damage + WeaponEffect(_weapon) + EtcEffect(_etc));
    }

    // <summary>
    // ���� ��� (����Ȯ�� - ȸ��Ȯ��)
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
    // ���� Ȯ�� (���*2 + ���/2 + ���� + ��Ÿ)
    // </summary>
    public int AccuracyPercentage(int _tec, int _luck, int[] _weapon, int[] _etc)
    {
        return Mathf.FloorToInt((_tec * 2) + Mathf.FloorToInt(_luck / 2) + WeaponEffect(_weapon) + EtcEffect(_etc));
    }

    // <summary>
    // ȸ�� Ȯ�� (�ӵ�*2 + ���/2 + ���� + ��Ÿ ) //E
    // </summary>
    public int AvoidPercentage(int _speed, int _luck, int[] _weapon, int[] _etc)
    {
        return Mathf.FloorToInt((_speed * 2) + Mathf.FloorToInt(_luck / 2) + WeaponEffect(_weapon) + EtcEffect(_etc));
    }

    // <summary>
    // �ʻ� ��� (�ʻ�Ȯ�� - �ʻ�ȸ��Ȯ��)
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
    // �ʻ� Ȯ�� (���/2 + ���� + ��Ÿ)
    // </summary>
    public int CriticalPercentage(int _tec, int[] _weappon, int[] _etc)
    {
        return Mathf.FloorToInt(Mathf.FloorToInt(_tec / 2) + WeaponEffect(_weappon) + EtcEffect(_etc));
    }

    // <summary>
    // �ʻ� ȸ�� Ȯ�� (��� + ���� + ��Ÿ) //E
    // </summary>
    public int CriticalAvoidPercentage(int _luck, int[] _weappon, int[] _etc)
    {
        return Mathf.FloorToInt(_luck + WeaponEffect(_weappon) + EtcEffect(_etc));
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
