using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    [Tooltip("The time of the animation in seconds")]
    public float duration = 2.167f;
    public float t;

    public string message = "Null";

    public NotificationSpawner spawner;

    public bool isFirstInQueue;
    public bool isQuest;

    public GameObject target;

    public float speed = 2;

    public Animator anim;

    public Text notificationText;
    public Text notificationType;

    [Space]
    public Color objectiveColor;
    public Color questColor;

    public SpriteRenderer typeRen;

    public GameObject questOnlyEffect;

    public float questOnlyDurationMultiplier = 1.5f;

    private void Update()
    {
        QueueTest();

        MoveInFrontOfPlayer();
    }

    public void RemoveSelfFromList()
    {
        spawner.queue.Remove(this);
        spawner.UpdateQueue();
        Destroy(gameObject);
    }

    public void LifeTime()
    {
        if (t == 0) //runs only once on the first frame it's in first place
        {
            anim = GetComponentInChildren<Animator>();
            anim.Play("NotificationAnimation");

            notificationText.text = message;

            if (isQuest)
            {
                //AudioManager.instance.quest.Play();

                notificationType.text = "< Opdracht voltooid >";
                typeRen.color = questColor;
                questOnlyEffect.SetActive(true);

                anim.speed = 1 / questOnlyDurationMultiplier;
                duration = duration * questOnlyDurationMultiplier;
            }
            else
            {
                //AudioManager.instance.objective.Play();

                notificationType.text = "• Taak voltooid •";
                typeRen.color = objectiveColor;
                anim.speed = 1;
            }
        }

        t += Time.deltaTime;

        if (t > duration)
        {
            RemoveSelfFromList();
        }
    }

    public void QueueTest()
    {
        if (isFirstInQueue)
        {
            LifeTime();
        }
    }

    public void SetVisuals()
    {

    }

    public void MoveInFrontOfPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, speed * Time.deltaTime * 100);
    }
}
