using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quests/Quest", order = 1)]
public class Quest : ScriptableObject
{
    public List<QuestObjective> objectives;
    public string questName = "Unnamed Quest";

    /// <summary> Updates all objectives connected to this Quest. Returns true when all objectives are completed. </summary>
    public bool UpdateObjectives(Objectives currentEvent)
    {
        foreach (var ob in objectives)
        {
            if(ob.objective == currentEvent)
            {
                ob.currentAmount++;
                if (ob.currentAmount >= ob.amountOfTimeNeededForCompletion && ob.completed == false)
                {
                    ob.completed = true;
                    AudioManager.instance.objective.Play();
                }
            }
        }

        foreach (var ob in objectives)
        {
            if (!ob.completed)
            {
                return false;
            }
        }

        //Code below this won't be used unless all objectives are completed

        return true;
    }

    public bool IsCompleted()
    {
        foreach (var ob in objectives)
        {
            if (!ob.completed)
            {
                return false;
            }
        }

        return true;
    }

    public bool UpdateObjectives(Objectives currentEvent, Stofinformatiehouder stof)
    {
        foreach (var ob in objectives)
        {
            if (ob.objective == currentEvent && !ob.completed)
            {
                if(ob.objective == Objectives.SpecificResult)
                {
                    if(ob.specificResult == stof)
                    {
                        ob.currentAmount++;

                        if (ob.currentAmount >= ob.amountOfTimeNeededForCompletion)
                        {
                            ob.completed = true;

                            AudioManager.instance.objective.Play();
                        } 
                    }
                }
                else
                {
                    ob.currentAmount++;
                    if (ob.currentAmount >= ob.amountOfTimeNeededForCompletion && ob.completed == false)
                    {
                        ob.completed = true;
                        AudioManager.instance.objective.Play();
                    }
                }
            }
        }

        foreach (var ob in objectives)
        {
            if (!ob.completed)
            {
                return false;
            }
        }

        //Code below this won't be used unless all objectives are completed

        return true;
    }

    public void Reset()
    {
        foreach (var ob in objectives)
        {
            ob.Reset();
        }
    }
}
