using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStates : MonoBehaviour
{
    public int coinCount { get; private set; } = 0;
    public int batteryCount { get; private set; }  = 0;
    public int diamondCount { get; private set; }  = 0;

    public bool discoveredGum { get; private set; } = false;
    public bool discoveredPhone { get; private set; } = false;
    public bool discoveredVacuum { get; private set; } = false;
    public bool discoveredDiamond { get; private set; } = false;

    private bool _isVacuumEquipped;
    public bool isVacuumEquipped
    {
        get { return _isVacuumEquipped; }
        set
        {
            if(value)
            {
                VacuumHandReplacement.SetActive(true);
                HandModel.SetActive(false);
            }
            else
            {
                VacuumHandReplacement.SetActive(false);
                HandModel.SetActive(true);
            }

            _isVacuumEquipped = value;
        }
    }
    public GameObject HandModel;
    public GameObject VacuumHandReplacement;

    public GameObject notifyPanel;

    public void IncrementItemCount(int amount, ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Coin:
                coinCount += amount;
                break;
            case ItemType.Battery:
                batteryCount += amount;
                if (batteryCount < 0) batteryCount = 0;
                break;
            case ItemType.Diamond:
                diamondCount += amount;
                break;
            default:
                Debug.LogError($"Item type {itemType} can't be incremented");
                break;
        }
    }

    public void DiscoverItem(ItemType itemType)
    {
        switch(itemType)
        {
            case ItemType.Vacuum:
                discoveredVacuum = true;
                break;
            case ItemType.Phone:
                discoveredPhone = true;
                break;
            case ItemType.Diamond:
                discoveredDiamond = true;
                break;
            case ItemType.Gum:
                discoveredGum = true;
                break;
            default:
                Debug.LogError($"Can't enable item type {itemType}");
                break;
        }
    }

}
