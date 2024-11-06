using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isNewGame;
    public bool gamePaused, gameReset;
    public bool isGameOver;

    [Header("UI Screens")]
    [SerializeField] public GameObject screensCanvas;
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject pauseScreen;


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
        Pause,
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
            { ScreenType.Pause, pauseScreen },
            { ScreenType.GameOver, gameOverScreen }
        };

        OpenMainMenu();

        ResetManager.instance.HardReset();

        // isNewGame = false; // change to false to skip intro cutscene

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gamePaused)
            {
                // pauseGame(true, RendererController.RendererType.VHS);
                pauseGame(true);
            }

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

        // RendererController.instance.toggleGameRenderer(RendererController.RendererType.Light2D);

        StartCoroutine(Timer.instance.startTimer());
        ClockController.instance.startRotating();

        StartCoroutine(MetersController.instance.hungerMeter.decreaseMeter());
        StartCoroutine(MetersController.instance.thirstMeter.decreaseMeter());
        StartCoroutine(MetersController.instance.toiletMeter.decreaseMeter());
        // StartCoroutine(MetersController.instance.sanityMeter.decreaseMeter());

        StartCoroutine(CandleController.instance.FindActiveLight());

        CrawlingController.instance.setAnimSpeeds();

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
        ResetManager.instance.SoftReset();

        enableScreen(ScreenType.Continue, true);
        // RendererController.instance.toggleGameRenderer(RendererController.RendererType.VHS);

        ContinueScreen.instance.updateText();

    }

    public void continueGame()
    {
        StartCoroutine(initializeGameStart());

        enableScreen(ScreenType.Continue, false);
        // RendererController.instance.toggleGameRenderer(RendererController.RendererType.Light2D);

        increaseNightCount();
    }

    public void returnToMainMenu()
    {
        OpenMainMenu();

        if(isGameOver) ResetManager.instance.HardReset();
        
        else ResetManager.instance.SoftReset();
    }

    public void triggerGameOver()
    {
        pauseGame(false);

        enableScreen(ScreenType.GameOver, true);

        StartCoroutine(GameOver.instance.fadeIn());
    }

    public void increaseNightCount()
    {
        nightCount++;
        isNewGame = false;
    }

    public void pauseGame(bool display)
    {
        gamePaused = true;

        if(display)
        {
            enableScreen(ScreenType.Pause, display);
            Time.timeScale = 0;
        }

        // RendererController.instance.toggleGameRenderer(renderer);
    }

    public void resumeGame()
    {
        gamePaused = false;
        enableScreen(ScreenType.Pause, false);
        Time.timeScale = 1;
        // RendererController.instance.toggleGameRenderer(RendererController.RendererType.Light2D);
    }

    public void playGame()
    {
        gameReset = false;
        gamePaused = false;
    }
    
}
