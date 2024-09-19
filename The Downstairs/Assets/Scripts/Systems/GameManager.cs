using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private GameObject screensCanvas;

    [Header("Gameplay")]
    [SerializeField] private GameObject gameplayCanvas;

    [Header("Systems")]
    [SerializeField] private MouseController mouseController;
    [SerializeField] private AudioController audioController;

    private bool inGame;
    // Start is called before the first frame update
    void Start()
    {
        inGame = false;
        switchMode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void switchMode(){
        if(inGame){
            screensCanvas.SetActive(false);
            gameplayCanvas.SetActive(true);

            audioController.playGameplayMusic();
        }
        else{
            screensCanvas.SetActive(true);
            gameplayCanvas.SetActive(false);

            audioController.playMainMenuMusic();
        }
    }

    public void playGame(){
        inGame = true;
        switchMode();
    }
    
}
