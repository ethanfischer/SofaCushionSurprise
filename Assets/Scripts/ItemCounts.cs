using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCounts : MonoBehaviour
{
    public int coinCount { get; private set; } = 0;
    public int batteryCount { get; private set; }  = 0;
    public int diamondCount { get; private set; }  = 0;

    public bool hasGum { get; private set; } = false;


    public void IncrementItemCount(int amount, ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Coin:
                coinCount += amount;
                break;
            case ItemType.Battery:
                batteryCount += amount;
                break;
            case ItemType.Diamond:
                diamondCount += amount;
                break;
            default:
                break;
        }
        itemType += amount;
    }

}
