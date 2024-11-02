using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Fridge : MonoBehaviour
{
    public int foodQuantity;
    [SerializeField] private TextMeshProUGUI foodQuantityUI;
    [SerializeField] private int hungerIncrease;

    private bool triggerable;
    public static Fridge instance {get; private set;}

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null && instance != this){
            Destroy(this);
        }
        else{
            instance = this;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        triggerable = false;

        updateFridgeText();

        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(triggerable && Input.GetKeyDown(KeyCode.Space))
        {
            useFridge();
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

    private void useFridge()
    {
        if(foodQuantity > 0)
        {
            foodQuantity--;
            updateFridgeText();

            MetersController.instance.resetHungerMeter();
        }
        else
        {
            Dialogue.instance.triggerEmptyFridgeDialogue();
        }
    }

    private void updateFridgeText()
    {
        foodQuantityUI.text = "Fridge Uses: " + foodQuantity.ToString();
    }
}
