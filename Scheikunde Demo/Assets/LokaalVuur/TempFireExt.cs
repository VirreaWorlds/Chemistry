using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempFireExt : MonoBehaviour
{
    public float small, large;
    public Transform c;
    public ParticleSystem ext;
    public ParticleSystem.EmissionModule e;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            Expand();
        }

        if (Input.GetKeyDown(KeyCode.Period))
        {
            Verkleinen();
        }
    }

    public void Expand()
    {
        //c.localScale = new Vector3(large, large, large);
        e = ext.emission;
        e.rateOverTime = 10;
    }

    public void Verkleinen()
    {
        //c.localScale = new Vector3(small, small, small);
        e = ext.emission;
        e.rateOverTime = 0;
    }
}
