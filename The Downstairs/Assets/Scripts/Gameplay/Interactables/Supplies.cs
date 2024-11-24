using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Supplies : MonoBehaviour
{
    [SerializeField] private bool isCandle;
    [SerializeField] private bool isMatches;

    [Header("Canned Food Specs")]
    [SerializeField] private bool isCannedFood;
    [SerializeField] private int fullnessChange;
    [SerializeField] private int sanityChange;


    private bool isTriggered;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        isTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTriggered && Input.GetKeyDown(KeyCode.Space)){
            if(isCandle)
            {
                CandleController.instance.pickUpCandle();
            }
            else if(isMatches)
            {
                MatchController.instance.pickUpMatches();
            }
            else if(isCannedFood)
            {
                MetersController.instance.adjustMeters(fullnessChange, 0, 0, sanityChange);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        // Debug.Log("is triggered " + gameObject);
        if(collider.gameObject.GetComponent<SidescrollPlayerController>() != null ||
            collider.gameObject.GetComponent<TopdownPlayerController>() != null)
        {
            isTriggered = true;
            // Debug.Log("is triggered " + gameObject);
            GameManager.instance.setInteract(true, "- supplies -");
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.GetComponent<SidescrollPlayerController>() != null ||
            collider.gameObject.GetComponent<TopdownPlayerController>() != null)
        {
            isTriggered = false;
            GameManager.instance.setInteract(false, "- supplies -");
        }
    }
}
