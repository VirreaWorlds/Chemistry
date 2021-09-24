using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public List<GameObject> anims;

    public void StartSpecific(int id)
    {
        if (id <= anims.Count - 1)
        {
            foreach (var a in anims)
            {
                a.SetActive(false);
            }

            anims[id].SetActive(true);
        }
        else
        {
            Debug.LogWarning("A button tried to select a non-existant animation");
        }
    }
}
