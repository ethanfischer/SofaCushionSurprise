using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnObject : MonoBehaviour
{

    public GameObject spawnContainer;
    public GameObject spawnPoint;
    Vector3 spawnLocation;

    public GameObject notifyPanel;

    public Animator armReach;

    private void Start()
    {
        spawnLocation = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z);
    }

    private void OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        //Destroy any existing GOs before spawning a new one
        GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag("GO Container");
        foreach (GameObject go in spawnedObjects)
        {
            Destroy(go);
        }
        armReach.SetBool("isReaching", true);
        notifyPanel.SetActive(false);
    }

    public void ObjectSpawn()
    {
        armReach.SetBool("isReaching", false);
        //Spawn the new object
        Instantiate(spawnContainer, spawnLocation, Quaternion.identity);
    }
}
