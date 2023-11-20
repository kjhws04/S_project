using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <surmmary>
// 캐릭터의 스킬 상태를 정의 하는 Define class
// </surmmary>
public class Skill : MonoBehaviour
{
    // <surmmary>
    // 캐릭터의 패시브 스킬
    // </surmmary>
    public enum PassiveSkill
    {
        Unknown
    }

    // <surmmary>
    // 캐릭터의 장착 스킬
    // </surmmary>
    public enum EquipSkill
    {
        None,
        Unknown
    }

    // <surmmary>
    // 캐릭터의 사용 스킬
    // </surmmary>
    public enum ActiveSkill
    {
        None,
        Unknown
    }
}
