using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    public SelectionManager selMan;
    public LayerMask layerMask;

    public Animator notifAnim; //the object which notifies you about selection changes
    public GameObject notifObject;
    public float notifDuration = 2.0f;
    public float notifTime;
    public bool notifActive;

    [Space]

    public bool screenIsOpen;
    public GameObject screen;
    public GameObject emptyScreen;
    public GameObject fullScreen;
    public Text questName;
    public List<Image> objectiveNames;
    bool b;

    public Color green, white, greenA, greyA;

    [Space]

    public LineRenderer lr;


    private void Update()
    {
        if (BNG.InputBridge.Instance.AButton)
        {
            FireRaycast();
        }

        lr.enabled = BNG.InputBridge.Instance.AButton || Input.GetKey(KeyCode.Mouse0);

        if ((BNG.InputBridge.Instance.BButton != b && BNG.InputBridge.Instance.BButton) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            ToggleScreen();
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            FireRaycast(true); //debug version
        }

        if (notifActive)
        {
            CheckTimer();
        }

        if (screenIsOpen)
        {
            UpdateScreenInfo();
        }

        b = BNG.InputBridge.Instance.BButton;
    }

    private void FixedUpdate()
    {
        if (!notifActive)
        {
            notifObject.transform.LookAt(Camera.main.transform);
        }
    }

    public void UpdateScreenInfo()
    {
        if(selMan.selectedVisual != null)
        {
            emptyScreen.SetActive(false);
            fullScreen.SetActive(true);

            if (selMan.selectedQuest.IsCompleted())
            {
                questName.text = selMan.selectedQuest.questName + " ☑";
                questName.color = green;
            }
            else
            {
                questName.text = selMan.selectedQuest.questName;
                questName.color = white;
            }

            foreach (var item in objectiveNames)
            {
                item.gameObject.SetActive(false);
            }

            for (int i = 0; i < selMan.selectedQuest.objectives.Count; i++)
            {
                var g = objectiveNames[i];
                g.gameObject.SetActive(true);

                if (selMan.selectedQuest.objectives[i].completed)
                {
                    objectiveNames[i].color = greenA;
                }
                else
                {
                    objectiveNames[i].color = greyA;
                }

                objectiveNames[i].GetComponentInChildren<Text>().text = selMan.selectedQuest.objectives[i].objectiveName;
            }
        }
        else
        {
            emptyScreen.SetActive(true);
            fullScreen.SetActive(false);
        }
    }

    public void ToggleScreen()
    {
        screenIsOpen = !screenIsOpen;
        screen.SetActive(screenIsOpen);
    }

    public void RefreshTimer()
    {
        if (!notifActive)
        {
            if (notifAnim != null)
            {
                notifAnim.Play("Open"); //not real yet
            }
        }

        notifTime = 0;
        notifActive = true;
    }

    public void CheckTimer()
    {
        notifTime += Time.deltaTime;

        if(notifTime > notifDuration)
        {
            notifActive = false;

            if (notifAnim != null)
            {
                notifAnim.Play("Close");
            }
        }
    }

    public void FireRaycast(bool isDebug = false)
    {
        Ray r;

        if (isDebug)
        {
            r = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        else
        {
            r = new Ray(transform.position, transform.forward);
        }

        RaycastHit hit;

        if (Physics.Raycast(r, out hit, 60.0f, layerMask))
        {
            var q = hit.collider.gameObject.GetComponent<QuestVisual>();

            if (selMan.SelectQuest(q))
            {
                RefreshTimer();
            }
        }
    }
}
