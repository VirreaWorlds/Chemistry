using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droplet : MonoBehaviour
{
    public Stofinformatiehouder stof;

    public Renderer r;

    public ParticleSystem p;

    public float lifeTime = 5;

    public bool isBurning;

    private void Awake()
    {
        if (GetComponent<FluidSplash>() != null)
        {
            GetComponent<FluidSplash>().Invoke("Splash", lifeTime);
        }

        Destroy(this.gameObject, lifeTime);
    }

    private void Update() {
        if(stof != null) {
            r.material.SetColor("_Color", stof.Kleur);
            
            if(GetComponent<TrailRenderer>() != null)
            {
                GetComponent<TrailRenderer>().startColor = stof.Kleur;
                GetComponent<TrailRenderer>().endColor = stof.Kleur;
            }

            var s = p.main;
            s.startColor = stof.Kleur;
            int testInt = !stof.Vloeistof ? 1 : 0;
            r.material.SetInt("_IsStof", testInt);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Untagged"))
        {
            if (GetComponent<FluidSplash>() != null)
            {
                GetComponent<FluidSplash>().Invoke("Splash", 0.0f);
            }

            Destroy(this.gameObject, 0.01f);
        }
    }
}
