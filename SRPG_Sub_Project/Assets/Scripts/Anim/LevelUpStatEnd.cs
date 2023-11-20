using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpStatEnd : MonoBehaviour
{
    // <summary>
    // 레벨업 popup 실행시, 재생되는 애니메이션
    // </summary>
    public void End()
    {
         GameObject parentObject = transform.parent.gameObject;
         parentObject.SetActive(false);
    }
}
