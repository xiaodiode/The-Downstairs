using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRenderer : MonoBehaviour
{
    private LineRenderer ringRenderer;
    private Transform buttonTransform;
    [SerializeField] private int ringRes;
    //[SerializeField] private float thickness;
    private float ringRadMax;
    private float ringRadMin;
    private float QTELength;
    private bool inputReceived; 
    private bool fail;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
     private IEnumerator shrinkRing(float time)
    {
        float timeLeft = time;
        while (timeLeft >= 0.0f){
            if (inputReceived) {
                break;
            }
            timeLeft -= Time.deltaTime;
            DrawRing(ringRes, ringRadMax-((ringRadMax-ringRadMin)*(1-(timeLeft/time))));
            //Debug.Log(timeLeft);
            yield return new WaitForEndOfFrame();
        }
        if (inputReceived && !fail) {
            ringRenderer.SetColors(Color.green, Color.green);
        } else {
            ringRenderer.SetColors(Color.red, Color.red);

        }
    } 
    void DrawRing(int steps, float radius) 
    {
        ringRenderer.positionCount = steps;
        for (int i = 0; i < steps; i++)
        {
            float circumferenceProgress =  (float)i/(steps-1);
            float currentRadian = circumferenceProgress * 2 * Mathf.PI;
            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = new Vector3(x,y,0) + buttonTransform.position;

            ringRenderer.SetPosition(i, currentPosition);
        }
    }

    public void InitialiseRing(LineRenderer line, float radMax, float radMin, float length, Transform buttonPos)
    {
        ringRenderer = line;
        ringRadMax = radMax;
        ringRadMin = radMin;
        QTELength = length;
        buttonTransform = buttonPos;
        float alpha = 0.5f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(new Color(0f,58f,107f), 0.0f), new GradientColorKey(new Color(0f,23f,255f), 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        ringRenderer.colorGradient = gradient;
        StartCoroutine(shrinkRing(QTELength));
    }

    public void Pressed(bool QTEStatus)
    {
        inputReceived = true;
        fail = QTEStatus;
    }
}
