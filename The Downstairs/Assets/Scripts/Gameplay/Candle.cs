using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Candle : MonoBehaviour
{
    [Header("Object Structure")]
    [SerializeField] private BoxCollider2D interactCollider;

    [Header("Dependencies")]
    [SerializeField] private PlayerController player;
    [SerializeField] private CandleController candleController;

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
            candleController.pickUpCandle();
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

    public void initializeMatch(CandleController newCandleControl){
        candleController = newCandleControl;
    }
}