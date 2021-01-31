using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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


    private int scriptCounter = 0;
    public string[] scriptedItems =
    {
        nameof(ItemType.Diamond),
        nameof(ItemType.Phone),
        nameof(ItemType.Vacuum)
    };

    private ItemStates itemStates;
    private ClickCounter clickCounter;
    private Randomizer randomizer;
    private int previousDiscoveryClick = 0;

    private int[] initialDistributionFromEditor;
    private int initialRequiredClicksBetweenDiscoveriesFromEditor;

    private void Start()
    {
        itemStates = GameObject.FindObjectOfType<ItemStates>();
        clickCounter = GameObject.FindObjectOfType<ClickCounter>();
        randomizer = GameObject.FindObjectOfType<Randomizer>();

        initialDistributionFromEditor = new int[randomizer.Distributions.Length];
        Array.Copy(randomizer.Distributions, initialDistributionFromEditor, randomizer.Distributions.Length);
        initialRequiredClicksBetweenDiscoveriesFromEditor = randomizer.RequiredClicksBetweenDiscoveries;
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
        var didDiscoverTooSoon = clickCounter.clickCount <= previousDiscoveryClick + randomizer.RequiredClicksBetweenDiscoveries;
        if (didDiscoverTooSoon)
        {
            Debug.Log("Next discovery happened too soon, skipping");
            GameObject.FindGameObjectWithTag("GO Container").GetComponent<FoundObject>().GetNothing();
        }

        var hasDiscoveredLastScriptedItem = scriptCounter >= scriptedItems.Length;
        if (hasDiscoveredLastScriptedItem)
        {
            Debug.Log("Player has discovered all the items");
            GameObject.FindGameObjectWithTag("GO Container").GetComponent<FoundObject>().GetNothing();
        }

        return didDiscoverTooSoon || hasDiscoveredLastScriptedItem;
    }

    private GameObject DiscoverPhone()
    {
        itemStates.DiscoverItem(ItemType.Phone);
        PhoneIcon.SetActive(true);

        Array.Copy(initialDistributionFromEditor, randomizer.Distributions, initialDistributionFromEditor.Length); //put distribution back pre-diamond 
        randomizer.Distributions[4] = 5; //make diamond discoverable now
        randomizer.RequiredClicksBetweenDiscoveries = initialRequiredClicksBetweenDiscoveriesFromEditor; //put required clicks back pre-diamond
        return Phone;
    }

    private GameObject DiscoverDiamond()
    {
        itemStates.DiscoverItem(ItemType.Diamond);
        itemStates.IncrementItemCount(1, ItemType.Diamond);

        //make it so next click discovers the phone
        randomizer.Distributions = new int[]
        { 
            1,
            0,
            0,
            0,
            0
        };

        //make it so we don't prevent discovering diamond on next click
        randomizer.RequiredClicksBetweenDiscoveries = 0;

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
