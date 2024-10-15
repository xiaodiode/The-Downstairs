using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider2D))]
public class Match : MonoBehaviour
{
    private BoxCollider2D interactCollider;

    private bool triggerable;
    
    // Start is called before the first frame update
    void Start()
    {
        interactCollider = gameObject.GetComponent<BoxCollider2D>();
        interactCollider.isTrigger = true;

        triggerable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(triggerable && Input.GetKeyDown(KeyCode.Space)){
            MatchController.instance.pickUpMatches();
            Destroy(gameObject);
        }
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



}
