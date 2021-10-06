using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFull : MonoBehaviour
{

    public bool Voll;

    [SerializeField] private Transform dezeParent;
    public GrabStof parentGrabStof;

    public GameObject inhoudPrefab;

    public AudioSource buttonClickAudio;

    public bool animationTrigger;
    bool b;


    void Start()
    {
        dezeParent = transform.parent;
        parentGrabStof = dezeParent.GetComponent<GrabStof>();
    }

    // Update is called once per frame
    void Update()
    {
        if(animationTrigger != b)
        {
            if (animationTrigger) DeleteStof();
        }

        try
        {
            var oplepel = dezeParent.GetChild(2);
            Voll = true;
        }
        catch 
        {
            Voll = false;
        }

        b = animationTrigger;

        if(parentGrabStof.UpsideDown())
        {
            if (Voll)
            {
                DeleteStof();
            }
        }
    }


    public void DeleteStof()
    {
        buttonClickAudio.Play();

        if (Voll)
        {
            if(inhoudPrefab != null)
            {
                GameObject g = Instantiate(inhoudPrefab, transform, true);

                g.GetComponent<Droplet>().stof = dezeParent.GetChild(2).GetComponent<StofHolder>().stofinformatiehouder;
                g.transform.SetParent(null);
                g.transform.position = transform.position;
            }

            //Debug.Log("Destroy");
            Destroy(dezeParent.GetChild(2).gameObject);
        }


    }
}
