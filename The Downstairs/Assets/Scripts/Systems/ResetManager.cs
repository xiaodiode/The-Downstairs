using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerReset
{
    public GameObject playerObject;
    public Transform resetPosition;
}

public class ResetManager : MonoBehaviour
{
    [SerializeField] private List<PlayerReset> playerResets = new();

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

        ClockController.instance.resetClockHands(ClockController.instance.startHour);

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

        GameManager.instance.isNewGame = false;
        GameManager.instance.gamePaused = true;
        GameManager.instance.gameReset = true;
    }

    private void resetPlayer()
    {
        foreach(PlayerReset player in playerResets)
        {
            player.playerObject.transform.position = player.resetPosition.position;
        }
    }
}
