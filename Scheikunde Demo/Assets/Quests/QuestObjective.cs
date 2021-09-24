using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objective", menuName = "Quests/Objective", order = 0)]
public class QuestObjective : ScriptableObject
{
    [Header("The Objective's Scriptable and Name")]
    public Objectives objective;
    public string objectiveName = "Empty Objective";
    public int amountOfTimeNeededForCompletion = 1;

    [Space]
    [Header("Per Game Stats")]
    public int currentAmount;
    public bool completed;


    [Space]


    [Header("This needs to be assigned only when the objective is set to SpecificResult")]
    public Stofinformatiehouder specificResult;




    public void Reset()
    {
        currentAmount = 0;
        completed = false;
    }
}
