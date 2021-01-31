using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedItemManager : MonoBehaviour
{
    public GameObject Phone;
    public GameObject Diamond;
    public GameObject Vacuum;

    public GameObject VacuumIcon;
    public GameObject PhoneIcon;
    public GameObject CoinsIcon;
    public GameObject BatteriesIcon;
    public GameObject GumIcon;
    public GameObject MagnetIcon;

    public int RequiredClicksBetweenDiscoveries = 0;

    private int scriptCounter = 0;
    public string[] scriptedItems =
    {
        nameof(ItemType.Diamond),
        nameof(ItemType.Phone),
        nameof(ItemType.Vacuum)
    };

    private ItemStates itemStates;
    private ClickCounter clickCounter;
    private int previousDiscoveryClick = 0;

    private void Start()
    {
        itemStates = GameObject.FindObjectOfType<ItemStates>();
        clickCounter = GameObject.FindObjectOfType<ClickCounter>();
    }

    public GameObject GetNextScriptedItem()
    {
        if (ShouldPreventNextDiscovery()) return null; 

        GameObject result;
        var item = scriptedItems[scriptCounter];

        switch (item)
        {
            case nameof(ItemType.Diamond):
                result = DiscoverDiamond();
                break;
            case nameof(ItemType.Phone):
                result = DiscoverPhone();
                break;
            case nameof(ItemType.Vacuum):
                result = DiscoverVacuum();
                break;
            default:
                result = null;
                break;
        }

        previousDiscoveryClick = clickCounter.clickCount;
        scriptCounter++;
        return result;
    }

    private bool ShouldPreventNextDiscovery()
    {
        var didDiscoverTooSoon = clickCounter.clickCount <= previousDiscoveryClick + RequiredClicksBetweenDiscoveries;
        if(didDiscoverTooSoon)
        {
            Debug.Log("Next discovery happened too soon, skipping");
        }

        var hasDiscoveredLastScriptedItem = scriptCounter >= scriptedItems.Length;
        if(hasDiscoveredLastScriptedItem)
        {
            Debug.Log("Player has discovered all the items");
        }

        return didDiscoverTooSoon || hasDiscoveredLastScriptedItem;
    }

    private GameObject DiscoverPhone()
    {
        itemStates.DiscoverItem(ItemType.Phone);
        PhoneIcon.SetActive(true); 
        return Phone;
    }

    private GameObject DiscoverDiamond()
    {
        itemStates.DiscoverItem(ItemType.Diamond);
        itemStates.IncrementItemCount(1, ItemType.Diamond);
        return Diamond;
    }

    private GameObject DiscoverVacuum()
    {
        itemStates.DiscoverItem(ItemType.Vacuum);
        itemStates.isVacuumEquipped = true; //TODO remove this once we make the vacuum button work
        VacuumIcon.SetActive(true); 
        return Vacuum;
    }
}
