using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Laundry : MonoBehaviour
{
    public int laundryQuantity;
    [SerializeField] private TextMeshProUGUI laundryQuantityUI;
    private bool triggerable;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        triggerable = false;

        updateLaundryText();
    }

    // Update is called once per frame
    void Update()
    {
        checkToilet();

        if(triggerable && Input.GetKeyDown(KeyCode.Space))
        {
            if(MetersController.instance.lockBedroom)
            {
                useLaundry();
            }
            else
            {
                Dialogue.instance.triggerLaundryDialogue("unused");
            }
        }
    }

    private void checkToilet()
    {
        if(MetersController.instance.toiletMeter.isEmpty && !MetersController.instance.sanityMeter.toiletEffect)
        {
            Debug.Log("toilet is empty");
            
            MetersController.instance.sanityMeter.changeDecreaseMultiplier(MetersController.instance.sanitySecondsToEmpty, 0.5f);
            MetersController.instance.lockBedroom = true;

            if(!MetersController.instance.sanityMeter.meterEnabled)
            {
                MetersController.instance.sanityMeter.startDecreasing();
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.GetComponent<TopdownPlayerController>()  != null || 
            other.gameObject.GetComponent<SidescrollPlayerController>() != null)
        {
            triggerable = true;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.GetComponent<TopdownPlayerController>()  != null || 
            other.gameObject.GetComponent<SidescrollPlayerController>() != null)
        {
            triggerable = false;
        }
    }

    private void useLaundry()
    {
        if(laundryQuantity > 0)
        {
            laundryQuantity--;
            updateLaundryText();

            MetersController.instance.lockBedroom = false;
        }
        else
        {
            Dialogue.instance.triggerLaundryDialogue("empty");
        }
    }

    private void updateLaundryText()
    {
        laundryQuantityUI.text = "Laundry Uses: " + laundryQuantity.ToString();
    }
}
