using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingSlotBase : BaseInventory
{
    public CharSelSlot[] slot;

    private void Start()
    {
        slot = GetComponentsInChildren<CharSelSlot>();
    }
}
