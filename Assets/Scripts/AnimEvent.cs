using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{

    public SpawnObject spawnObject;

    public void AnimationEvent()
    {
        spawnObject.ObjectSpawn();
    }
}
