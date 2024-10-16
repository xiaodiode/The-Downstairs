using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    [SerializeField] private float secondsForHour;
    [SerializeField] public int startHour, resetHour;
    [SerializeField] public int morningHour;
    [SerializeField] private int totalHours;

    [SerializeField] private GameObject hourHand;
    [SerializeField] private GameObject minuteHand;
    private float rotationSpeed;
    private float startGameAngle, endGameAngle;
    private float oldHourAngle;

    private bool isRotating;

    Vector3 currHourRotation, currMinuteRotation;

    private float rotateStart, timePassed;
    
    public static ClockController instance {get; private set;}

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
        rotationSpeed = secondsForHour/30;

        endGameAngle = 360 + (-30*morningHour);

        resetClockHands(startHour);
    }

    // Update is called once per frame
    void Update()
    {
        if(isRotating)
        {
            // rotateClockHands();

            // if(currHourRotation.z <= endGameAngle && oldHourAngle > endGameAngle)
            // {
            //     Debug.Log("night over");

            //     GameManager.instance.openContinueScreen();
            // }

            // oldHourAngle = currHourRotation.z;


        }
        
    }

    public void startRotating()
    {
        isRotating = true;

        rotateStart = Time.time;

        StartCoroutine(rotateHourHand());
    }

    private IEnumerator rotateHourHand()
    {
        while(timePassed < secondsForHour*totalHours)
        {
            timePassed = Time.time - rotateStart;

            currHourRotation = hourHand.transform.rotation.eulerAngles;

            currHourRotation.z = startGameAngle - (totalHours*30)*(timePassed/(secondsForHour*totalHours));

            hourHand.transform.rotation = Quaternion.Euler(currHourRotation);
            
            yield return null;
        }

        Debug.Log("done");
        
    }

    private void rotateClockHands()
    {
        currHourRotation = hourHand.transform.rotation.eulerAngles;
        currMinuteRotation = minuteHand.transform.rotation.eulerAngles;

        currHourRotation.z -= rotationSpeed*Time.deltaTime;
        currMinuteRotation.z -= rotationSpeed*12*Time.deltaTime;
        
        hourHand.transform.rotation = Quaternion.Euler(currHourRotation);
        minuteHand.transform.rotation = Quaternion.Euler(currMinuteRotation);
    }

    public void resetClockHands(int hour)
    {
        currHourRotation = hourHand.transform.rotation.eulerAngles;
        currMinuteRotation = minuteHand.transform.rotation.eulerAngles;

        if(hour == 12)
        {
            currHourRotation.z = 0;
            startGameAngle = 0;
        }
        else{
            currHourRotation.z = 30*(12-hour);
            startGameAngle = 30*(12-hour);
        }

        currMinuteRotation.z = 0;

        totalHours = morningHour + (12 - hour);

        hourHand.transform.rotation = Quaternion.Euler(currHourRotation);
        minuteHand.transform.rotation = Quaternion.Euler(currMinuteRotation);
    }

}
