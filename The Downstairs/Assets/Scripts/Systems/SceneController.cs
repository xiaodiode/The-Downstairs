using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;

    [Header("Room Scenes")]

    [Header("Bedroom")]
    [SerializeField] private GameObject bedroom;

    [Header("Stairs")]
    [SerializeField] private Stairs stairs1;
    [SerializeField] private Stairs stairs2;
    [SerializeField] private Stairs stairs3;
    [SerializeField] private Transform stairsCameraPosition;

    [Header("The Downstairs")]
    [SerializeField] private GameObject upstairs;
    [SerializeField] private GameObject downstairs;
    [SerializeField] private GameObject basement;

    public Dictionary<ScenesType, GameObject> scenesDict;
    public Dictionary<ScenesType, Stairs> stairsScenesDict;
    public ScenesType currentScene;
    
    public enum ScenesType
    {
        Upstairs,
        Bedroom,
        Stairs1,
        Stairs2,
        Stairs3,
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
            { ScenesType.Bedroom, bedroom },
            { ScenesType.Upstairs, upstairs },
            { ScenesType.Basement, basement },
            { ScenesType.Downstairs, downstairs },
            { ScenesType.Stairs1, stairs1.gameObject },
            { ScenesType.Stairs2, stairs2.gameObject },
            { ScenesType.Stairs3, stairs3.gameObject },
        };

        stairsScenesDict = new()
        {
            { ScenesType.Stairs1, stairs1 },
            { ScenesType.Stairs2, stairs2 },
            { ScenesType.Stairs3, stairs3 },
        };
        
        currentScene = ScenesType.Basement;

        switchScenes(currentScene);
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

        currentScene = newScene;

        CandleController.instance.checkActiveLight();
    }

}
