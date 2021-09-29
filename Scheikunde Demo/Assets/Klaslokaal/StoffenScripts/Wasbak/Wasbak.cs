using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasbak : MonoBehaviour
{
    public float inhoud;

    public Vector2 minMaxWaterHeight;
    public GameObject waterPlane;

    public float waterAddedPerDrop;

    public Stofinformatiehouder waterExample;

    public Transform kraan1, kraan2;
    public GameObject waterDruppelPrefab;
    public float kraanDruppelDelay;
    public bool kraanAan;
    public bool forceAan;

    public Transform kraanBekerDetector;
    public float kraanBekerDetectorRange = 0.5f;

    public float waterNeededPerBekerScoop = 9;
    bool b;

    public AudioSource kraanAudio;
    public float drainSpeed = 1.0f;

    //public bool dingOnderwater;

    private void Start()
    {
        StartCoroutine("KraanLoop");
    }

    private void Update()
    {
        inhoud = Mathf.Clamp(inhoud, 0, 100);

        waterPlane.transform.localPosition = new Vector3(0, Mathf.Lerp(minMaxWaterHeight.x, minMaxWaterHeight.y, inhoud / 100), 0);

        if(b)
        {
            kraanAan = true;

            if (!kraanAudio.isPlaying)
            {
                kraanAudio.Play();
            }
        }
        else
        {
            kraanAan = false;

            kraanAudio.Stop();
        }

        if (forceAan) kraanAan = true;

        if(inhoud > 0)
        {
            inhoud -= drainSpeed * Time.deltaTime;
        }
        
        //kraanAan = inhoud < 100.0f;
    }

    public IEnumerator KraanLoop()
    {
        if (kraanAan)
        {
            var a = Instantiate(waterDruppelPrefab, kraan1.transform, true);
            var b = Instantiate(waterDruppelPrefab, kraan2.transform, true);

            a.transform.position = kraan1.transform.position;
            b.transform.position = kraan2.transform.position;
        }

        yield return new WaitForSeconds(kraanDruppelDelay);

        StartCoroutine("KraanLoop");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Droplet>() != null || other.GetComponent<StofHolder>() != null)
        {
            if (other.gameObject.transform.position.y < waterPlane.transform.position.y)
            {
                Stofinformatiehouder s;

                if (other.GetComponent<Droplet>() != null)
                {
                    s = other.GetComponent<Droplet>().stof;
                }
                else if (other.GetComponent<StofHolder>() != null)
                {
                    s = other.GetComponent<StofHolder>().stofinformatiehouder;
                }
                else
                {
                    return;
                }

                if (s == waterExample)
                {
                    inhoud += waterAddedPerDrop;
                }

                if (other.transform.CompareTag("Droplet"))
                {
                    AudioManager.instance.PlaceSoundAt(SoundType.Fluid, other.transform.position, 1);
                }

                Destroy(other.gameObject);
            }
        }

        if (other.GetComponent<BekerGieter>() != null)
        {
            b = Vector3.Distance(other.transform.position, kraanBekerDetector.transform.position) <= kraanBekerDetectorRange;

            //if (inhoud >= waterNeededPerBekerScoop)
            //{
            //    var b = other.GetComponent<BekerGieter>();
            //    if (b.inhoudEmitPoint.transform.position.y < waterPlane.transform.position.y)
            //    {
            //        int amountOfLevelsUp = 0;

            //        if (waterPlane.transform.position.y > b.lowestPoint.transform.position.y && b.bekerInhoud.Full < 1)
            //        {
            //            b.bekerInhoud.Full = 1;
            //            b.bekerInhoud.stofinformatiehouder = waterExample;
            //            amountOfLevelsUp++;
            //        }

            //        if (waterPlane.transform.position.y > b.midPoint.transform.position.y && b.bekerInhoud.Full < 2)
            //        {
            //            b.bekerInhoud.Full = 2;
            //            b.bekerInhoud.stofinformatiehouder = waterExample;
            //            amountOfLevelsUp++;
            //        }

            //        if (waterPlane.transform.position.y > b.highestPoint.transform.position.y && b.bekerInhoud.Full < 3)
            //        {
            //            b.bekerInhoud.Full = 3;
            //            b.bekerInhoud.stofinformatiehouder = waterExample;
            //            amountOfLevelsUp++;
            //        }

            //        inhoud -= amountOfLevelsUp * 3;
            //    }
            //}
        }

        //if (other.GetComponent<BekerGieter>() != null)
        //{
        //    other.GetComponent<BekerGieter>().inwater = other.GetComponent<BekerGieter>().inhoudEmitPoint.transform.position.y < waterPlane.transform.position.y;
        //    //other.GetComponent<BekerGieter>().inwater = false;
        //}
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.GetComponent<BekerGieter>() != null)
    //    {
    //        other.GetComponent<BekerGieter>().inwater = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.GetComponent<BekerGieter>() != null)
    //    {
    //        other.GetComponent<BekerGieter>().inwater = false;
    //    }
    //}
}
