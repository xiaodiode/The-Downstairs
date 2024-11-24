using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(BoxCollider2D))]
public class WaterPitcher : MonoBehaviour
{
    public int currWaterQuantity;
    [SerializeField] private int startingWaterQuantity;
    [SerializeField] private TextMeshProUGUI waterQuantityUI;
    [SerializeField] private int thirstIncrease;

    private bool triggerable;
    public static WaterPitcher instance {get; private set;}

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
            usePitcher();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<TopdownPlayerController>()  != null)
        {
            triggerable = true;
            GameManager.instance.setInteract(true, "- water pitcher -");

        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<TopdownPlayerController>()  != null)
        {
            triggerable = false;
            GameManager.instance.setInteract(false, "- water pitcher -");
        }
    }

    private void usePitcher()
    {
        if(currWaterQuantity > 0)
        {
            currWaterQuantity--;
            updatePitcherText();

            MetersController.instance.resetThirstMeter();

            StartCoroutine(AudioManager.instance.playDrinking());
        }
        else
        {
            Dialogue.instance.triggerEmptyPitcherDialogue();
        }
    }

    private void updatePitcherText()
    {
        waterQuantityUI.text = "Water Pitcher Uses: " + currWaterQuantity.ToString();
    }

    public void resetWater()
    {
        triggerable = false;

        currWaterQuantity = startingWaterQuantity;

        updatePitcherText();
    }
}
