using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class : MonoBehaviour
{
    // <surmmary>
    // ĳ������ ���⺰ Ŭ����
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
    // ĳ������ ���⺰ Ŭ����
    // </surmmary>
    public enum VehicleClass
    {
        Unknown,
        Infantry, //����
        Cavalry, //�⺴
        Fly, //��
    }

    // <surmmary>
    // ĳ������ Ŭ����
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
