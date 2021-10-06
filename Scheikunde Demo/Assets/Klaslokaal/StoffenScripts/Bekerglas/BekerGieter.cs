using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BekerGieter : MonoBehaviour
{
    public BekerglasInhoud bekerInhoud;

    public Transform inhoudEmitPoint;
    public GameObject inhoudPrefab;

    public Transform mainPoint, highestPoint, midPoint, lowestPoint;

    public List<Transform> mainPoints;
    public Transform currentLowest;

    public List<Transform> lowerPoints;
    public List<Transform> upperPoints;
    public Transform fluidPlane;
    public bool fluidPlaneVisible = true;

    [Range(0, 1)]
    public float level;

    public bool inwater;

    [Space]

    public bool activeGieter;
    public float activeTimer;
    public bool b; //isTrueWhenInHand
    private float t; //countDownBeforeDeactivate

    private void Update()
    {
        if (Timer(b))
        {
            Gieter();
        }
    }

    public void Gieter()
    {
        currentLowest = GetLowestPoint(mainPoints);

        if (!inwater && bekerInhoud.stofinformatiehouder != null)
        {
            if (currentLowest.position.y < highestPoint.position.y && bekerInhoud.Full == 3)
            {
                LowerLevel();
            }
            else if (currentLowest.position.y < midPoint.position.y && bekerInhoud.Full == 2)
            {
                LowerLevel();
            }
            else if (currentLowest.position.y < lowestPoint.position.y && bekerInhoud.Full == 1)
            {
                LowerLevel();
            }
        }
    }

    public bool Timer(bool isHolding)
    {
        if (isHolding)
        {
            t = 0;
        }
        else
        {
            t += Time.deltaTime;
        }

        activeGieter = t < activeTimer;
        return t < activeTimer;
    }

    public void OnLevelGet(float l)
    {
        SetWaterPlaneLevel(lowerPoints, upperPoints, fluidPlane, l);
    }

    public void LowerLevel() {
        //var s = Instantiate(bekerInhoud.stofinformatiehouder);
        var s = bekerInhoud.stofinformatiehouder;

        if (inhoudPrefab != null)
        {
            GameObject g = Instantiate(inhoudPrefab, currentLowest);

            g.GetComponent<Droplet>().stof = s;
            g.GetComponent<Droplet>().isBurning = bekerInhoud.stofholderInside.burningInside;
            g.transform.SetParent(null);
            g.transform.position = currentLowest.position;
        }
        else
        {
            Debug.LogError("InhoudPrefab van BekerGieter is null");
        }

        //var s = g.AddComponent(typeof(Stofinformatiehouder));

        bekerInhoud.Full--;

        if(bekerInhoud.Full <= 0)
        {
            bekerInhoud.stofinformatiehouder = null;
            bekerInhoud.stofholderInside.stofinformatiehouder = null;

            GetComponent<ParticleHandeler>().Nothing();
        }
    }

    public void SetWaterPlaneLevel(List<Transform> lowerCircle, List<Transform> upperCircle, Transform waterPlane, float progressZeroToOne)
    {
        if (fluidPlaneVisible)
        {
            Transform l = GetLowestPoint(lowerCircle);
            Transform h = GetHighestPoint(upperCircle);

            float dis = h.position.y - l.position.y;

            Vector3 planePos = new Vector3(transform.position.x, l.transform.position.y + (dis * progressZeroToOne), transform.position.z);

            waterPlane.position = planePos;
            waterPlane.LookAt(new Vector3(0, 0, 3000));

            waterPlane.gameObject.SetActive(true);
        }
        else
        {
            waterPlane.gameObject.SetActive(false);
        }
    }

    public Transform GetLowestPoint(List<Transform> transforms)
    {
        if(transforms.Count <= 0)
        {
            return null;
        }

        Transform lowest = transforms[0];

        foreach (Transform t in transforms)
        {
            if(t.position.y < lowest.position.y)
            {
                lowest = t;
            }
        }

        return lowest;
    }

    public Transform GetHighestPoint(List<Transform> transforms)
    {
        if (transforms.Count <= 0)
        {
            return null;
        }

        Transform highest = transforms[0];

        foreach (Transform t in transforms)
        {
            if (t.position.y > highest.position.y)
            {
                highest = t;
            }
        }

        return highest;
    }

    public void Hold()
    {
        b = true;
    }

    public void Holdnt()
    {
        b = false;
    }
}
