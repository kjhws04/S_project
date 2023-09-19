using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "New Weapon/Weapon")]
public class WeaponStat : ScriptableObject
{
    public string weaponName; //���� �̸�
    public Class.WeaponClass weaponType; //���� ����
    public Weapon.WeaponType weaponAttackType; //AD, AP
    public int increaseVal = 0; //���� ��ġ
    public int rank; //���� ���
    public bool isUsed = false; //���� ������� ��������

    public Sprite weaponCardImg;
}
