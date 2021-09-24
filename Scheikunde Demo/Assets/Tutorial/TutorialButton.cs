using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    public TutorialManager tutMan;
    public int id;
    public AudioSource buttonSound;

    private void OnTriggerEnter(Collider other)
    {
        tutMan.StartSpecific(id);
        buttonSound.Play();
    }
}
