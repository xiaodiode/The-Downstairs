using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutscenesController : MonoBehaviour
{
    [SerializeField] private RawImage cutsceneImage;
    
    [Header("Intro Cutscene")]
    [SerializeField] private List<Texture2D> introCutscene = new();
    [SerializeField] private List<int> secondsPerIntroScene = new();

    [Header("Stairs Cutscene")]
    [SerializeField] private List<Texture2D> stairsCutscene = new();
    [SerializeField] private List<int> secondsPerStairsScene = new();

    [Header("Ending Cutscene")]
    [SerializeField] private List<Texture2D> endingCutscene = new();
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

        RendererController.instance.toggleGameRenderer(RendererController.RendererType.VHS);

        for(int i=0; i<introCutscene.Count; i++)
        {
            cutsceneImage.texture = introCutscene[i];
            yield return new WaitForSeconds(secondsPerIntroScene[i]);
        }

        StartCoroutine(GameManager.instance.initializeGameStart());
    }

    public IEnumerator playStairsCutscene()
    {
        GameManager.instance.enableScreen(GameManager.ScreenType.Cutscene, true);

        MetersController.instance.pauseAllMeters();

        RendererController.instance.toggleGameRenderer(RendererController.RendererType.VHS);

        for(int i = 0; i < stairsCutscene.Count; i++)
        {
            cutsceneImage.texture = stairsCutscene[0];
            yield return new WaitForSeconds(secondsPerStairsScene[0]);
        }

        GameManager.instance.enableScreen(GameManager.ScreenType.Cutscene, false);

        RendererController.instance.toggleGameRenderer(RendererController.RendererType.Light2D);

        MetersController.instance.resumeAllMeters();

        if(!MetersController.instance.sanityMeter.meterEnabled)
        {
            MetersController.instance.sanityMeter.startDecreasing();
            Debug.Log("entered darkness");
        }

        stairsCutscenePlayed = true;

    }
}
