using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public QuestVisual selectedVisual;
    public Quest selectedQuest;

    public bool SelectQuest(QuestVisual visual)
    {
        if(visual == selectedVisual)
        {
            return false;
        }

        if(selectedVisual != null)
        {
            selectedVisual.isSelected = false;
        }

        selectedVisual = visual;
        selectedQuest = selectedVisual.quest;

        selectedVisual.isSelected = true;

        return true;
    }
}
