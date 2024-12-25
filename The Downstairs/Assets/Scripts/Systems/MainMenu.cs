using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private List<EnterFont> TextElements = new();
    [SerializeField] private GameObject cutsceneVolume;
    public static MainMenu instance {get; private set;}
    public bool skipCutscene = false;

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

    public void startMainMenu()
    {
        RendererController.instance.toggleScreensRenderer(RendererController.RendererType.Light2D);

        AudioManager.instance.playMainMenuMusic();

        animateTextEnter();
    }

    public void playGame()
    {
        if(GameManager.instance.isNewGame && !skipCutscene)
        {
            GameManager.instance.playIntroCutscene(); 
        }
        else
        {
            StartCoroutine(GameManager.instance.initializeGameStart());
        }
    }

    private void animateTextEnter()
    {
        foreach(EnterFont text in TextElements)
        {
            text.StartDilation();
        }
    }

    public void toggleSkip(bool on){
        skipCutscene = on;
        cutsceneVolume.SetActive(!skipCutscene);
    }

}
