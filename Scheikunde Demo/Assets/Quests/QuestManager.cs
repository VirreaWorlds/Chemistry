using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    static public QuestManager instance;
    public List<Quest> quests;
    public List<Quest> completedQuests;
    public Color questCompletedColor;

    public List<GameObject> visualQuests;
    public GameObject visualQuestPrefab;
    public Transform visualQuestsTransform;
    public NotificationSpawner nSpawner;

    public void SendObjectiveInputToQuests(Objectives objectiveInput) //Send the new objective information to all Quests and checks whether they are completed or not.
    {
        foreach (var quest in quests)
        {
            if (quest.UpdateObjectives(objectiveInput))
            {
                Debug.Log("< " + quest.questName + " > is now completed.");

                completedQuests.Add(quest);
                //quests.Remove(quest);
                AudioManager.instance.quest.Play();
                QuestManager.instance.nSpawner.CreateNewNotification(quest);
            }
        }

        foreach (var completedQuest in completedQuests)
        {
            if(isInBothList(completedQuest, quests))
            {
                quests.Remove(completedQuest);
            }
        }
    }

    public void SendObjectiveInputToQuests(Objectives objectiveInput, Stofinformatiehouder stof) //Send the new objective information to all Quests and checks whether they are completed or not.
    {
        foreach (var quest in quests)
        {
            if (quest.UpdateObjectives(objectiveInput, stof))
            {
                Debug.Log("< " + quest.questName + " > is now completed.");

                completedQuests.Add(quest);
                //quests.Remove(quest);
                AudioManager.instance.quest.Play();
                QuestManager.instance.nSpawner.CreateNewNotification(quest);
            }
        }

        foreach (var completedQuest in completedQuests)
        {
            if (isInBothList(completedQuest, quests))
            {
                quests.Remove(completedQuest);
            }
        }
    }

    public bool isInBothList(Quest questToCompare, List<Quest> compareToList)
    {
        foreach (Quest comparable in compareToList)
        {
            if(comparable == questToCompare)
            {
                return true;
            }
        }

        return false;
    }

    private void Start()
    {
        instance = this;

        foreach (var quest in quests)
        {
            var g = Instantiate(visualQuestPrefab, visualQuestsTransform);
            g.name = "< " + quest.questName + " >";
            var v = g.GetComponent<QuestVisual>();

            v.completedColor = questCompletedColor;
            v.quest = quest;
            v.Initialize();

            visualQuests.Add(g);
        }
    }

    public void ResetAllQuests()
    {
        foreach (var quest in quests)
        {
            quest.Reset();
        }

        foreach (var quest in completedQuests)
        {
            quest.Reset();
        }
    }

    private void OnApplicationQuit()
    {
        ResetAllQuests();
    }
}

[System.Serializable]
public enum Objectives
{
    Reaction,
    Explosion,
    FillComplete,
    Empty,
    CreateFire,
    TurnFireOn,
    Boil,
    SpecificResult,
    PickUpMaterial,
    PutMaterialInContainer,
    StartAFire,
    BurnEntireRoom,
    RemoveAllFire
}


//  QuestManager.instance.SendObjectiveInputToQuests(Objectives.Empty); //Sends a quest objective//
//  Example line which you can use to send Objectives