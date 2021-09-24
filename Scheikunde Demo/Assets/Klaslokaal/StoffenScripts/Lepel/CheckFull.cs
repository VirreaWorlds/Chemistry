using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFull : MonoBehaviour
{

    public bool Voll;

    [SerializeField] private Transform dezeParent;

    public GameObject inhoudPrefab;

    public AudioSource buttonClickAudio;

    public bool animationTrigger;
    bool b;


    void Start()
    {
        dezeParent = transform.parent;
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
            var oplepel = dezeParent.GetChild(1);
            Voll = true;
        }
        catch 
        {
            Voll = false;
        }

        b = animationTrigger;
    }


    public void DeleteStof()
    {
        buttonClickAudio.Play();

        if (Voll)
        {
            if(inhoudPrefab != null)
            {
                GameObject g = Instantiate(inhoudPrefab, transform, true);

                g.GetComponent<Droplet>().stof = dezeParent.GetChild(1).GetComponent<StofHolder>().stofinformatiehouder;
                g.transform.SetParent(null);
                g.transform.position = transform.position;
            }

            Debug.Log("Destroy");
            Destroy(dezeParent.GetChild(1).gameObject);
        }


    }
}
