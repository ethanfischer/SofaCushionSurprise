using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{

    public GameObject spawnContainer;
    public GameObject spawnPoint;
    Vector3 spawnLocation;

    private void Start()
    {
        spawnLocation = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z);
    }

    private void OnMouseUp()
    {
        //Destroy any existing GOs before spawning a new one
        GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag("GO Container");
        foreach(GameObject go in spawnedObjects)
        {
            Destroy(go);
        }
        //Spawn the new object
        Instantiate(spawnContainer, spawnLocation, Quaternion.identity);
    }
}
