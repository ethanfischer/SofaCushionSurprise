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
    public TextMeshProUGUI diamondCount;
    public TextMeshProUGUI diamondTotalValueText;
    public int diamondSellValue;
    int diamondTotalValueInt = 0;
    public GameObject phoneDisplay;

    // Update is called once per frame
    void Update()
    {
        //I think all of these shoud be called when there is reason to update the display instead of here
        CoinCountUpdate();
        BatteryCountUpdate();
        DiamondCountUpdate();
    }

    void CoinCountUpdate()
    {
        coinCount.text = itemStates.coinCount.ToString();
    }

    void BatteryCountUpdate()
    {
        batteryCount.text = itemStates.batteryCount.ToString();
    }

    void DiamondCountUpdate()
    {
        diamondCount.text = itemStates.diamondCount.ToString();
        diamondTotalValueInt = itemStates.diamondCount * diamondSellValue;
        diamondTotalValueText.text = diamondTotalValueInt.ToString();
    }

    public void SellDiamonds()
    {
        int diamondDecreaseAmount = itemStates.diamondCount;
        itemStates.IncrementItemCount(-diamondDecreaseAmount, ItemType.Diamond);
        itemStates.IncrementItemCount(diamondTotalValueInt, ItemType.Coin);
    }

    public void OpenClosePhoneDisplay()
    {
        phoneDisplay.SetActive(!phoneDisplay.activeSelf);
    }
}
