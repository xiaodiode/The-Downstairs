using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Door : MonoBehaviour
{
    [Header("Structure")]
    [SerializeField] private BoxCollider2D doorCollider;

    [Header("Door Type")]
    [SerializeField] private bool bedroom;
    [SerializeField] private bool stairs1, stairs2, stairs3;
    [SerializeField] private bool upstairs, downstairs, basement;
    [SerializeField] private bool goingUp;
    

    [Header("Dependencies")]
    [SerializeField] private PlayerController player;
    [SerializeField] private SceneController sceneController;

    private bool triggerable;
    private string nextScene;

    // Start is called before the first frame update
    void Start()
    {
        initializeDoor();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(triggerable && Input.GetKeyDown(KeyCode.Space)){
            sceneController.switchScenes(nextScene);
        }
    }

    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject == player.gameObject){
            triggerable = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other){
        if (other.gameObject == player.gameObject){
            triggerable = false;
        }
    }

    private void initializeDoor(){
        doorCollider.isTrigger = true;
        triggerable = false;
        
        if(bedroom){
            nextScene = "stairsBedUp";
        }
        else if(stairs1){
            if(goingUp){
                nextScene = "bedroom";
            }
            else{
                nextScene = "upstairs";
            }
        }
        else if(stairs2){
            if(goingUp){
                nextScene = "upstairs";
            }
            else{
                nextScene = "downstairs";
            }
        }
        else if(stairs3){
            if(goingUp){
                nextScene = "downstairs";
            }
            else{
                nextScene = "basement";
            }
        }
        else if(upstairs){
            if(goingUp){
                nextScene = "stairsBedUp";
            }
            else{
                nextScene = "stairsUpDown";
            }
        }
        else if(downstairs){
            if(goingUp){
                nextScene = "stairsUpDown";
            }
            else{
                nextScene = "stairsDownBase";
            }
        }
        else if(basement){
            nextScene = "stairsDownBase";
        }

    }
}
