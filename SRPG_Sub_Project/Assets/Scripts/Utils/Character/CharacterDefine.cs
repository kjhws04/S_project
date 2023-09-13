using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDefine : MonoBehaviour
{
    // <surmmary>
    // 캐릭터의 성별
    // </surmmary>
    public enum Gender
    {
        Unknown,
        Male,
        Female
    }

    // <surmmary>
    // 캐릭터의 종족
    // </surmmary>
    public enum Tribe
    {
        Unknown,
        Human,
        Half,
        Balaur, //용족
        Demon
    }

    // <surmmary>
    // 캐릭터의 소속
    // </surmmary>
    public enum Belong
    {
        Unknown,
        School
    }

    // <surmmary>
    // 캐릭터의 등급
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
