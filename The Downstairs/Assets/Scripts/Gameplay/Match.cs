using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider2D))]
public class Match : MonoBehaviour
{
    [Header("Object Structure")]
    [SerializeField] private BoxCollider2D interactCollider;

    [FormerlySerializedAs("topDownPlayer")]
    [Header("Dependencies")]
    [SerializeField] private SidescrollPlayerController player;
    [SerializeField] private MatchController matchController;

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
            matchController.pickUpMatches();
            Destroy(gameObject);
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

    public void initializeMatch(MatchController newMatchControl){
        matchController = newMatchControl;
    }


}
