using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Door : MonoBehaviour
{
    [SerializeField] public Door target;
    [SerializeField] public SceneController.ScenesType selfScene;
    
    [SerializeField] private SceneController sceneController;

    private bool triggerable;
    public bool isBedroom;

    void Start()
    {
        triggerable = false;
    }


    void Update()
    {
        if(triggerable && Input.GetKeyDown(KeyCode.Space))
        {
            if(selfScene == SceneController.ScenesType.Bedroom)
            {
                if(!CutscenesController.instance.stairsCutscenePlayed)
                {
                    Debug.Log("playing stairs cutscene now");
                    GameManager.instance.playStairsCutscene();
                }
                else
                {
                    if(!MetersController.instance.sanityMeter.meterEnabled)
                    {
                        MetersController.instance.sanityMeter.startDecreasing();
                        Debug.Log("entered darkness");
                    }
                }
            }
            sceneController.switchScenes(target.selfScene);
        }
    }

    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.GetComponent<TopdownPlayerController>()  != null || 
            other.gameObject.GetComponent<SidescrollPlayerController>() != null)
        {
            triggerable = true;
            Debug.Log("trigger entered");
        }
    }

    public void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.GetComponent<TopdownPlayerController>()  != null || 
            other.gameObject.GetComponent<SidescrollPlayerController>() != null)
        {
            triggerable = false;
        }
    }
}
