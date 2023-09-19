using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "New Weapon/Weapon")]
public class WeaponStat : ScriptableObject
{
    public string weaponName; //무기 이름
    public Class.WeaponClass weaponType; //무기 종류
    public Weapon.WeaponType weaponAttackType; //AD, AP
    public int increaseVal = 0; //증감 수치
    public int rank; //무기 등급
    public bool isUsed = false; //현재 사용중인 무기인지

    public Sprite weaponCardImg;
}
