using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class GameManager : MonoBehaviour
{
    [Header("UI Screens")]
    [SerializeField] private GameObject screensCanvas;
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

        OpenMainMenu();
        
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


    private void enableScreen(ScreenType screen, bool enable)
    {
        screensDict[screen].SetActive(enable);
    }

    public void OpenMainMenu()
    {
        switchScreen(ScreenType.MainMenu);
        screensCanvas.SetActive(true);

        AudioController.instance.playMainMenuMusic();
    }


    public void playGame()
    {
        screensCanvas.SetActive(false);

        switchScreen(ScreenType.Gameplay);

        AudioController.instance.playGameplayMusic();
        MetersController.instance.initializeMeters();
    }
    
}
