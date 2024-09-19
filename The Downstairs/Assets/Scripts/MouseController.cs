using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] public Camera mouseCamera;

    [SerializeField] public GameObject glowLight;
    [SerializeField] public bool mouseLightOn;

    Vector3 oldMousePosition, mouseScreen, mouseWorld;
    // Start is called before the first frame update
    void Start()
    {
        mouseLightOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(mouseLightOn){
            enableGlow();
        }
    }

    private void enableGlow(){

        mouseScreen = Input.mousePosition;
        mouseWorld = mouseCamera.ScreenToWorldPoint(new Vector3(mouseScreen.x, mouseScreen.y, mouseCamera.transform.position.y));
        mouseWorld.z = glowLight.transform.position.z;

        glowLight.transform.position = mouseWorld;
    }
}
