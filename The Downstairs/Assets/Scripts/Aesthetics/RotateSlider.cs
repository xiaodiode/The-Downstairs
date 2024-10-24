using UnityEngine;
using UnityEngine.UI;  
public class RotateSlider : MonoBehaviour
{
    public GameObject objectToRotate; // Assign the GameObject you want to rotate in the Inspector
    private Slider rotationSlider; 

    void Start()
    {
        rotationSlider = gameObject.GetComponent<Slider>();
        rotationSlider.onValueChanged.AddListener(delegate { RotateBasedOnSlider(rotationSlider.value); });
    }
     void RotateBasedOnSlider(float sliderValue)
    { 
        float newZRotation = sliderValue * 180f;  
        objectToRotate.transform.rotation = Quaternion.Euler(0, 0, newZRotation);
    }
}