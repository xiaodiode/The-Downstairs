using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MeterQTE : MonoBehaviour
{
    public float time, speed, cursorMax;
    // [Header ("Percentage float from 0.0-1.0")]
    [SerializeField] [Range(0.0f, 1.0f)] public float start;
    [SerializeField] [Range(0.0f, 1.0f)] public float end;

    // public float start, end;
    [SerializeField] private Slider QTEGreen;
    [SerializeField] private RectTransform QTEGreenRect;
    [SerializeField] private Slider QTERed;
    [SerializeField] private Slider cursorSlider;
    
    [SerializeField] private Meter timer;
    [SerializeField] private Image cursor;
    [SerializeField] private Image buttonui;
    // Start is called before the first frame update
    private float convertedStart, convertedEnd;
    private bool success, input;
    void Start()
    {
       initialiseQTEMeter();
       initialiseTimer();
       StartCoroutine(cycleCursor());
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Debug.Log("keypressed");
            calculateQTE();
            input = true;
            Debug.Log("sent");
        }
        if (!input){
            cursorSlider.value += speed;
        }
    }
    
    private void calculateQTE() //When Input is received, call this function to determine if the QTE was a success; 
                                //measurees if the cursor is within bounds of success area
    { 
        if (cursorSlider.value >= convertedStart && cursorSlider.value <= convertedEnd){
            success = true;
        }
    }

    private IEnumerator cycleCursor()
    {
        float modifier = speed;
        while (!timer.isEmpty && !input){
            if (cursorSlider.value <= 0 || cursorSlider.value >= cursorMax) {
                speed *= -1;
            }
            cursorSlider.value += speed;
            yield return new WaitForEndOfFrame();
        }
        
    } 

    private void initialiseTimer(){
        timer.initializeMeter(time);
        timer.startDecreasing();
    }

    private void initialiseQTEMeter() {
        convertedStart = start * cursorMax;
        convertedEnd = end * cursorMax;
        cursorSlider.maxValue = cursorMax;
        cursorSlider.value = 0;
        Vector3[] vectGreen = new Vector3[4];
        Vector3[] vectRed = new Vector3[4];
        QTEGreenRect.GetWorldCorners(vectGreen);
        QTEGreenRect.GetWorldCorners(vectRed);
    }

}
