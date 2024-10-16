using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private List<EnterFont> TextElements = new();

    public static MainMenu instance {get; private set;}

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
        Time.timeScale = 1;

        RendererController.instance.toggleScreensRenderer(RendererController.RendererType.Light2D);

        AudioController.instance.playMainMenuMusic();

        animateTextEnter();
    }

    public void playGame()
    {
        // animateTextEnter(true);

        if(GameManager.instance.isNewGame)
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

}
