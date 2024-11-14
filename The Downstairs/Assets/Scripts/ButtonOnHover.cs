using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hoverObject;
    public bool onOff = false;
    void Start()
    {
        if (hoverObject != null)
        {
            hoverObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverObject != null && !onOff)
        {
            hoverObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverObject != null && !onOff)
        {
            hoverObject.SetActive(false);
        }
    }

    public void turnOpposite(){
        if(onOff){
            hoverObject.SetActive(!hoverObject.activeSelf);
        }
    }
}