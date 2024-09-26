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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator playIntroCutscene()
    {
        GameManager.instance.switchScreen(GameManager.ScreenType.Cutscene);
        GameManager.instance.screensCanvas.SetActive(true);

        for(int i=0; i<introCutscene.Count; i++)
        {
            cutsceneImage.texture = introCutscene[i];
            yield return new WaitForSeconds(secondsPerIntroScene[i]);
        }

        StartCoroutine(GameManager.instance.intiializeGameStart());
    }
}
