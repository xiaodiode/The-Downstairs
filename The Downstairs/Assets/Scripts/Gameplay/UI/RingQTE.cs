using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RingQTEs : MonoBehaviour
{

    [Header("InputList File")]
    [SerializeField] private TextAsset inputList;
    [Header("")]
    [SerializeField] private GameObject QTESuccessZone;
    [SerializeField] private GameObject QTEFailZone;
    [SerializeField] private Image buttonui;
    [SerializeField] private RingController ring;
    [SerializeField] private float ringStartRad;
    [SerializeField] private float ringEndRad;
    [Header ("In Coordinate Units, Reasonable Values Start From 0.0")]
    [SerializeField] private float ringThickness;
    [SerializeField] private float QTELength;
    [SerializeField] private float QTEZoneOuterRad;
    [SerializeField] private float QTEZoneInnerRad;
    [SerializeField] private Color ringColour, failColour, successColour;
 

    private bool fail;


    
    // Start is called before the first frame update
    void Start()
    {

        InitialiseCircle(QTEZoneOuterRad,QTEZoneInnerRad);
        ring.InitialiseRing(ringStartRad, ringEndRad, ringThickness, QTELength, ringColour, failColour, successColour);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Debug.Log("keypressed");
            calculateQTE();
            ring.Pressed(fail);
            Debug.Log("sent");
        }
    }

    void InitialiseCircle(float radius, float innerRadius){
        QTEFailZone.transform.localScale = new Vector3(innerRadius,innerRadius,innerRadius);
        QTESuccessZone.transform.localScale = new Vector3(radius,radius,radius);
    }
    
    private void calculateQTE() {        
        float ringPos = ring.GetAvgX();
        if (ringPos <= QTEZoneOuterRad && ringPos >= QTEZoneInnerRad) {
            fail = false;
        } else {
            fail = true;
        }
        Debug.Log("QTEFailed calculated" + fail);

    }
}
