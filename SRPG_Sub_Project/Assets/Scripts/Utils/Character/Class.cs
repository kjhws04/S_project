using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <surmmary>
// 캐릭터의 무기, 직업, 기승 상태를 정의 하는 Define class
// </surmmary>
public class Class : MonoBehaviour
{
    // <surmmary>
    // 캐릭터의 무기별 클래스
    // </surmmary>
    public enum WeaponClass
    {
        Unknown,
        Sword,
        Spear,
        Ax,
        Bow,
        Pistol,
        Staff,
        Dagger,
        Shield,
    }

    // <surmmary>
    // 기승 상태별 클래스
    // </surmmary>
    public enum VehicleClass
    {
        Unknown,
        Infantry, //보병
        Cavalry, //기병
        Fly, //비병
    }

    // <surmmary>
    // 캐릭터의 클래스
    // </surmmary>
    public enum CurClass
    {
        Unknown,
        Cityzen,
        Swrodsman,
        Lancer,
        Woodcutter,
        Acrcher,
        Assassin,
        Wizard,
        Defencer
    }
}
