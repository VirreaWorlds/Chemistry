using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParticleHandeler : MonoBehaviour
{
    [SerializeField] private Stofinformatiehouder stofinformatiehouder;

    [SerializeField] private GameObject Vlammen;
    [SerializeField] private GameObject Explosie;
    [SerializeField] private GameObject Koken;
    [SerializeField] private GameObject A;

    [SerializeField] private ParticleSystem Vlammenprt;
    [SerializeField] private ParticleSystem Explosieprt;
    [SerializeField] private ParticleSystem Kokenprt;
    [SerializeField] private ParticleSystem AP;

    public GameObject explosionEffect;


    [SerializeField] private AudioSource Vlammensrc;
    [SerializeField] private AudioSource Explosiesrc;
    [SerializeField] private AudioSource Kokensrc;

    private ParticleSystem.EmissionModule Vem;
    private ParticleSystem.EmissionModule Eem;
    private ParticleSystem.EmissionModule Kem;
    private ParticleSystem.EmissionModule Vam;

    public bool isExploding;

    public AudioSource vlammenAudio;
    public AudioSource explosieAudio;
    public AudioSource kokenAudio;

    bool canPlayExplosion = true;


    void Awake()
    {
        stofinformatiehouder = gameObject.GetComponent<StofHolderInside>().stofinformatiehouder;
        SetVariables();
        GameObject A = Explosie.transform.GetChild(0).gameObject;
    }
    // Update is called once per frame
    void Update()
    {


    }

    private void SetVariables()
    {
        
            //Vlammen = transform.GetChild(3).gameObject;
            //Explosie = transform.GetChild(4).gameObject;
            //Koken = transform.GetChild(5).gameObject;

            //ParticleSystem Vlammenprt = Vlammen.GetComponent<ParticleSystem>();
            //ParticleSystem Explosieprt = Explosie.GetComponent<ParticleSystem>();
            //ParticleSystem Kokenprt = Koken.GetComponent<ParticleSystem>();

            //AudioSource Vlammensrc = Vlammen.GetComponent<AudioSource>();
            //AudioSource Explosiesrc = Explosie.GetComponent<AudioSource>();
            //AudioSource Kokensrc = Koken.GetComponent<AudioSource>();

            Vem = Vlammenprt.emission;
            Eem = Explosieprt.emission;
            Kem = Kokenprt.emission;
            Vam = AP.emission;

            Vlammensrc.enabled = true;
            Explosiesrc.enabled = true;
            Kokensrc.enabled = true;
    }



    public void Fire()
    {
        if (Vem.enabled)
        {
            return;
        }

        //Vlammenprt.enableEmission = true;
        Vem.enabled = true;
        //  Vlammensrc.Play();
        //Kokenprt.enableEmission = false;
        Kem.enabled = false;
        Kokensrc.Stop();

        if (!Vlammensrc.isPlaying)
        {
            Vlammensrc.Play();
        }

        QuestManager.instance.SendObjectiveInputToQuests(Objectives.CreateFire); //Sends a quest objective//
    }

    public void Explosion()
    {
        //Vlammenprt.enableEmission = true;
        Vem.enabled = true;
        //  Vlammensrc.Play();
        //Explosieprt.enableEmission = true;
        Eem.enabled = true;
        //  Explosiesrc.Play();
        //Kokenprt.enableEmission = false;
        Kem.enabled = false;
        Kokensrc.Stop();

        //Debug.Log("boem");

        if (!Explosiesrc.isPlaying && canPlayExplosion)
        {
            canPlayExplosion = false;
            Explosiesrc.Play();
        }

        if (!Vlammensrc.isPlaying)
        {
            Vlammensrc.Play();
        }
    }

    public void Explode()
    {
        if (!isExploding)
        {
            isExploding = true;
            Nothing();

            AudioManager.instance.PlaceSoundAt(SoundType.Explosion, transform.position, 1.0f);
            QuestManager.instance.SendObjectiveInputToQuests(Objectives.Explosion); //Sends a quest objective//

            var g = Instantiate(explosionEffect);
            g.transform.SetParent(null);
            g.transform.position = this.transform.position;
            //g.transform.localScale = Vector3.one;
            g.GetComponent<ParticleSystem>().Play();
            Destroy(g, g.GetComponent<ParticleSystem>().main.duration);

            DestroyAndRespawn();
        }
    }

    public void DestroyAndRespawn()
    {
        transform.parent.GetComponent<BNG.Grabbable>().DropItem(true,true);
        Destroy(transform.parent.parent.gameObject, 0.0f);
        GetComponent<BekerglasInhoud>().Respawn();
    }

    public void Cooking()
    {
        //Vlammenprt.enableEmission = false;
        Vem.enabled = false;
        Vlammensrc.Stop();
        //AP.enableEmission = false;
        Vam.enabled = false;
        Explosiesrc.Stop();
        //Kokenprt.enableEmission = true;
        Kem.enabled = true;

        if (!Kokensrc.isPlaying)
        {
            Kokensrc.Play();
        }

        //Debug.Log("cooking");

        QuestManager.instance.SendObjectiveInputToQuests(Objectives.Boil); //Sends a quest objective//
    }

    public void Nothing()
    {
        //Vlammenprt.enableEmission = false;
        Vem.enabled = false;
        //AP.enableEmission = false;
        Vam.enabled = false;
        //Kokenprt.enableEmission = false;
        Kem.enabled = false;
        //Explosieprt.enableEmission = false;
        Eem.enabled = false;
        //Debug.Log("Niks");

        Vlammensrc.Stop();
        Kokensrc.Stop();
        Explosiesrc.Stop();

        canPlayExplosion = true;
    }
}
