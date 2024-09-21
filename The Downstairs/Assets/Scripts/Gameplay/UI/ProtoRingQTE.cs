using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProtoRingQTEs : MonoBehaviour
{
    public float Time;

    [Header("InputList File")]
    [SerializeField] private TextAsset inputList;
    [SerializeField] private Image buttonui;
    [SerializeField] private LineRenderer ringRenderer;
    [SerializeField] private RingRenderer ring;
    [SerializeField] private RectTransform circle;
    [SerializeField] private RectTransform innerCircle;
    [SerializeField] private float QTELength;
    [SerializeField] private float QTEZoneOuterRad;
    [SerializeField] private float QTEZoneInnerRad;
 

    private bool fail;


    
    // Start is called before the first frame update
    void Start()
    {

        InitialiseCircle(QTEZoneOuterRad,QTEZoneInnerRad);
        ring.InitialiseRing(ringRenderer, 2f, 0.5f, QTELength, this.transform);
        
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
        circle.sizeDelta = new Vector2(radius,radius);
        innerCircle.sizeDelta = new Vector2(innerRadius,innerRadius);
    }
    
    private void calculateQTE() {
        float ringX = ringRenderer.GetPosition(0).x;
        Vector3[] v1 = new Vector3[4];
        Vector3[] v2 = new Vector3[4];
        circle.GetWorldCorners(v1);
        innerCircle.GetWorldCorners(v2);
        float outerX = v1[2].x;
        float innerX = v2[2].x;
        Debug.Log("Ring" + ringX);
        Debug.Log("Outer" + outerX);
        Debug.Log("Inner" + innerX);
        if (ringX <= outerX && ringX >= innerX) {
            fail = false;
        } else {
            fail = true;
        }
        Debug.Log("QTEFailed calculated" + fail);

    }
}
