using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Meter : MonoBehaviour
{
    public bool isEmpty, meterEnabled;
    [SerializeField] private Slider meter;
    [SerializeField] private TextMeshProUGUI meterValue; //debugging
    
    private float secondsToEmpty;
    private float startTime, timePassed;
    public bool hit80, hit50, hit20, hit10;
    public bool toiletEffect;

    private float oldValue = 100;

    // Start is called before the first frame update
    void Start()
    {
        toiletEffect = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isEmpty && meterEnabled){
            decreaseMeter();
        }  

        //checkIncrements();
        if (meter.value < 50 && oldValue >= 50){
            hit50 = true;
        }
    }

    public void initializeMeter(float newSecondsToEmpty){
        isEmpty = false;
        meterEnabled = false;

        updateMeterUI(meter.maxValue);

        secondsToEmpty = newSecondsToEmpty;

        //debugging
        // meter.value = 80;
        // startDecreasing();

        // StartCoroutine(waitForSeconds(2));
    }

    public void startDecreasing(){
        startTime = Time.time;

        meterEnabled = true;
    }

    public void stopDecreasing(){

        meterEnabled = false;

    }

    public void makeMeterFull(){
        stopDecreasing();

        updateMeterUI(meter.maxValue);

        // toiletEffect = false;
    }

    public void changeByAmount(float amount){
        
        stopDecreasing();

        if(amount < 0){
            updateMeterUI(meter.value + amount);

            if(meter.value < 0){
                isEmpty = true;
                updateMeterUI(0);
            }

            startDecreasing();
            
        }
        else{
            updateMeterUI(meter.value + amount);

            if(meter.value > meter.maxValue){
                updateMeterUI(meter.maxValue);
            }
        }
    }

    private void decreaseMeter()
    {
        timePassed = Time.time - startTime;

        if(meter.value > 0){
            updateMeterUI(meter.maxValue*(1 - (timePassed/secondsToEmpty)));
        }
        else{
            updateMeterUI(0);
            isEmpty = true;

            stopDecreasing(); 
        }
    }   

    public void changeDecreaseMultiplier(float originalRate, float multiplier)
    {
        startTime += timePassed*multiplier;
        secondsToEmpty = originalRate * multiplier;

        toiletEffect = true;
    }

    private IEnumerator waitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        //debugging
        changeByAmount(20);
        startDecreasing();
    }

    private void updateMeterUI(float newValue)
    {
        if(newValue == 0){
            isEmpty = true;
        }

        meter.value = newValue;
        meterValue.text = Mathf.CeilToInt(meter.value).ToString(); //TextGUI update
    }

    public float getValue()
    {
        return meter.value;
    }

    private void checkIncrements()
    {
        if(meter.value <= 10)
        {
            hit10 = true;
            hit20 = false;
            hit50 = false;
            hit80 = false;
        } 
        else if(meter.value <= 20)
        {
            hit10 = false;
            hit20 = true;
            hit50 = false;
            hit80 = false;
        } 
        else if(meter.value <= 50)
        {
            hit10 = false;
            hit20 = false;
            hit50 = true;
            hit80 = false;
        } 
        else if(meter.value <= 80)
        {
            hit10 = false;
            hit20 = false;
            hit50 = false;
            hit80 = true;
        } 
    }
}
