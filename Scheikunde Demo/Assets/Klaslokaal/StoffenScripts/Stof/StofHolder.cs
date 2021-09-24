using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StofHolder : MonoBehaviour
{
    public Stofinformatiehouder stofinformatiehouder;
    [SerializeField] public float temperatuur;
    private float timeElapsed;
    [SerializeField] private float lerpDuration;
    public bool onfire;
    public bool infiniteSource;

    public GameObject infoShower;
    GameObject g;
    public bool showInfo = true;



    // Start is called before the first frame update
    void Awake()
    {
        if(stofinformatiehouder == null)
        {
            Invoke("Awake", 0);
            return;
        }

        onfire = false;
        if (GetComponent<Renderer>() != null)
        {
            GetComponent<Renderer>().material.color = stofinformatiehouder.Kleur;
        }
        else if (GetComponentInChildren<Renderer>() != null)
        {
            GetComponentInChildren<Renderer>().material.color = stofinformatiehouder.Kleur;
        }

        if (showInfo)
        {
            if (infoShower != null && stofinformatiehouder != null)
            {
                Invoke("CreateInfoShower", 0);
            }
            else
            {
                //Debug.Log("InfoShower and/or stofinformatiehouder is not assigned");
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!onfire)
        {
            if (temperatuur >= 22)
            {
                float b = 20;
                timeElapsed += Time.deltaTime;
                temperatuur = Mathf.Lerp(temperatuur, b, timeElapsed / lerpDuration);
            }
        }

        if (g != null)
        {
            //g.transform.position = transform.position;
            //g.transform.Translate(0, 0.01f, 0);

            g.transform.LookAt(Camera.main.transform.position);
            //g.transform.Rotate(0, 180, 0, Space.Self);
        }
    }

    void CreateInfoShower()
    {
        if(g != null)
        {
            return;
        }

        g = Instantiate(infoShower, null);

        g.transform.SetParent(transform);

        g.transform.position = transform.position;
        //g.transform.Translate(0, 0.01f, 0);

        var t1 = g.GetComponent<TextMesh>();
        var t2 = g.transform.GetChild(0).GetComponentInChildren<TextMesh>();

        t1.text = stofinformatiehouder.Naam;
        t2.text = "";

        if (stofinformatiehouder.Vloeistof)
        {
            t2.text += " Vloeistof ";
        }

        if (stofinformatiehouder.zuur)
        {
            t2.text += " Zuur ";
        }

        if (stofinformatiehouder.explosief)
        {
            t2.text += " Explosief ";
        }
    }

    private void OnDestroy()
    {
        Destroy(g);
    }
}
