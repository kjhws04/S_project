using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDefine : MonoBehaviour
{
    // <surmmary>
    // ĳ������ ����
    // </surmmary>
    public enum Gender
    {
        Unknown,
        Male,
        Female
    }

    // <surmmary>
    // ĳ������ ����
    // </surmmary>
    public enum Tribe
    {
        Unknown,
        Human,
        Half,
        Balaur, //����
        Demon
    }

    // <surmmary>
    // ĳ������ �Ҽ�
    // </surmmary>
    public enum Belong
    {
        Unknown,
        School
    }

    // <surmmary>
    // ĳ������ ���
    // </surmmary>
    public enum Rating
    {
        Unknown,
        S1,
        S2,
        S3,
        S4,
        S5,
    }
}
