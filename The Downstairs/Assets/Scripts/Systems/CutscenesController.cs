using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutscenesController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cutsceneSprite;
    
    [Header("Intro Cutscene")]
    [SerializeField] private List<Sprite> introCutscene = new();
    [SerializeField] private List<int> secondsPerIntroScene = new();

    [Header("Stairs Cutscene")]
    [SerializeField] private List<Sprite> stairsCutscene = new();
    [SerializeField] private List<int> secondsPerStairsScene = new();

    [Header("Ending Cutscene")]
    [SerializeField] private List<Sprite> endingCutscene = new();
    [SerializeField] private List<int> secondsPerEndingScene = new();

    public static CutscenesController instance {get; private set;}

    public bool stairsCutscenePlayed;

    // Start is called before the first frame update
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
        stairsCutscenePlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator playIntroCutscene()
    {   
        GameManager.instance.enableGame(true);
        GameManager.instance.switchScreen(GameManager.ScreenType.Cutscene);

        AudioManager.instance.StopMusic();

        // RendererController.instance.toggleGameRenderer(RendererController.RendererType.VHS);

        for(int i=0; i<introCutscene.Count; i++)
        {
            cutsceneSprite.sprite = introCutscene[i];
            yield return new WaitForSeconds(secondsPerIntroScene[i]);
        }

        StartCoroutine(GameManager.instance.initializeGameStart());
    }

    public IEnumerator playStairsCutscene()
    {
        GameManager.instance.enableScreen(GameManager.ScreenType.Cutscene, true);
        GameManager.instance.pauseGame(false);

        AudioManager.instance.StopMusic();

        // RendererController.instance.toggleGameRenderer(RendererController.RendererType.VHS);

        for(int i = 0; i < stairsCutscene.Count; i++)
        {
            cutsceneSprite.sprite = stairsCutscene[0];
            yield return new WaitForSeconds(secondsPerStairsScene[0]);
        }

        GameManager.instance.enableScreen(GameManager.ScreenType.Cutscene, false);

        RendererController.instance.toggleGameRenderer(RendererController.RendererType.Light2D);

        GameManager.instance.resumeGame();

        if(MetersController.instance.sanityMeter.isIdle)
        {
            StartCoroutine(MetersController.instance.sanityMeter.decreaseMeter());
        }

        stairsCutscenePlayed = true;

    }
}
