using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // <surmmary>
    // 주무기
    // </surmmary>
    public enum MainWeapon
    {
        None
    }

    // <surmmary>
    // 보조무기
    // </surmmary>
    public enum SubWeapon
    {
        None
    }

    // <surmmary>
    // 무기 타입
    // </surmmary>
    public enum WeaponType
    {
        AD,
        AP
    }
}
