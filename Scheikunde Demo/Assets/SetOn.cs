using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOn : MonoBehaviour
{

    [SerializeField] private bool IsOn;

    public Fire fire;

    [SerializeField] private GameObject Child;
    [SerializeField] private GameObject Burner;

    [SerializeField] private ParticleSystem Branderprt;
    [SerializeField] private FlameManager flameMan;
    private ParticleSystem.EmissionModule Bem;

    public AudioSource burnerAudio;
    public AudioSource toggleAudio;

    [Space]
    bool b;
    public bool isAnimatedBurner;


    private void Start()
    {
        Burner = this.gameObject;
        Child = transform.GetChild(0).gameObject;
        Branderprt = Burner.GetComponent<ParticleSystem>();
        Bem = Branderprt.emission;

        fire = Child.GetComponent<Fire>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (IsOn)
        {
            Child.GetComponent<Fire>().CanDo = true;
            flameMan.isOn = true;
            Bem.enabled = true;

            //set the emission of child on and activate fire script
        } else
        {
            Child.GetComponent<Fire>().CanDo = false;
            flameMan.isOn = false;
            Bem.enabled = false;
        }


        if (Input.GetKeyDown(KeyCode.F6))
        {
            DoThis();
        }

        if(isAnimatedBurner && IsOn != b)
        {
            AnimatedDoThis();
        }

        b = IsOn;
    }

    public void DoThis()
    {
        IsOn = !IsOn;

        toggleAudio.Stop();
        toggleAudio.Play();

        if (IsOn)
        {
            QuestManager.instance.SendObjectiveInputToQuests(Objectives.TurnFireOn); //Sends a quest objective//
            burnerAudio.Play();
        }
        else
        {
            burnerAudio.Stop();
        }
    }

    public void AnimatedDoThis()
    {
        toggleAudio.Stop();
        toggleAudio.Play();

        if (IsOn)
        {
            burnerAudio.Play();
        }
        else
        {
            burnerAudio.Stop();
        }
    }
}
