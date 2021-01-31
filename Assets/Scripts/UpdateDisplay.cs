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
    public TextMeshProUGUI batteryCountPhone;
    public TextMeshProUGUI diamondCount;
    public TextMeshProUGUI diamondTotalValueText;
    public int diamondSellValue;
    int diamondTotalValueInt = 0;
    public GameObject phoneDisplay;

    //Audio Variables
    public AudioSource audioSource;
    public AudioClip phoneDisplayAC;
    public AudioClip buySellAC;
    public AudioClip buySellFailAC;

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
        batteryCountPhone.text = batteryCount.text = itemStates.batteryCount.ToString();
    }

    void DiamondCountUpdate()
    {
        diamondCount.text = itemStates.diamondCount.ToString();
        diamondTotalValueInt = itemStates.diamondCount * diamondSellValue;
        diamondTotalValueText.text = diamondTotalValueInt.ToString();
    }

    public void SellDiamonds()
    {
        if (itemStates.diamondCount == 0)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(buySellFailAC);
        }
        else
        {
            int diamondDecreaseAmount = itemStates.diamondCount;
            itemStates.IncrementItemCount(-diamondDecreaseAmount, ItemType.Diamond);
            itemStates.IncrementItemCount(diamondTotalValueInt, ItemType.Coin);
            audioSource.Stop();
            audioSource.PlayOneShot(buySellAC);
        }        
    }

    public void BuyBatteries()
    {
        if (itemStates.coinCount >= 100)
        {
            itemStates.IncrementItemCount(1, ItemType.Battery);
            itemStates.IncrementItemCount(-100, ItemType.Coin);
            audioSource.Stop();
            audioSource.PlayOneShot(buySellAC);
        }
        else
        {
            audioSource.Stop();
            audioSource.PlayOneShot(buySellFailAC);
        }
        
    }

    public void OpenClosePhoneDisplay()
    {
        phoneDisplay.SetActive(!phoneDisplay.activeSelf);
        audioSource.Stop();
        audioSource.PlayOneShot(phoneDisplayAC);
    }
}
