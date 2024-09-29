using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(BoxCollider2D))]
public class WaterPitcher : MonoBehaviour
{
    public int waterQuantity;
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
        triggerable = false;

        updatePitcherText();

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
        if (other.gameObject.GetComponent<TopdownPlayerController>()  != null || 
            other.gameObject.GetComponent<SidescrollPlayerController>() != null)
        {
            triggerable = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<TopdownPlayerController>()  != null || 
            other.gameObject.GetComponent<SidescrollPlayerController>() != null)
        {
            triggerable = false;
        }
    }

    private void usePitcher()
    {
        if(waterQuantity > 0)
        {
            waterQuantity--;
            updatePitcherText();

            MetersController.instance.adjustMeters(0, thirstIncrease, 0,0);
        }
        else
        {
            Dialogue.instance.triggerEmptyPitcherDialogue();
        }
    }

    private void updatePitcherText()
    {
        waterQuantityUI.text = "Water Pitcher Uses: " + waterQuantity.ToString();
    }
}
