using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ObjectReset
{
    public GameObject gameObject;
    public Transform resetPosition;
}

public class ResetManager : MonoBehaviour
{
    [SerializeField] private List<ObjectReset> objectResets = new();

    public static ResetManager instance {get; private set;}

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HardReset()
    {
        resetPlayer();

        MetersController.instance.resetMeters();
        CandleController.instance.resetCandles();
        MatchController.instance.resetMatches();

        ClockController.instance.resetClockHands(ClockController.instance.tutorialHour);

        Dialogue.instance.resetDialogue();

        Fridge.instance.resetFridge();
        WaterPitcher.instance.resetWater();
        Laundry.instance.resetLaundry();

        GameManager.instance.nightCount = 1;
        GameManager.instance.isNewGame = true;
        GameManager.instance.gamePaused = true;
        GameManager.instance.gameReset = true;

        GameOver.instance.isGameOver = false;
    }

    public void SoftReset()
    {
        resetPlayer();

        MetersController.instance.resetMeters();

        ClockController.instance.resetClockHands(ClockController.instance.resetHour);

        Dialogue.instance.resetDialogue();

        GameManager.instance.isNewGame = false;
        GameManager.instance.gamePaused = true;
        GameManager.instance.gameReset = true;
    }

    private void resetPlayer()
    {
        foreach(ObjectReset obj in objectResets)
        {
            obj.gameObject.transform.position = obj.resetPosition.position;
        }
    }
}
