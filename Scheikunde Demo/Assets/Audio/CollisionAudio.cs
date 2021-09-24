using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CollisionAudio : MonoBehaviour
{
    public SoundType type;
    public Rigidbody r;
    public float velocity;

    private void OnCollisionEnter(Collision collision)
    {
        AudioManager.instance.PlaceSoundAt(type, transform.position, 0.2f);

        velocity = r.velocity.magnitude;
    }
}
