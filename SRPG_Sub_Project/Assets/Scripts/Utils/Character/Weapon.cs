using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <surmmary>
// 캐릭터의 착용 무기 상태를 정의 하는 Define class
// </surmmary>
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
