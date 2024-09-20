using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Meter : MonoBehaviour
{
    public bool isEmpty, meterEnabled;
    [SerializeField] private Slider meter;
    [SerializeField] private TextMeshProUGUI meterValue; //debugging
    
    private float secondsToEmpty;
    private float startTime, timePassed;
    private float currValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isEmpty && meterEnabled){
            decreaseMeter();
        }  
    }

    public void initializeMeter(float newSecondsToEmpty){
        isEmpty = false;
        meterEnabled = false;

        currValue = 100;
        updateMeterUI(currValue);

        secondsToEmpty = newSecondsToEmpty;

        //debugging
        // meter.value = 80;
        startDecreasing();

        // StartCoroutine(waitForSeconds(2));
    }

    public void startDecreasing(){
        startTime = Time.time;
        currValue = meter.value;

        meterEnabled = true;
    }

    public void stopDecreasing(){

        meterEnabled = false;

    }

    public void makeMeterFull(){
        stopDecreasing();

        updateMeterUI(meter.maxValue);
    }

    public void changeByAmount(float amount){
        
        stopDecreasing();

        if(amount < 0){
            updateMeterUI(meter.value - amount);

            if(meterEnabled){
                startDecreasing();
            }
        }
        else{
            updateMeterUI(meter.value + amount);

            if(meter.value > meter.maxValue){
                updateMeterUI(meter.maxValue);
            }
        }
    }

    private void decreaseMeter(){
        timePassed = (secondsToEmpty*(meter.maxValue - currValue)/meter.maxValue) + (Time.time - startTime);

        if(meter.value >= 0){
            updateMeterUI(meter.maxValue*(1 - (timePassed/secondsToEmpty)));
        }
        else{
            updateMeterUI(0);
            isEmpty = true;

            stopDecreasing(); 
        }
        
    }   

    private IEnumerator waitForSeconds(float seconds){
        yield return new WaitForSeconds(seconds);

        //debugging
        changeByAmount(20);
        startDecreasing();
    }

    private void updateMeterUI(float newValue){
        meter.value = newValue;
        //meterValue.text = Mathf.CeilToInt(meter.value).ToString(); //TextGUI update
    }
}
