using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BekerglasInhoud : MonoBehaviour
{

    public Color kleur;

    [SerializeField] private ChemistryManager chemistryManager;


    public Stofinformatiehouder stofinformatiehouder;
    public Stofinformatiehouder s;
    public StofHolderInside stofholderInside;
    [SerializeField] public float Full;

    private GameObject child1;
    private GameObject child2;
    private GameObject child3;

    public Renderer fluidRenderer;

    private bool Firsttime;
    private bool Readytomix;

    public float targetLevel;
    public float currentLevel;
    public float fluidMovementSpeed = 1;

    private GameObject otherGameObject;

    public BekerGieter gieter;

    [Space]

    public Stofinformatiehouder luminolSolution;
    public Light luminolLight;

    [Space]
    [Header("Respawn functionality")]
    public bool respawnOnDestruction = true;
    public GameObject respawnPrefab;
    public Transform respawnAnchor;
    public Vector3 respawnPos;
    public bool acceptingInput;

    private float i;

    [Space]

    public bool isUsedInAnimation;

    private void Awake()
    {
        Full = 0;

        i = Full;

        Firsttime = true;
        child1 = transform.GetChild(0).gameObject;
        child2 = transform.GetChild(1).gameObject;
        child3 = transform.GetChild(2).gameObject;

        Invoke("AcceptInput", 1.0f);

        if (respawnOnDestruction)
        {
            //respawnAnchor = transform.parent.parent.parent;
            respawnPos = transform.parent.position;
        }
    }

    private void Update()
    {
        SetInformation();
        SetFullnes();

        if(i != Full)
        {
            OnChangeFull();
        }

        i = Full;
    }

    public void AcceptInput()
    {
        acceptingInput = true;
    }

    private void SetInformation()
    {
        if (stofinformatiehouder != null)
        {
            kleur = stofinformatiehouder.Kleur;
        }
    }

    public void Respawn()
    {
        if (respawnOnDestruction)
        {
            var b = Instantiate(respawnPrefab, null, true);
            b.transform.position = respawnPos;
            //b.transform.Translate(0, 0.1f, 0);
        }
    }

    public void OnChangeFull()
    {
        if (!isUsedInAnimation)
        {
            if (Full == 0)
            {
                QuestManager.instance.SendObjectiveInputToQuests(Objectives.Empty); //Sends a quest objective//
            }

            if (Full == 3)
            {
                QuestManager.instance.SendObjectiveInputToQuests(Objectives.FillComplete); //Sends a quest objective//
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!acceptingInput)
        {
            return;
        }

        if (other.transform.position.y < gieter.fluidPlane.transform.position.y || (Full == 0 && other.transform.position.y < gieter.midPoint.transform.position.y))
        {
            if (Full <= 2)
            {
                if (other.GetComponent<StofHolder>() != null)
                {
                    var othersubstance = other.GetComponent<StofHolder>().stofinformatiehouder;
                    var thissubstance = stofinformatiehouder;
                    var vloeistof = othersubstance.Vloeistof;
                    otherGameObject = other.gameObject;

                    if (vloeistof == true)
                    {
                        if (Firsttime == true)
                        {
                            stofinformatiehouder = chemistryManager.Mix(thissubstance, othersubstance);
                            StartCoroutine(Fulltoevoegen());

                            if (!other.GetComponent<StofHolder>().infiniteSource)
                            {
                                Destroy(otherGameObject);
                            }

                            Full = Full + 1;
                            stofholderInside.stofinformatiehouder = stofinformatiehouder;
                        }
                    }
                }

                if (other.gameObject.GetComponent<Droplet>() != null)
                {
                    //Debug.Log("droplet found");
                    //s = Instantiate(other.GetComponent<Droplet>().stof);
                    s = other.GetComponent<Droplet>().stof;

                    var othersubstance = s;
                    var thissubstance = stofinformatiehouder;
                    var vloeistof = s.Vloeistof;
                    otherGameObject = other.gameObject;

                    if (vloeistof == true)
                    {
                        //Debug.Log("is vloeistof");
                        if (Firsttime == true)
                        {
                            //Debug.Log("firstt time");
                            stofinformatiehouder = chemistryManager.Mix(thissubstance, othersubstance);
                            StartCoroutine(Fulltoevoegen());
                            Destroy(otherGameObject);
                            Full = Full + 1;
                            stofholderInside.stofinformatiehouder = stofinformatiehouder;
                        }
                    }

                    Destroy(other.gameObject);
                }
            }

            if (Full >= 0.2)
            {
                if (other.GetComponent<StofHolder>() != null)
                {
                    var othersubstance = other.GetComponent<StofHolder>().stofinformatiehouder;
                    var thissubstance = stofinformatiehouder;
                    var vloeistof = other.GetComponent<StofHolder>().stofinformatiehouder.Vloeistof;
                    otherGameObject = other.gameObject;

                    if (vloeistof == false)
                    {
                        stofinformatiehouder = chemistryManager.Mix(thissubstance, othersubstance);
                        if (chemistryManager.Mix(thissubstance, othersubstance) != null)
                        {
                            stofholderInside.stofinformatiehouder = stofinformatiehouder;
                            QuestManager.instance.SendObjectiveInputToQuests(Objectives.PutMaterialInContainer); //Sends a quest objective//

                            if (!other.GetComponent<StofHolder>().infiniteSource)
                            {
                                Destroy(otherGameObject);
                            }
                        }

                    }
                }

                if (other.gameObject.GetComponent<Droplet>() != null)
                {
                    AudioManager.instance.PlaceSoundAt(SoundType.Fluid, transform.position, 1);

                    //s = Instantiate(other.GetComponent<Droplet>().stof);
                    s = other.GetComponent<Droplet>().stof;

                    var othersubstance = s;
                    var thissubstance = stofinformatiehouder;
                    var vloeistof = s.Vloeistof;
                    otherGameObject = other.gameObject;

                    if (vloeistof == false)
                    {
                        stofinformatiehouder = chemistryManager.Mix(thissubstance, othersubstance);
                        if (chemistryManager.Mix(thissubstance, othersubstance) != null)
                        {
                            stofholderInside.stofinformatiehouder = stofinformatiehouder;
                            Destroy(otherGameObject);
                        }

                    }

                    Destroy(otherGameObject);
                }
            }
        }
        
    }

    IEnumerator Fulltoevoegen()
    {
        if (Firsttime == true)
        {
            Firsttime = false;
            yield return new WaitForSeconds(0.05f);
            Firsttime = true;
        }

    }


    private void SetFullnes()
    {
        
        if (Full == 0)
        {
            Readytomix = false;
            //child1.SetActive(false);
            //child2.SetActive(false);
            //child3.SetActive(false);

            targetLevel = 0.0f;
        }

        if (Full == 1)
        {
            Readytomix = true;
            //child1.SetActive(true);
            //child2.SetActive(false);
            //child3.SetActive(false);

            targetLevel = 0.3f;
        }

        if (Full == 2)
        {
            Readytomix = true;
            //child1.SetActive(false);
            //child2.SetActive(true);
            //child3.SetActive(false);

            targetLevel = 0.6f;
        }

        if (Full == 3)
        {
            Readytomix = true;
            //child1.SetActive(false);
            //child2.SetActive(false);
            //child3.SetActive(true);

            targetLevel = 0.9f;
        }

        if (luminolLight != null && luminolSolution != null)
        {
            if (stofinformatiehouder == luminolSolution)
            {
                if (Full != 0)
                {
                    luminolLight.intensity = targetLevel / 5;

                    //if (!luminolLight.enabled)
                    //{
                    //    luminolLight.enabled = true;
                    //}
                }
                else
                {
                    luminolLight.intensity = 0;
                    //luminolLight.enabled = false;
                }
            }
            else
            {
                //luminolLight.enabled = false;
                luminolLight.intensity = 0;
            }
        }

        if(currentLevel != targetLevel)
        {
            currentLevel = Mathf.MoveTowards(currentLevel, targetLevel, (fluidMovementSpeed * Time.deltaTime));
        }

        if(currentLevel <= 0.0f)
        {
            kleur = new Color(0, 0, 0, 0);
        }

        fluidRenderer.material.SetFloat("_WorldPlaneHeight", gieter.fluidPlane.position.y);
        gieter.fluidPlaneVisible = Full != 0;
        gieter.OnLevelGet(currentLevel);
    }
}

