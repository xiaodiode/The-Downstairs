using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    public float secondsForHour;

    [SerializeField] private GameObject hourHand;
    [SerializeField] private GameObject minuteHand;
    private float rotationSpeed;

    Vector3 currHourRotation, currMinuteRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = secondsForHour/30;
    }

    // Update is called once per frame
    void Update()
    {
        rotateHands(hourHand, minuteHand, rotationSpeed);
    }

    private void rotateHands(GameObject hour, GameObject minute, float speed){
        currHourRotation = hour.transform.rotation.eulerAngles;
        currMinuteRotation = minute.transform.rotation.eulerAngles;

        currHourRotation.z -= rotationSpeed*Time.deltaTime;
        currMinuteRotation.z -= rotationSpeed*12*Time.deltaTime;
        
        hour.transform.rotation = Quaternion.Euler(currHourRotation);
        minute.transform.rotation = Quaternion.Euler(currMinuteRotation);
    }

}
