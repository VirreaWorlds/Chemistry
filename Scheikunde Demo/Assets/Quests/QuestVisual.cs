using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestVisual : MonoBehaviour
{
    public Quest quest;

    public Text nameText;
    public List<Text> colorChangingTextObjects;
    public Transform objectivesTransform;
    public GameObject objectivePrefab;

    public Color completedColor;

    public bool isCompleted;

    public List<GameObject> visualObjectives;

    bool lastFrame;

    private void Update()
    {
        UpdateQuest();
    }

    public void UpdateQuest()
    {
        isCompleted = quest.IsCompleted();

        if (isCompleted != lastFrame)
        {
            if (isCompleted)
            {
                UpdateAllUIColor(completedColor);
            }
            else
            {
                UpdateAllUIColor(Color.white);
            }
        }

        lastFrame = isCompleted;
    }

    public void UpdateAllUIColor(Color color)
    {
        foreach (var colorText in colorChangingTextObjects)
        {
            colorText.color = color;
        }
    }

    public void Initialize()
    {
        foreach (var questObjective in quest.objectives)
        {
            var g = Instantiate(objectivePrefab, objectivesTransform);
            var v = g.GetComponent<ObjectiveVisual>();

            v.completedColor = completedColor;
            v.objective = questObjective;

            visualObjectives.Add(g);
        }

        nameText.text = quest.questName;
    }
}
