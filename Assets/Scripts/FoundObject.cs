//using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class FoundObject : MonoBehaviour
{
    //Variables for motion
    public float rotateSpeed;
    public float moveSpeed;
    Vector3 targetPosition;
    private ItemStates itemStates;
    private Randomizer randomizer;
    private ScriptedItemManager scriptedItemManager;
    private ClickCounter clickCounter;

    //Spawn Objects variables
    public GameObject coinObject;
    public GameObject diamondObject;
    public GameObject batteryObject;
    public GameObject gumObject;

    //Audio Variables
    public AudioSource audioSource;
    public AudioClip foundNothingAC;
    public AudioClip vacuumClip;

    //Text Variables
    //public GameObject notifyPanel;
    //public TextMeshProUGUI notifyText;

    void Start()
    {
        itemStates = GameObject.FindObjectOfType<ItemStates>();
        randomizer = GameObject.FindObjectOfType<Randomizer>();
        scriptedItemManager = GameObject.FindObjectOfType<ScriptedItemManager>();
        clickCounter = GameObject.FindObjectOfType<ClickCounter>();

        //Set position and destination
        targetPosition = transform.position;
        targetPosition.y = transform.position.y + 1;  //Moves object 1 unit up

        switch (randomizer.ChooseItem())
        {
            case nameof(ItemType.Nothing):
                GetNothing();
                break;
            case nameof(ItemType.Coin):
                GetCoin();
                break;
            case nameof(ItemType.Battery):
                GetBattery();
                break;
            case nameof(ItemType.Diamond):
                GetDiamond();
                break;
            case nameof(ItemType.ScriptedItem):
                GetScriptedItem();
                break;
            default:
                break;
        }

        clickCounter.IncrementClickCount();
    }

    private void GetScriptedItem()
    {
        var scriptedItem = scriptedItemManager.GetNextScriptedItem();
        if (scriptedItem == null) return;

        SetChildToParent(scriptedItem);
        Debug.Log($"Got scripted item: {scriptedItem.name}");
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    void SetChildToParent(GameObject child) //When instantiating a GO, we need to make it a child of this object so thit it will move
    {
        GameObject childGO;
        childGO = Instantiate(child, transform.position, Quaternion.Euler(90, 0, 0));  //Coin needs to be rotated 90� - This may need to be changed when objects are modelled by Tanya
        childGO.transform.SetParent(this.transform);
    }

    public void GetNothing()
    {
        var r = Random.Range(0, 3);
        if (r == 1)
        {
            GetGum();
            return;
        }

        Debug.Log("Got Nothing");
        audioSource.Stop();
        audioSource.PlayOneShot(foundNothingAC);
    }

    private void GetGum()
    {
        SetChildToParent(gumObject);
        Debug.Log("Got Gum");
        audioSource.Stop();
        audioSource.PlayOneShot(foundNothingAC);
    }

    void GetCoin()
    {
        scriptedItemManager.CoinsIcon.SetActive(true);
        //Determine how much money is found
        //Display text stating how much money is found
        //Add money to total
        //spawn coin gameobject
        SetChildToParent(coinObject);  //Instantiates object
        var amount = GetCoinAmount();
        //DisplayCoinAmount(amount);
        StartCoroutine(DisplayCoinNotify(amount));
        itemStates.IncrementItemCount(amount, ItemType.Coin);
        Debug.Log($"Got {amount} Coin(s)");
    }

    void DisplayCoinAmount(int amount)
    {
        //Notification
        //itemStates.notifyPanel.SetActive(true);
        //GameObject.FindGameObjectWithTag("Notification Text").GetComponent<TextMeshProUGUI>().text = amount.ToString() + " coins";
        //notifyPanel.SetActive(false);
        //notifyText.GetComponent<TextMeshProUGUI>().text = amount.ToString();

        string amountTotalText = amount.ToString() + " coins";
    }

    private int GetCoinAmount()
    {
        var isVaccumEquippedAndCharged = itemStates.discoveredVacuum && itemStates.isVacuumEquipped && itemStates.batteryCount > 0;
        if (isVaccumEquippedAndCharged)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(vacuumClip);
            itemStates.IncrementItemCount(-1, ItemType.Battery); //use a battery each time the vacuum is enabled
            return Random.Range(1, randomizer.MaxCoinsVacuum);
        }
        else
        {
            return Random.Range(1, randomizer.MaxCoinsNormal);
        }
    }

    void GetBattery()
    {
        scriptedItemManager.BatteriesIcon.SetActive(true);
        Debug.Log("Got Battery");
        SetChildToParent(batteryObject);
        itemStates.IncrementItemCount(1, ItemType.Battery);
    }

    void GetDiamond()
    {
        if (!itemStates.discoveredDiamond)
        {
            Debug.Log("Haven't discovered diamond through scriptedItem yet, skipping click");
            GetNothing();
            return;
        }
        //Display text stating that 1 diamond was found
        //Add diamond to inventory
        //Spawn Diamond gameobject
        Debug.Log("Got Diamond");
        SetChildToParent(diamondObject);
        itemStates.IncrementItemCount(1, ItemType.Diamond);
    }

    IEnumerator DisplayCoinNotify(int amount)
    {
        yield return new WaitForSeconds(0.5f);
        itemStates.notifyPanel.SetActive(true);
        GameObject.FindGameObjectWithTag("Notification Text").GetComponent<TextMeshProUGUI>().text = amount.ToString() + " coins";
    }
}
