using System;
using UnityEngine;

public class FoundObject : MonoBehaviour
{
    //Variables for motion
    public float rotateSpeed;
    public float moveSpeed;
    Vector3 targetPosition;
    private ItemStates itemStates;
    private Randomizer randomizer;
    private ScriptedItemManager scriptedItemManager;

    //Spawn Objects variables
    public GameObject coinObject;
    public GameObject diamondObject;
    public GameObject batteryObject;

    //Money/Coin variables
    public int minCoins;
    public int maxCoins;

    void Start()
    {
        itemStates = GameObject.FindObjectOfType<ItemStates>();
        randomizer = GameObject.FindObjectOfType<Randomizer>();
        scriptedItemManager = GameObject.FindObjectOfType<ScriptedItemManager>();

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
            case nameof(ItemType.Vacuum):
                GetDiamond();
                break;
            case nameof(ItemType.ScriptedItem):
                GetScriptedItem();
                break;
            default:
                break;
        }
    }

    private void GetScriptedItem()
    {
        var scriptedItem = scriptedItemManager.GetNextScriptedItem();
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
        childGO = Instantiate(child, transform.position, Quaternion.Euler(90, 0, 0));  //Coin needs to be rotated 90° - This may need to be changed when objects are modelled by Tanya
        childGO.transform.SetParent(this.transform);
    }

    void GetNothing()
    {
        Debug.Log("Got Nothing");
    }

    void GetCoin()
    {
        //Determine how much money is found
        //Display text stating how much money is found
        //Add money to total
        //spawn coin gameobject
        Debug.Log("Got Coin");
        SetChildToParent(coinObject);  //Instantiates object
        itemStates.IncrementItemCount(1, ItemType.Coin);
    }

    void GetBattery()
    {
        Debug.Log("Got Battery");
        SetChildToParent(batteryObject);
        itemStates.IncrementItemCount(1, ItemType.Battery);
    }

    void GetDiamond()
    {
        //Display text stating that 1 diamond was found
        //Add diamond to inventory
        //Spawn Diamond gameobject
        Debug.Log("Got Diamond");
        SetChildToParent(diamondObject);
        itemStates.IncrementItemCount(1, ItemType.Diamond);
    }
}
