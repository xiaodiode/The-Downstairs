using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Toilet : MonoBehaviour
{
    private bool triggerable;

    // Start is called before the first frame update
    void Start()
    {
        triggerable = false;

        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(triggerable && Input.GetKeyDown(KeyCode.Space))
        {
            useToilet();
        }
    }

    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.GetComponent<TopdownPlayerController>()  != null)
        {
            triggerable = true;
            GameManager.instance.setInteract(true, "- toilet -");
        }
    }

    public void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.GetComponent<TopdownPlayerController>()  != null)
        {
            triggerable = false;
            GameManager.instance.setInteract(false, "- toilet -");
        }
    }

    private void useToilet()
    {
        MetersController.instance.resetToiletMeter();
        
        AudioManager.instance.playToiletFlush();
    }

}
