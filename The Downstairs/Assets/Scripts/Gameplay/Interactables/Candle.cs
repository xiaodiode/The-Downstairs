using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider2D))]
public class Candle : MonoBehaviour
{
    [Header("Object Structure")]
    [SerializeField] private BoxCollider2D interactCollider;


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
            CandleController.instance.pickUpCandle();
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
