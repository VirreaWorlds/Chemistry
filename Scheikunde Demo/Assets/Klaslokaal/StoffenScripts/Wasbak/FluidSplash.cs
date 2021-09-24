using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidSplash : MonoBehaviour
{
    public GameObject splashPrefab;
    public bool splashSound = true;

    private void Splash()
    {
        if (splashSound)
        {
            AudioManager.instance.PlaceSoundAt(SoundType.Fluid, transform.position, 1);
        }

        var g = Instantiate(splashPrefab, this.transform);
        g.transform.SetParent(null);
        g.transform.position = transform.position;
        g.transform.localScale = Vector3.one;
        var p = g.GetComponent<ParticleSystem>();
        var m = p.main;

        if (GetComponent<Droplet>())
        {
            m.startColor = GetComponent<Droplet>().stof.Kleur;
        }

        if (GetComponent<StofHolder>())
        {
            m.startColor = GetComponent<StofHolder>().stofinformatiehouder.Kleur;
        }

        p.Play();
    }
}
