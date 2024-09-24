using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenuScreen;

    [Header("Gameplay")]
    [SerializeField] private GameObject gameplayScreen;

    private Dictionary<ScreenType, GameObject> screensDict;

    public enum ScreenType
    {
        MainMenu,
        Gameplay
    }

    [Header("Systems")]
    [SerializeField] private MouseController mouseController;

    private bool inGame;
    // Start is called before the first frame update
    void Start()
    {
        screensDict = new()
        {
            { ScreenType.MainMenu, mainMenuScreen },
            { ScreenType.Gameplay, gameplayScreen},
        };

        switchScreen(ScreenType.MainMenu);
        AudioController.instance.playMainMenuMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void switchScreen(ScreenType newScreen)
    {

        foreach (ScreenType screen in System.Enum.GetValues(typeof(ScreenType)))
        {
            screensDict[screen].SetActive(false);
        }

        screensDict[newScreen].SetActive(true);
    }


    public void playGame()
    {
        switchScreen(ScreenType.Gameplay);

        AudioController.instance.playGameplayMusic();
        MetersController.instance.initializeMeters();
    }
    
}
