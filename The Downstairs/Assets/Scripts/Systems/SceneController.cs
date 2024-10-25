using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [Header("Bedroom")]
    [SerializeField] private GameObject bedroom;

    [Header("Stairs")]
    [SerializeField] private GameObject stairsBedUp;
    [SerializeField] private GameObject stairsUpDown;
    [SerializeField] private GameObject stairsDownBase;

    [Header("The Downstairs")]
    [SerializeField] private GameObject upstairs;
    [SerializeField] private GameObject downstairs;
    [SerializeField] private GameObject basement;

    public Dictionary<ScenesType, GameObject> scenesDict;
    public ScenesType currentScene;

    private List<ScenesType> stairsScenes;
    
    public enum ScenesType
    {
        Upstairs,
        Bedroom,
        StairsBedUp,
        StairsUpDown,
        StairsDownBase,
        Downstairs,
        Basement,
    }

    public static SceneController instance {get; private set;}

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
        scenesDict = new()
        {
            { ScenesType.Upstairs, upstairs },
            { ScenesType.Basement, basement },
            { ScenesType.Bedroom, bedroom },
            { ScenesType.StairsBedUp, stairsBedUp },
            { ScenesType.StairsDownBase, stairsDownBase },
            { ScenesType.StairsUpDown, stairsUpDown },
            { ScenesType.Downstairs, downstairs }
        };
        currentScene = ScenesType.Downstairs;
        switchScenes(currentScene);

        stairsScenes = new()
        {
            ScenesType.StairsBedUp,
            ScenesType.StairsUpDown,
            ScenesType.StairsDownBase
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchScenes(ScenesType newScene)
    {
        foreach (ScenesType scene in System.Enum.GetValues(typeof(ScenesType)))
        {
            scenesDict[scene].SetActive(false);
        }

        scenesDict[newScene].SetActive(true);

        if(newScene == ScenesType.Bedroom) 
        {
            MetersController.instance.resetSanityMeter();
        }
        else
        {
            // foreach(ScenesType stairs in stairsScenes)
            // {
            //     if(stairs == newScene)
            //     {
            //         StairsController.instance.currentStairs = scenesDict[newScene];
                    
            //         QTEController.instance.StartStairsGameplay();
                    
            //         break;
            //     }
                
            // }
        }

        currentScene = newScene;

        CandleController.instance.checkActiveLight();
    }

}
