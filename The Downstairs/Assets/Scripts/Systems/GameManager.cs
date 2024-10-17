using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isNewGame;
    public bool gamePaused;
    public bool gameReset;

    [Header("UI Screens")]
    [SerializeField] public GameObject screensCanvas;
    [SerializeField] private GameObject mainMenuScreen;
    

    [Header("Gameplay")]
    [SerializeField] public GameObject gameCanvas;
    [SerializeField] private GameObject cutsceneScreen;
    [SerializeField] private GameObject continueScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject gameplayScreen;

    [Header("Game Properties")]
    [SerializeField] public int nightCount;

    public Dictionary<ScreenType, GameObject> screensDict;

    public enum ScreenType
    {
        MainMenu,
        Cutscene,
        Gameplay,
        Continue,
        GameOver
    }

    public static GameManager instance {get; private set;}

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
        screensDict = new()
        {
            { ScreenType.MainMenu, mainMenuScreen },
            { ScreenType.Gameplay, gameplayScreen },
            { ScreenType.Cutscene, cutsceneScreen },
            { ScreenType.Continue, continueScreen },
            { ScreenType.GameOver, gameOverScreen }
        };

        OpenMainMenu();

        // enableGame(true);
        // triggerGameOver();

        nightCount = 1;

        // isNewGame = true; 
        isNewGame = false; // change to false to skip intro cutscene

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!gamePaused) pauseGame();

            else resumeGame();
        }
    }

    public void switchScreen(ScreenType newScreen)
    {

        foreach (ScreenType screen in System.Enum.GetValues(typeof(ScreenType)))
        {
            screensDict[screen].SetActive(false);
        }

        screensDict[newScreen].SetActive(true);
    }


    public void enableScreen(ScreenType screen, bool enable)
    {
        screensDict[screen].SetActive(enable);
    }

    public void OpenMainMenu()
    {
        enableGame(false);
        switchScreen(ScreenType.MainMenu);

        resetGame();

        MainMenu.instance.startMainMenu();
    }

    public void enableGame(bool enable)
    {
        screensCanvas.SetActive(!enable);
        gameCanvas.SetActive(enable);
    }


    public IEnumerator initializeGameStart()
    {
        enableGame(true);
        switchScreen(ScreenType.Gameplay);

        playGame();

        AudioController.instance.playGameplayMusic();
        MetersController.instance.resetMeters();

        RendererController.instance.toggleGameRenderer(RendererController.RendererType.Light2D);

        StartCoroutine(Timer.instance.startTimer());
        ClockController.instance.startRotating();

        StartCoroutine(MetersController.instance.hungerMeter.decreaseMeter());
        StartCoroutine(MetersController.instance.thirstMeter.decreaseMeter());
        StartCoroutine(MetersController.instance.toiletMeter.decreaseMeter());
        StartCoroutine(MetersController.instance.sanityMeter.decreaseMeter());

        StartCoroutine(CandleController.instance.FindActiveLight());

        yield return new WaitForSeconds(3);

    }

    public void playIntroCutscene()
    {
        StartCoroutine(CutscenesController.instance.playIntroCutscene());   
    }

    public void playStairsCutscene()
    {
        StartCoroutine(CutscenesController.instance.playStairsCutscene());
    }

    public void openContinueScreen()
    {
        resetGame();

        enableScreen(ScreenType.Continue, true);
        RendererController.instance.toggleGameRenderer(RendererController.RendererType.VHS);

        ContinueScreen.instance.updateText();

    }

    public void continueGame()
    {
        playGame();

        enableScreen(ScreenType.Continue, false);
        RendererController.instance.toggleGameRenderer(RendererController.RendererType.Light2D);

        isNewGame = false;
        nightCount++;
        MetersController.instance.resetMeters();

        ClockController.instance.resetClockHands(ClockController.instance.resetHour);

        ClockController.instance.startRotating();
    }

    public void returnToMainMenu()
    {
        OpenMainMenu();
    }

    public void triggerGameOver()
    {
        pauseGame();

        enableScreen(ScreenType.GameOver, true);

        StartCoroutine(GameOver.instance.fadeIn());
    }

    public void increaseNightCount()
    {
        nightCount++;
        isNewGame = false;
    }

    public void pauseGame()
    {
        gamePaused = true;
    }

    public void resumeGame()
    {
        gamePaused = false;
    }
    
    public void resetGame()
    {
        gamePaused = true;
        gameReset = true;
    }

    public void playGame()
    {
        gameReset = false;
        gamePaused = false;
    }
    
}
