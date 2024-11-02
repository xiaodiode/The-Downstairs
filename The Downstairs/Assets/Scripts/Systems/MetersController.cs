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

    public bool lockBedroom;

    public bool pauseMeters;

    private List<Meter> pausedMeters = new();

    public static MetersController instance {get; private set;}

    void Awake()
    {
        if(instance != null && instance != this){
            Destroy(this);
        }
        else{
            instance = this;
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        lockBedroom = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        // if(SidescrollPlayerController.instance.gameObject.activeInHierarchy){
        //     resetSanityMeter();
        // }

        checkTriggers();
    }

    public void resetMeters()
    {
        hungerMeter.resetMeter(hungerSecondsToEmpty);
        thirstMeter.resetMeter(thirstSecondsToEmpty);
        toiletMeter.resetMeter(toiletSecondsToEmpty);
        sanityMeter.resetMeter(sanitySecondsToEmpty);

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

    public void resetSanityMeter()
    {
        if(!lockBedroom)
        {
            // Debug.Log("resetting sanity");
            sanityMeter.makeMeterFull();
            sanityMeter.resetMeter(sanitySecondsToEmpty);
        }
    }

    public void resetHungerMeter()
    {
        hungerMeter.makeMeterFull();
        hungerMeter.resetMeter(hungerSecondsToEmpty);
    }

    private void checkTriggers()
    {
        if(sanityMeter.dataReady)
        {
            checkHunger();
            checkThirst();
            checkToilet();
            checkSanity();
        }
    }

    private void checkHunger()
    {
        if(hungerMeter.isTriggered[2])
        {
            hungerMeter.isTriggered[2] = false;
            Dialogue.instance.triggerHungerDialogue(20);
        }
        else if(hungerMeter.isTriggered[5])
        {
            hungerMeter.isTriggered[5] = false;
            Dialogue.instance.triggerHungerDialogue(50);
        }
    }

    private void checkThirst()
    {
        if(thirstMeter.isTriggered[2])
        {
            thirstMeter.isTriggered[2] = false;
            Dialogue.instance.triggerThirstDialogue(20);
        }
        else if(thirstMeter.isTriggered[5])
        {
            thirstMeter.isTriggered[5] = false;
            Dialogue.instance.triggerThirstDialogue(50);
        }
    }

    private void checkToilet()
    {
        if(toiletMeter.isTriggered[2])
        {
            toiletMeter.isTriggered[2] = false;
            Dialogue.instance.triggerToiletDialogue(20);
        }
        else if(toiletMeter.isTriggered[5])
        {
            toiletMeter.isTriggered[5] = false;
            Dialogue.instance.triggerToiletDialogue(50);
        }
    }

    private void checkSanity()
    {
        if(sanityMeter.isTriggered[1])
        {
            sanityMeter.isTriggered[1] = false;
            Dialogue.instance.triggerSanityDialogue(10);
        }
        else if(toiletMeter.isTriggered[5])
        {
            sanityMeter.isTriggered[5] = false;
            Dialogue.instance.triggerSanityDialogue(50);
        }
    }

}
