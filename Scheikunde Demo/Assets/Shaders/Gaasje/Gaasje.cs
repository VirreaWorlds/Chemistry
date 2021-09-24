using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaasje : MonoBehaviour
{
    public float heatingSpeed;
    public float coolingSpeed;

    public MeshRenderer mR;

    public float currentHeat;

    public FlameManager burner;

    private void Update() {
        if (burner != null) {
            if (burner.hitObject == this.gameObject) {
                currentHeat = Mathf.MoveTowards(currentHeat, 1, heatingSpeed * Time.deltaTime);
            } else {
                currentHeat = Mathf.MoveTowards(currentHeat, 0, coolingSpeed * Time.deltaTime);
            }
        } else {
            currentHeat = Mathf.MoveTowards(currentHeat, 0, coolingSpeed * Time.deltaTime);
        }

        mR.material.SetVector("_MaskPosition", transform.position);
        mR.material.SetVector("_RadiusHardness", new Vector4(currentHeat, mR.material.GetVector("_RadiusHardness").y, 0, 0));

        burner = null;
    }
}
