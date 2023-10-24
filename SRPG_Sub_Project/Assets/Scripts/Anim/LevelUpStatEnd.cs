using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpStatEnd : MonoBehaviour
{
    public void End()
    {
         GameObject parentObject = transform.parent.gameObject;
         parentObject.SetActive(false);
    }
}
