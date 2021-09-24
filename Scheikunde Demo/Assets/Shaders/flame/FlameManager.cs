using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameManager : MonoBehaviour
{
    public bool isOn;

    public Transform flameStartPoint;
    public float flameLength = 0.1f;
    public float lerpAlphaPercent;
    public float currentDistance;
    public MeshRenderer flameMR;

    public GameObject flameJetObject;
    public GameObject flameImpactObject;
    public ScaleInfo flameJet, flameImpact;

    public AnimationCurve alphaCurve;


    public GameObject hitObject;
    public GameObject flame;
    public LayerMask flameMask;


    private void Update() {

        if (!isOn) {
            flame.SetActive(false);
            return;
        } else {
            flame.SetActive(true);
        }


        Ray r = new Ray(flameStartPoint.position, flameStartPoint.forward);

        if (Physics.Raycast(r, out var hit, flameLength, flameMask)) {
            UpdateLerpedObjects(lerpAlphaPercent);

            currentDistance = Vector3.Distance(flameStartPoint.position, hit.point);
        } 
        else 
        {
            UpdateLerpedObjects(1);

            currentDistance = flameLength;
        }

        if (Physics.Raycast(r, out var hit2, flameLength * 2, flameMask)) {
            hitObject = hit2.collider.gameObject;
            hitObject.GetComponent<Gaasje>().burner = this;
        } else {
            hitObject = null;
        }

        lerpAlphaPercent = currentDistance / flameLength;

        

    }

    private void OnDrawGizmos() {

        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(flameStartPoint.position, flameStartPoint.position + new Vector3(0, currentDistance, 0));

    }

    public void UpdateLerpedObjects(float a) {
        flameJetObject.transform.localPosition = flameJet.EvaluatePos(a);
        flameJetObject.transform.localScale = flameJet.EvaluateScale(a);

        flameImpactObject.transform.localPosition = flameImpact.EvaluatePos(a);
        flameImpactObject.transform.localScale = flameImpact.EvaluateScale(a);

        Vector3 v = new Vector3(2, a * 2, 2);

        flameMR.material.SetFloat("_OpacityCutoff", alphaCurve.Evaluate(a));

        //flameMR.material.SetVector("_MaskSize", v);
        //flameMR.material.SetVector("_BoxPos", flameStartPoint.position);
        
    }

    [System.Serializable]
    public struct ScaleInfo
    {
        public Vector3 pos, scale;
        public Vector3 pos1, scale1;

        public Vector3 EvaluatePos(float alpha) {
            return Vector3.Lerp(pos, pos1, alpha);
        }

        public Vector3 EvaluateScale(float alpha) {
            return Vector3.Lerp(scale, scale1, alpha);
        }
    }
}
