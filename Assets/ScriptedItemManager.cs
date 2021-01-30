using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedItemManager : MonoBehaviour
{
    public GameObject Phone;
    public GameObject Diamond;
    public GameObject Vacuum;

    private int counter = 0;
    public string[] scriptedItems =
    {
        nameof(ItemType.Diamond),
        nameof(ItemType.Phone),
        nameof(ItemType.Vacuum)
    };

    private ItemStates itemStates;

    private void Start()
    {
        itemStates = GameObject.FindObjectOfType<ItemStates>();
    }

    public GameObject GetNextScriptedItem()
    {
        GameObject result;
        var item = scriptedItems[counter];
        switch (item)
        {
            case nameof(ItemType.Diamond):
                result = GetDiamond();
                break;
            case nameof(ItemType.Phone):
                result = GetPhone();
                break;
            case nameof(ItemType.Vacuum):
                result = GetVacuum();
                break;
            default:
                result = null;
                break;
        }

        counter++;
        return result;
    }

    private GameObject GetPhone()
    {
        itemStates.DiscoverItem(ItemType.Phone);
        return Phone;
    }

    private GameObject GetDiamond()
    {
        return Diamond;
    }

    private GameObject GetVacuum()
    {
        return Vacuum;
    }
}
