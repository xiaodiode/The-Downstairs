using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isNewGame;

    [Header("UI Screens")]
    [SerializeField] public GameObject screensCanvas;
    [SerializeField] private GameObject mainMenuScreen;
    

    [Header("Gameplay")]
    [SerializeField] public GameObject gameCanvas;
    [SerializeField] private GameObject cutsceneScreen;
    [SerializeField] private GameObject gameplayScreen;

    public Dictionary<ScreenType, GameObject> screensDict;

    public enum ScreenType
    {
        MainMenu,
        Cutscene,
        Gameplay
    }

    public static GameManager instance {get; private set;}

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null && instance != this){
            Destroy(this);
        }
        else{
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        screensDict = new()
        {
            { ScreenType.MainMenu, mainMenuScreen },
            { ScreenType.Gameplay, gameplayScreen },
            { ScreenType.Cutscene, cutsceneScreen }
        };

        OpenMainMenu();

        isNewGame = true; 
        // isNewGame = false; // change to false to skip intro cutscene
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchScreen(ScreenType newScreen)
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
        enableGame(false);

        MainMenu.instance.startMainMenu();
    }

    public void enableGame(bool enable)
    {
        screensCanvas.SetActive(!enable);
        gameCanvas.SetActive(enable);
    }


    public IEnumerator intitializeGameStart()
    {
        enableGame(true);
        switchScreen(ScreenType.Gameplay);

        AudioController.instance.playGameplayMusic();
        MetersController.instance.initializeMeters();

        Timer.instance.startCountUp();

        MetersController.instance.hungerMeter.startDecreasing();
        MetersController.instance.thirstMeter.startDecreasing();
        MetersController.instance.toiletMeter.startDecreasing();
        MetersController.instance.sanityMeter.startDecreasing();

        yield return new WaitForSeconds(3);
        // Dialogue.instance.addToDialogue("this is the first dialogue that i am printing");
        // Dialogue.instance.addToDialogue("second dialogue incoming");

    }

    public void playIntroCutscene()
    {
        StartCoroutine(CutscenesController.instance.playIntroCutscene());   
    }

    public void playStairsCutscene()
    {
        StartCoroutine(CutscenesController.instance.playStairsCutscene());
    }
    
}
