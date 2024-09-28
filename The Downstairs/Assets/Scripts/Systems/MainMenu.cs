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

        DontDestroyOnLoad(gameObject);
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
        AudioController.instance.playMainMenuMusic();

        foreach(EnterFont text in TextElements)
        {
            text.StartDilation();
        }
    }

    public void playGame()
    {
        if(GameManager.instance.isNewGame)
        {
            StartCoroutine(CutscenesController.instance.playIntroCutscene());    
        }
        else
        {
            StartCoroutine(GameManager.instance.intitializeGameStart());
        }
    }
}
