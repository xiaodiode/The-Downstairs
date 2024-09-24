using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class RingController : MonoBehaviour
{
    [SerializeField] private GameObject outerCircle;
    [SerializeField] private GameObject circleMask;
    [SerializeField] private SpriteRenderer circleSprite;
    private float thickness;
    private float timeToTarget;

    private Vector3 startScaleVector, targetScaleVector;
    private float elapsedTime;
    private Color successColour, failureColour;
    private float maskThickness;
    private bool inputReceived, fail;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ShrinkCircle(){
        elapsedTime = 0;

        while(elapsedTime < timeToTarget && !inputReceived){
            outerCircle.transform.localScale = Vector3.Lerp(startScaleVector, targetScaleVector, elapsedTime/timeToTarget);

            maskThickness = outerCircle.transform.localScale.x - thickness;

            circleMask.transform.localScale = new Vector3(maskThickness, maskThickness, maskThickness);
            
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
         if (inputReceived && !fail) {
            circleSprite.color = successColour;
        } else {
            circleSprite.color = failureColour;
        }

        
    }

    public void InitialiseRing(float startScale, float targetScale, float ringThickness, float QTETime, Color start, Color fail, Color success){
        startScaleVector = new Vector3(startScale,startScale,startScale);
        targetScaleVector = new Vector3(targetScale, targetScale, targetScale);
        thickness = ringThickness;
        timeToTarget = QTETime;
        maskThickness = outerCircle.transform.localScale.x - thickness;
        successColour = success;
        failureColour = fail;

        circleMask.transform.localScale = new Vector3(maskThickness, maskThickness, maskThickness);
        circleSprite.color = start;
        StartCoroutine(ShrinkCircle());

    }
    public void Pressed(bool QTEStatus)
    {
        inputReceived = true;
        fail = QTEStatus;
    }
    public float GetAvgX(){ 
        return (outerCircle.transform.localScale.x + maskThickness)/2;
    }
}
