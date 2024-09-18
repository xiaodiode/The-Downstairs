using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer ringRenderer;
    [SerializeField] private Transform buttonPrompt;
    [SerializeField] private int ringRes;
    [SerializeField] private float ringRad;
    // Start is called before the first frame update
    void Start()
    {
        DrawRing(ringRes, ringRad);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /* private IEnumerator shrinkRing()
    {

    } */
    void DrawRing(int steps, float radius) 
    {
        ringRenderer.positionCount = steps;
        for (int i = 0; i < steps; i++)
        {
            float circumferenceProgress =  (float)i/steps;
            float currentRadian = circumferenceProgress * 2 * Mathf.PI;
            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = new Vector3(x,y,0) + buttonPrompt.position;

            ringRenderer.SetPosition(i, currentPosition);
        }
    }
}
