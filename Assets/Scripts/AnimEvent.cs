using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{

    public SpawnObject spawnObject;
    public Animator sofaCushion;

    public void AnimationEvent()
    {
        spawnObject.ObjectSpawn();
    }

    public void AnimateCushion()
    {
        sofaCushion.SetBool("isMoving", !sofaCushion.GetBool("isMoving"));
    }
}
