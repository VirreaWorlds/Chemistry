using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabStof : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject StofObj;

    public Transform ThisTrans;
    public GameObject ThisOBJ;


    private void Start()
    {
        ThisTrans = transform.GetChild(0).transform;
        ThisOBJ = this.gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<StofHolder>() != null)
        {

            if (!ThisTrans.GetComponent<CheckFull>().Voll)
            {

                StofObj = other.gameObject;

                Quaternion lol = new Quaternion(0, 0, 0, 0);

                GameObject Clone = Instantiate(Prefab, ThisTrans.position, Quaternion.identity);
                Clone.GetComponent<StofHolder>().stofinformatiehouder = StofObj.GetComponent<StofHolder>().stofinformatiehouder;
                Clone.transform.SetParent(ThisOBJ.transform, true);
                Clone.GetComponentInChildren<Renderer>().material.color = StofObj.GetComponent<StofHolder>().stofinformatiehouder.Kleur;
                Clone.transform.localRotation = Quaternion.identity;
                Clone.transform.Rotate(100, 0, 0);
                // Clone.transform.localScale = new Vector3(0.0005f, 0.0005f, 0.0005f);

                QuestManager.instance.SendObjectiveInputToQuests(Objectives.PickUpMaterial); //Sends a quest objective//
            }
        }
    }

}
