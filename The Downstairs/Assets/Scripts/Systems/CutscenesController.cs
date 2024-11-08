using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

[Serializable]
public struct Cutscene
{
    public Sprite cutsceneSprite;
    public List<GameObject> visualFX;
    public float cutsceneSeconds;
    public bool fadeIn;
    public float fadeInSeconds;
    public float maxGlobalIntensity;
    public bool zoomIn;
    public float zoomInSeconds;
    public Vector3 zoomScale;
}
public class CutscenesController : MonoBehaviour
{
    [SerializeField] private Vector3 normalZoomScale;
    [SerializeField] private RectTransform cutsceneSpriteRect;
    [SerializeField] private SpriteRenderer cutsceneSprite;
    [SerializeField] private Light2D globalLight;
    
    [Header("Intro Cutscene")]
    [SerializeField] private List<Cutscene> introCutscenes = new();

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

        float timePassed;

        for(int i = 0; i < introCutscenes.Count; i++)
        {
            AudioManager.instance.StopMusic();
            AudioManager.instance.StopSFX();

            cutsceneSpriteRect.localScale = normalZoomScale;

            if(i == 0) AudioManager.instance.playTVCutsceneAudio();

            cutsceneSprite.sprite = introCutscenes[i].cutsceneSprite;
         
            globalLight.intensity = introCutscenes[i].fadeIn ? 0 : introCutscenes[i].maxGlobalIntensity;

            foreach(GameObject light in introCutscenes[i].visualFX)
            {
                light.SetActive(true);
            }

            timePassed = 0; 

            while(timePassed < introCutscenes[i].cutsceneSeconds)
            {
                if(introCutscenes[i].fadeIn)
                {
                    if(timePassed < introCutscenes[i].fadeInSeconds)
                    {
                        globalLight.intensity = Mathf.Lerp(0, introCutscenes[i].maxGlobalIntensity, timePassed/introCutscenes[i].fadeInSeconds);
                    }

                }

                if(introCutscenes[i].zoomIn)
                {
                    if(timePassed < introCutscenes[i].zoomInSeconds)
                    {
                        cutsceneSpriteRect.localScale = Vector3.Lerp(normalZoomScale, introCutscenes[i].zoomScale, timePassed/introCutscenes[i].zoomInSeconds);
                    }
                }

                timePassed += Time.deltaTime;

                yield return null;
            }

            

            foreach(GameObject light in introCutscenes[i].visualFX)
            {
                light.SetActive(false);
            }
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

    private void playTVCutscene()
    {

    }
}
