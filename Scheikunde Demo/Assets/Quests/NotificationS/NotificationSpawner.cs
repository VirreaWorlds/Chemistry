using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationSpawner : MonoBehaviour
{
    public List<Notification> queue;
    public NotificationSpawner instance;
    public GameObject notifPrefab;
    public Transform prefabTarget;
    public QuestManager qM;

    private void Start()
    {
        instance = this;

        qM = this.GetComponent<QuestManager>();
        qM.nSpawner = this;
    }

    public void Update()
    {
        
    }

    public void CreateNewNotification(QuestObjective q)
    {
        Debug.Log("Adding Notification for : " + q.ToString());
        //this needs to add a new notification to the queue

        var g = Instantiate(notifPrefab, prefabTarget, true);
        g.transform.position = prefabTarget.position;
        g.transform.rotation = prefabTarget.rotation;
        g.transform.SetParent(qM.gameObject.transform);
        g.name = "[NotificationPrefab]:[" + q.objectiveName + "]:[Objective]";

        var n = g.GetComponent<Notification>();
        n.spawner = this;
        n.message = q.objectiveName;
        n.target = prefabTarget.gameObject;

        queue.Add(n);

        UpdateQueue();
    }

    public void CreateNewNotification(Quest q)
    {
        Debug.Log("Adding Notification for : " + q.ToString());
        //this needs to add a new notification to the queue

        var g = Instantiate(notifPrefab, prefabTarget, true);
        g.transform.position = prefabTarget.position;
        g.transform.rotation = prefabTarget.rotation;
        g.transform.SetParent(qM.gameObject.transform);
        g.name = "[NotificationPrefab]:[" + q.questName + "]:[Quest]";

        var n = g.GetComponent<Notification>();
        n.spawner = this;
        n.message = q.questName;
        n.target = prefabTarget.gameObject;
        n.isQuest = true;

        queue.Add(n);

        UpdateQueue();
    }

    public void UpdateQueue()
    {
        if(queue.Count > 0)
        {
            queue[0].isFirstInQueue = true;
        }
    }
}