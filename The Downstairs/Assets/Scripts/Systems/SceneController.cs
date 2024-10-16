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

    private Dictionary<ScenesType, GameObject> scenesDict;

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
    
    
    // Start is called before the first frame update
    void Start()
    {
        scenesDict = new()
        {
            { ScenesType.Upstairs, upstairs },
            { ScenesType.Basement, basement},
            { ScenesType.Bedroom, bedroom},
            { ScenesType.StairsBedUp, stairsBedUp },
            { ScenesType.StairsDownBase, stairsDownBase },
            { ScenesType.StairsUpDown, stairsUpDown },
            { ScenesType.Downstairs, downstairs }
        };

        switchScenes(ScenesType.Upstairs);
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

        CandleController.instance.checkActiveLight();
    }


}
