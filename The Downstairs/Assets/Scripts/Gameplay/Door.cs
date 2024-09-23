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

    void Start()
    {
        triggerable = false;
    }


    void Update()
    {
        if(triggerable && Input.GetKeyDown(KeyCode.Space)){
            sceneController.switchScenes(target.selfScene);
        }
    }

    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.GetComponent<TopdownPlayerController>()  != null || 
            other.gameObject.GetComponent<SidescrollPlayerController>() != null){
            triggerable = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.GetComponent<TopdownPlayerController>()  != null || 
            other.gameObject.GetComponent<SidescrollPlayerController>() != null){
            triggerable = true;
        }
    }
}
