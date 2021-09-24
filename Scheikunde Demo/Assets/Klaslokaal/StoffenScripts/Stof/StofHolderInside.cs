using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StofHolderInside : MonoBehaviour
{
    public Stofinformatiehouder stofinformatiehouder;
    [SerializeField] public float temperatuur;
    private float timeElapsed;
    [SerializeField] private float lerpDuration;
    public bool onfire;
    [SerializeField] private AudioSource Asrc;

    [SerializeField] private bool Burning;

    [SerializeField] private GameObject Parent;

    public bool critical;

    public bool burningInside;




    // Start is called before the first frame update
    void Awake()
    {
        onfire = false;

        SetVariables();

        Parent = this.gameObject.transform.parent.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        FireCheck();

        if(GetComponent<BekerglasInhoud>().stofinformatiehouder != null)
        {
            SetParticles();
        }
        else
        {
            GetComponent<ParticleHandeler>().Nothing();
            burningInside = false;
            temperatuur = 20;
        }

    }
    private void SetVariables()
    {

    }

    private void FireCheck()
    {
        {
            if(!onfire)
            {
                if (temperatuur >= 22)
                {
                    float b = 20;
                    timeElapsed += Time.deltaTime;
                    temperatuur = Mathf.Lerp(temperatuur, b, timeElapsed / lerpDuration);
                }
            }
        }
    }


    private void SetParticles()
    {
        if (critical)
        {
            return;
        }

        if (stofinformatiehouder != null)
        {
            var TempB = temperatuur >= stofinformatiehouder.Brandpunt;
            var TempC = temperatuur >= stofinformatiehouder.Kookpunt && temperatuur <= stofinformatiehouder.Brandpunt;
            var TempN = temperatuur <= stofinformatiehouder.Kookpunt;


            var PH = GetComponent<ParticleHandeler>();

            if (TempB)
            {
                if (stofinformatiehouder.explosief)
                {
                    if(GetComponent<BekerglasInhoud>().Full == 3)
                    {
                        PH.Explode();
                        critical = true;
                        burningInside = false;
                    }
                    else
                    {
                        PH.Explosion();
                    }
                } else
                {
                    PH.Fire();
                }

                burningInside = true;
            }
            else
            {
                burningInside = false;
            }

            if (TempC)
            {
                PH.Cooking();
            }

            if (TempN)
            {
                PH.Nothing();
            }
        }
    }
   
}
