using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider2D))]
public class InteractableObject : MonoBehaviour
{
    [Header("Object Structure")]
    [SerializeField] private BoxCollider2D interactCollider;

    [Header("Meter Effects")]
    [SerializeField] private float hungerChange;
    [SerializeField] private float thirstChange;
    [SerializeField] private float toiletChange; 
    [SerializeField] private float sanityChange;

    [FormerlySerializedAs("topDownPlayer")]
    [Header("Dependencies")]
    [SerializeField] private TopdownPlayerController player;
    [SerializeField] private MetersController metersController;

    private bool triggerable;
    // Start is called before the first frame update
    void Start()
    {
        interactCollider.isTrigger = true;

        triggerable = false;
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if(triggerable && Input.GetKeyDown(KeyCode.Space)){
            metersController.adjustMeters(hungerChange, thirstChange, toiletChange, sanityChange);
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
}
