using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveVisual : MonoBehaviour
{
    public Text objectiveStatus;

    public Color completedColor;

    public bool isCompleted;

    public QuestObjective objective;

    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        int current = objective.currentAmount;
        int needed = objective.amountOfTimeNeededForCompletion;

        string s;

        if (current >= needed)
        {
            if(objective.amountOfTimeNeededForCompletion > 1)
            {
                s = objective.objectiveName + "   " + needed + " / " + needed;
            }
            else
            {
                s = objective.objectiveName;
            }

            objectiveStatus.color = completedColor;
        }
        else
        {
            if (objective.amountOfTimeNeededForCompletion > 1)
            {
                s = objective.objectiveName + "   " + current + " / " + needed;
            }
            else
            {
                s = objective.objectiveName;
            }

            objectiveStatus.color = Color.white;
        }

        objectiveStatus.text = s;

        isCompleted = objective.completed;
    }
}
