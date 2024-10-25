using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Door : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;
    [SerializeField] private Transform stairsCameraPosition;
    [SerializeField] public SceneController.ScenesType targetStairs;

    private bool triggerable;

    void Start()
    {
        triggerable = false;
    }


    void Update()
    {
        checkInteraction();
    }

    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.GetComponent<TopdownPlayerController>()  != null || 
            other.gameObject.GetComponent<SidescrollPlayerController>() != null)
        {
            triggerable = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.GetComponent<TopdownPlayerController>()  != null || 
            other.gameObject.GetComponent<SidescrollPlayerController>() != null)
        {
            triggerable = false;
        }
    }

    private void checkInteraction()
    {
        if(triggerable && Input.GetKeyDown(KeyCode.Space))
        {
            if(targetStairs == SceneController.ScenesType.Stairs1)
            {
                if(!CutscenesController.instance.stairsCutscenePlayed)
                {
                    Debug.Log("playing stairs cutscene now");
                    GameManager.instance.playStairsCutscene();
                }
                else
                {
                    StartCoroutine(MetersController.instance.sanityMeter.decreaseMeter());
                }
            }

            SceneController.instance.switchScenes(targetStairs);
            gameCamera.gameObject.transform.position = stairsCameraPosition.position;

            StairsController.instance.currentStairs = SceneController.instance.stairsScenesDict[targetStairs];

            QTEController.instance.StartStairsGameplay();
            
        }
    }
}
