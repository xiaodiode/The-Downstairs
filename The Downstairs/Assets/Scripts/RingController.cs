using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class RingController : MonoBehaviour
{
    [SerializeField] private GameObject outerCircle;
    [SerializeField] private GameObject circleMask;
    [SerializeField] private float thickness;
    [SerializeField] private float targetScale;
    [SerializeField] private float timeToTarget;

    private Vector3 startScaleVector, targetScaleVector;
    private float elapsedTime;
    private float maskThickness;
    // Start is called before the first frame update
    void Start()
    {
        startScaleVector = outerCircle.transform.localScale;
        targetScaleVector = new Vector3(targetScale, targetScale, targetScale);

        maskThickness = outerCircle.transform.localScale.x - thickness;

        circleMask.transform.localScale = new Vector3(maskThickness, maskThickness, maskThickness);

        StartCoroutine(shrinkCircle());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator shrinkCircle(){
        elapsedTime = 0;

        while(elapsedTime < timeToTarget){
            outerCircle.transform.localScale = Vector3.Lerp(startScaleVector, targetScaleVector, elapsedTime/timeToTarget);

            maskThickness = outerCircle.transform.localScale.x - thickness;

            circleMask.transform.localScale = new Vector3(maskThickness, maskThickness, maskThickness);
            
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        
    }
}
