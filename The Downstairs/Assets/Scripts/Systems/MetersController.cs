using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MetersController : MonoBehaviour
{
    [Header("Hunger Meter")]
    [SerializeField] public Meter hungerMeter;
    [SerializeField] public float hungerSecondsToEmpty;

    [Header("Thirst Meter")]
    [SerializeField] public Meter thirstMeter;
    [SerializeField] public float thirstSecondsToEmpty;

    [Header("Toilet Meter")]
    [SerializeField] public Meter toiletMeter;
    [SerializeField] public float toiletSecondsToEmpty;

    [Header("Sanity Meter")]
    [SerializeField] public Meter sanityMeter;
    [SerializeField] public float sanitySecondsToEmpty;


    public static MetersController instance {get; private set;}

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null && instance != this){
            Destroy(this);
        }
        else{
            instance = this;
        }

    }

    // Update is called once per frame
    void Update()
    {
        checkToilet();
    }

    public void initializeMeters(){
        hungerMeter.initializeMeter(hungerSecondsToEmpty);
        thirstMeter.initializeMeter(thirstSecondsToEmpty);
        toiletMeter.initializeMeter(toiletSecondsToEmpty);
        sanityMeter.initializeMeter(sanitySecondsToEmpty);
    }

    public void adjustMeters(float hungerchange, float thirstChange, float toiletChange, float sanityChange){
        if(hungerchange != 0){
            hungerMeter.changeByAmount(hungerchange);
        }
        
        if(thirstChange != 0){
            thirstMeter.changeByAmount(thirstChange);
        }
        
        if(toiletChange != 0){
            toiletMeter.changeByAmount(toiletChange);
        }

        if(sanityChange != 0){
            sanityMeter.changeByAmount(sanityChange);
        }
        
    }

    private void checkToilet(){
        if(toiletMeter.isEmpty && !thirstMeter.multiplierUpdated)
        {
            Debug.Log("toilet is empty");
            thirstMeter.changeDecreaseMultiplier(thirstSecondsToEmpty, 0.5f);
        }
    }

    
}
