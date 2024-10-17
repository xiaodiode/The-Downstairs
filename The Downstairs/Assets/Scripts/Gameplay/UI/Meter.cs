using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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
    private bool newMultiplier;

    private float currValueChange;
    private bool newValueChange;

    public bool toiletEffect;

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
    }

    public IEnumerator decreaseMeter()
    {
        isIdle = false;

        float secondsPassed = 0;

        float newSecondsPassed = secondsPassed * currMultiplier;
        float newSecondsToEmpty = secondsToEmpty * currMultiplier;

        while(newSecondsPassed <= newSecondsToEmpty)
        {
            if(GameManager.instance.gamePaused) yield return null;
            
            else
            {
                if(newMultiplier)
                {
                    newMultiplier = false;

                    newSecondsPassed = secondsPassed * currMultiplier; 
                    newSecondsToEmpty = secondsToEmpty * currMultiplier;
                }

                if(newValueChange)
                {
                    newValueChange = false;

                    newSecondsPassed += newSecondsToEmpty * (currValueChange/meter.maxValue);
                }

                meter.value = meter.maxValue * (1 - (newSecondsPassed/newSecondsToEmpty));

                newSecondsPassed += Time.deltaTime;

                meterValue.text = Mathf.CeilToInt(meter.value).ToString();

                yield return null;                
            }

            
        }

        isEmpty = true;

        Debug.Log("meter is empty");
    }

    public void resetMeter(float newSecondsToEmpty)
    {
        isEmpty = false;
        isIdle = true;

        meter.value = meter.maxValue;

        secondsToEmpty = newSecondsToEmpty;

        currMultiplier = 1;
        newMultiplier = false;
    }

    public void makeMeterFull()
    {
        StopCoroutine(decreaseMeter());

        isIdle = true;

        meter.value = meter.maxValue;
    }

    public void changeByAmount(float amount)
    {
        currValueChange = amount;
        newValueChange = true;
    }


    public void changeMultiplier(float multiplier)
    {
        currMultiplier = multiplier;
        newMultiplier = true;

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

}
