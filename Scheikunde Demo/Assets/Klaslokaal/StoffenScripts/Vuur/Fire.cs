using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

                     private float timeElapsed;
    [SerializeField] private float lerpDuration;
    public bool CanDo;


    private void OnTriggerStay(Collider other)
    {

        if (CanDo)
        {
             if (other.GetComponent<StofHolderInside>() != null)
             {
                var Stoftemp = other.GetComponent<StofHolderInside>();
                Stoftemp.onfire = true;
                float b = 2000;
                timeElapsed += Time.deltaTime;
                Stoftemp.temperatuur = Mathf.Lerp(Stoftemp.temperatuur, b, timeElapsed / lerpDuration);

            }
        } else
        {
            if (other.GetComponent<StofHolderInside>() != null)
            {
                var Stoftemp = other.GetComponent<StofHolderInside>();
                Stoftemp.onfire = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<StofHolderInside>() != null)
        {
            var Stoftemp = other.GetComponent<StofHolderInside>();
            Stoftemp.onfire = false;
        }
    }
}
