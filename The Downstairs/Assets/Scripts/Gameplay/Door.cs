using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Door : MonoBehaviour
{
    [SerializeField] public Door target;
    [SerializeField] public SceneController.ScenesType selfScene;
    
    [SerializeField] private SceneController sceneController;

    private bool triggerable;
    private string nextScene;

    void Update()
    {
        if(triggerable && Input.GetKeyDown(KeyCode.Space)){
            sceneController.switchScenes(target.selfScene);
        }
    }

    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.GetComponent<TopdownPlayerController>() != null){
            triggerable = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.GetComponent<TopdownPlayerController>() != null){
            triggerable = false;
        }
    }
}
