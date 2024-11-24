using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Fridge : MonoBehaviour
{
    public int currFoodQuantity;
    [SerializeField] private int startingFoodQuantity;
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

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<TopdownPlayerController>())
        {
            triggerable = true;
            GameManager.instance.setInteract(true, "- fridge -");
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<TopdownPlayerController>())
        {
            triggerable = false;
            GameManager.instance.setInteract(false, "- fridge -");
        }
    }

    private void useFridge()
    {
        if(currFoodQuantity > 0)
        {
            currFoodQuantity--;
            updateFridgeText();

            MetersController.instance.resetHungerMeter();

            StartCoroutine(AudioManager.instance.playEatingSequence());
        }
        else
        {
            Dialogue.instance.triggerEmptyFridgeDialogue();
        }
    }

    private void updateFridgeText()
    {
        foodQuantityUI.text = "Fridge Uses: " + currFoodQuantity.ToString();
    }

    public void resetFridge()
    {
        triggerable = false;

        currFoodQuantity = startingFoodQuantity;

        updateFridgeText();
    }
    
}
