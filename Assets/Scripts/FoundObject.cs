using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundObject : MonoBehaviour
{

    //Variables for motion
    public float rotateSpeed;
    public float moveSpeed;
    Vector3 targetPosition;

    //Variables for chance/percentage to find objects
    float randomValue;

    //Spawn Objects variables
    public GameObject coinObject;
    public GameObject diamondObject;
    public GameObject batteryObject;

    //Money/Coin variables
    public int minCoins;
    public int maxCoins;
 

    void Start()
    {
        //Set position and destination
        targetPosition = transform.position;
        targetPosition.y = transform.position.y + 1;  //Moves object 1 unit up

        //Determine what object will be found
        randomValue = Random.value;  //numbers match change. ie: 0.3 = 30%
        //Smallest chance goes first and on through the line...
        if(randomValue < 0.2f)
        {
            GetDiamond();
        }
        else if (randomValue < 0.4f)
        {
            //Get battery?
            GetBattery();
        }
        else  //last bit is money
        {
            GetCoin();
        }

        //Do a thing based specifically on the object found. ie: Add money to total or add battery to inventory...
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

    void GetCoin()
    {
        //Determine how much money is found
        //Display text stating how much money is found
        //Add money to total
        //spawn coin gameobject
        Debug.Log("Got Coin");
        SetChildToParent(coinObject);  //Instantiates object

        
    }

    void GetBattery()
    {
        Debug.Log("Got Battery");
        SetChildToParent(batteryObject);
    }

    void GetDiamond()
    {
        //Display text stating that 1 diamond was found
        //Add diamond to inventory
        //Spawn Diamond gameobject
        Debug.Log("Got Diamond");
        SetChildToParent(diamondObject);
    }
}
