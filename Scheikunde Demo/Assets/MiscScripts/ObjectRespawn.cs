using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespawn : MonoBehaviour
{
    public Vector3 position;
    public Quaternion rotation;
    public float timeBeforeRespawn = 10.0f;
    public float t;

    private void Awake()
    {
        position = transform.position;
        rotation = transform.rotation;
    }

    private void Update()
    {
        RespawnCheckLoop();
    }

    public void RespawnCheckLoop()
    {
        if (transform.position.y <= 0.4f) //around leg height
        {
            t += Time.deltaTime;
        }
        else
        {
            t = 0;
        }

        if (t >= timeBeforeRespawn)
        {
            transform.position = position;
            transform.rotation = rotation;
            t = 0;

            if (GetComponent<Rigidbody>())
            {
                var r = GetComponent<Rigidbody>();

                r.velocity = Vector3.zero;
                r.angularVelocity = Vector3.zero;
            }
        }
    }
}
