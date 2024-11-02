using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Meter : MonoBehaviour
{
    public bool isEmpty, isIdle;
    [SerializeField] private Slider meter;
    [SerializeField] private TextMeshProUGUI meterValue; //debugging

    private List<int> intervals = new();
    public List<bool> isTriggered = new();

    
    public bool dataReady;
    private float secondsToEmpty;

    private float currMultiplier;

    private float currValueChange;
    private bool newValueChange;

    public bool toiletEffect;

    private float secondsPassed;

    private float oldValue = 100;

    // Start is called before the first frame update
    void Start()
    {
        toiletEffect = false;
        dataReady = false;

        for(int interval = 0; interval < 100; interval += 10)
        {
            intervals.Add(interval);
            isTriggered.Add(false);
        }

        dataReady = true;

    }

    // Update is called once per frame
    void Update()
    {
        checkDialogueIncrements();

        testMeter();
    }

    public IEnumerator decreaseMeter()
    {
        isIdle = false;

        secondsPassed = 0;

        while(secondsPassed <= secondsToEmpty && !isIdle)
        {
            if(GameManager.instance.gamePaused) yield return null;
            
            else
            {
                if(newValueChange)
                {
                    newValueChange = false;

                    secondsPassed -= secondsToEmpty * (currValueChange/meter.maxValue);

                }
                else 
                {
                    meter.value = meter.maxValue * (1 - (secondsPassed/secondsToEmpty));

                    secondsPassed += Time.deltaTime * currMultiplier;

                    meterValue.text = Mathf.FloorToInt(meter.value).ToString();

                    yield return null;
                }
                                
            }

        }

        if(isIdle) Debug.Log("full");

        else
        {
            isEmpty = true;

            meter.value = 0;
            meterValue.text = Mathf.FloorToInt(meter.value).ToString();

            Debug.Log("meter is empty");
        }


        
    }

    public void resetMeter(float newSecondsToEmpty)
    {
        isEmpty = false;
        isIdle = true;

        meter.value = meter.maxValue;

        secondsToEmpty = newSecondsToEmpty;

        currMultiplier = 1;
    }

    public void makeMeterFull()
    {
        isIdle = true;

        meter.value = meter.maxValue;

        meterValue.text = Mathf.FloorToInt(meter.value).ToString();
    }

    public void changeByAmount(float amount)
    {
        currValueChange = amount;
        newValueChange = true;
    }


    public void changeMultiplier(float multiplier)
    {
        currMultiplier = multiplier;

        toiletEffect = true;
    }

    private void checkDialogueIncrements()
    {
        for(int i = 0; i < intervals.Count; i++)
        {
            if(meter.value < intervals[i] && oldValue >= intervals[i])
            {
                isTriggered[i] = true;
                oldValue = meter.value;
            }
        }

    }

    private void testMeter()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            changeByAmount(-10);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            changeMultiplier(2f);
        }
    }

}
