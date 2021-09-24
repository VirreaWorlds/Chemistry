using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSystem : MonoBehaviour
{
    public GameObject nodePrefab;
    public int amount = 28;
    public Transform spawnTransform;
    public List<FireNode> nodes;
    public List<FireNode> activeNodes;
    public float extinguishStrengthMultiplier = 2;

    bool fireInHistory;

    void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            var g = Instantiate(nodePrefab, spawnTransform);
            g.GetComponent<FireNode>().fireSystem = this;
            g.GetComponent<FireNode>().extinguishMultiplier = extinguishStrengthMultiplier;
            nodes.Add(g.GetComponent<FireNode>());
        }
    }

    public void MoveToOther(FireNode node, List<FireNode> targetList)
    {
        if(targetList == nodes)
        {
            nodes.Add(node);
            activeNodes.Remove(node);
        }
        else
        {
            activeNodes.Add(node);
            nodes.Remove(node);
            fireInHistory = true;
        }

        if(activeNodes.Count == amount)
        {
            QuestManager.instance.SendObjectiveInputToQuests(Objectives.BurnEntireRoom); //Sends a quest objective//
        }

        if(activeNodes.Count == 0 && fireInHistory)
        {
            QuestManager.instance.SendObjectiveInputToQuests(Objectives.RemoveAllFire); //Sends a quest objective//
            fireInHistory = false;
        }
    }
}
