using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateDisplay : MonoBehaviour
{

    public ItemStates itemStates;
    public TextMeshProUGUI coinCount;
    public TextMeshProUGUI batteryCount;

    // Update is called once per frame
    void Update()
    {
        //I think all of these shoud be called when there is reason to update the display instead of here
        CoinCountUpdate();
        BatteryCountUpdate();
    }

    void CoinCountUpdate()
    {
        coinCount.text = itemStates.coinCount.ToString();
    }

    void BatteryCountUpdate()
    {
        batteryCount.text = itemStates.batteryCount.ToString();
    }
}
