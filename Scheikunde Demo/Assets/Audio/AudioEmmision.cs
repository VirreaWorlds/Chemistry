using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEmmision : MonoBehaviour
{
    public AudioSource source;
    public bool onCollision;

    public void PlaySpecificSound()
    {
        source.Play();
    }

    public void TurnOff()
    {
        source.Stop();
    }

    public void TurnOn()
    {
        source.Play();
    }
}