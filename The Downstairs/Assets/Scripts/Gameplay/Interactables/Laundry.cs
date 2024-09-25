using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Laundry : MonoBehaviour
{

    private bool triggerable;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        triggerable = false;

    }

    // Update is called once per frame
    void Update()
    {
        checkToilet();

        if(triggerable && Input.GetKeyDown(KeyCode.Space))
        {
            MetersController.instance.lockBedroom = false;
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
            triggerable = true;
        }
    }
}
