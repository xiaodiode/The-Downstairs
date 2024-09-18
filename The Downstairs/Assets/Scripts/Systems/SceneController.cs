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
    // Start is called before the first frame update
    void Start()
    {
        switchScenes("bedroom");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchScenes(string scene){
        disableAllScenes();

        switch(scene){
            case "bedroom":
                bedroom.SetActive(true);
                break;

            case "stairsBedUp":
                stairsBedUp.SetActive(true);
                break;

            case "stairsUpDown":
                stairsUpDown.SetActive(true);
                break;

            case "stairsDownBase":
                stairsDownBase.SetActive(true);
                break;

            case "upstairs":
                upstairs.SetActive(true);
                break;

            case "downstairs":
                downstairs.SetActive(true);
                break;

            case "basement":
                basement.SetActive(true);
                break;
            
            default:
                bedroom.SetActive(true);
                break;
        }
    }

    private void disableAllScenes(){
        bedroom.SetActive(false);
        stairsBedUp.SetActive(false);
        stairsUpDown.SetActive(false);
        stairsDownBase.SetActive(false);
        upstairs.SetActive(false);
        downstairs.SetActive(false);
        basement.SetActive(false);
    }

}
