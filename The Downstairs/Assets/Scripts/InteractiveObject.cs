using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        //print("triggered");
        //print(other.tag);
        if (other.gameObject == controller.gameObject)
        {
            //Debug.Log("gameobj");
            Debug.Log(Input.GetKeyDown(KeyCode.Space));
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("space");
                controller.SetInteract();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //print(triggerActive);
        //print(Input.GetKeyDown(KeyCode.Space));
        
    }
}
