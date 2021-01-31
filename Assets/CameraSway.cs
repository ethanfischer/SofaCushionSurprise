using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSway : MonoBehaviour
{
    public float offsetZ = -6.01f;
    public float swayAmount = 0.02f;
    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        var newpos = transform.position;
        newpos.z = (Mathf.Sin(Time.time * speed) * swayAmount) + offsetZ;

        transform.position = newpos;
    }
}
