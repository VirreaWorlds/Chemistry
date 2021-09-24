using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireNode : MonoBehaviour
{
    public float temp = 20;
    public float roomTemp = 20;
    public float spreadTemp = 80;
    public float onFirePoint = 60;
    public float ignitionPoint = 100;
    public float tempRiseSpeed = 5;
    public bool onFire;
    public ParticleSystem fire;
    [SerializeField] public RaycastHit[] closeObjects;
    public List<GameObject> closeFlammables;

    public Color coldColor;
    public Color hotColor;
    public float gizmosRadius;
    public bool useGizmos;
    public bool heating;

    public FireSystem fireSystem;
    public float extinguishMultiplier = 1;

    bool lastFrame;
    float lerpValue;

    private void Update()
    {
        if(temp > onFirePoint)
        {
            onFire = true;
        }
        else
        {
            onFire = false;
        }

        lerpValue = (temp - onFirePoint) / (ignitionPoint - onFirePoint); //roomtemp

        var e = fire.emission;
        if (onFire)
        {
            e.rateOverTime = lerpValue * 20;
        }
        else
        {
            e.rateOverTime = 0;
        }

        if(lastFrame != onFire)
        {
            if (onFire)
            {
                fireSystem.MoveToOther(this, fireSystem.activeNodes);
            }
            else
            {
                fireSystem.MoveToOther(this, fireSystem.nodes);
            }
        }

        lastFrame = onFire;

        if (onFire)
        {
            temp = Mathf.MoveTowards(temp, ignitionPoint, tempRiseSpeed * Time.deltaTime);

            if(temp > spreadTemp)
            {
                foreach (var item in closeFlammables)
                {
                    item.GetComponent<FireNode>().heating = true;
                }
            }
        }

        if (heating)
        {
            Heating();
            heating = false;
        }
    }

    public void Heating()
    {
        temp = Mathf.MoveTowards(temp, ignitionPoint, tempRiseSpeed * Time.deltaTime);
    }

    private void Start()
    {
        Invoke("FindObjects", 0.1f);
    }

    public void FindObjects()
    {
        closeObjects = Physics.SphereCastAll(transform.position, gizmosRadius, transform.up);
        foreach (var item in closeObjects)
        {
            if (item.transform.CompareTag("Flammable"))
            {
                closeFlammables.Add(item.transform.gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (useGizmos)
        {
            Gizmos.color = Color.Lerp(coldColor, hotColor, lerpValue);
            Gizmos.DrawSphere(transform.position, gizmosRadius);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Droplet>() != null)
        {
            var d = other.GetComponent<Droplet>();
            if (d.isBurning)
            {
                onFire = true;
            }

            QuestManager.instance.SendObjectiveInputToQuests(Objectives.StartAFire); //Sends a quest objective//
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Extinguisher"))
        {
            temp = roomTemp;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Extinguisher"))
        {
            temp = Mathf.MoveTowards(temp, roomTemp, (tempRiseSpeed + (tempRiseSpeed * extinguishMultiplier)) * Time.deltaTime);
        }
    }
}
