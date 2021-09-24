using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opvullingkleur : MonoBehaviour
{
    public Color kleurie;

    private void Update()
    {
        kleurie = transform.parent.gameObject.GetComponent<BekerglasInhoud>().kleur;
        //GetComponent<Renderer>().material.color = kleurie;
        GetComponent<Renderer>().material.SetColor("_Color", kleurie);
    }
}
