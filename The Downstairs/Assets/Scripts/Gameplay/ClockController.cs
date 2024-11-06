using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    [Header("Clock Structure")]
    [SerializeField] private GameObject hourHand;
    [SerializeField] private GameObject minuteHand;

    [Header("Rotation Mechanics")]
    [SerializeField] private float secondsForHour;
    [SerializeField] public int tutorialHour, resetHour;
    [SerializeField] public int morningHour;
    [SerializeField] private int totalHours;

    private float startGameAngle;

    private bool isRotating;

    Vector3 currHourRotation, currMinuteRotation;

    private float rotateStart;
    
    public static ClockController instance {get; private set;}

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
        resetClockHands(tutorialHour);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startRotating()
    {
        StartCoroutine(rotateHourHand());
        StartCoroutine(rotateMinuteHand());
    }

    private IEnumerator rotateHourHand()
    {
        float timePassed = 0;

        isRotating = true;
        
        while(timePassed < secondsForHour*totalHours)
        {
            if(GameManager.instance.gamePaused) yield return null;

            else
            {
                currHourRotation = hourHand.transform.rotation.eulerAngles;

                currHourRotation.z = startGameAngle - (totalHours*30)*(timePassed/(secondsForHour*totalHours));

                hourHand.transform.rotation = Quaternion.Euler(currHourRotation);
                
                timePassed += Time.deltaTime;
                
                yield return null;
            }
        }

        isRotating = false;
        
    }

    private IEnumerator rotateMinuteHand()
    {
        float timePassed = 0;

        while(isRotating)
        {
            if(GameManager.instance.gamePaused) yield return null;

            else
            {
                currMinuteRotation = minuteHand.transform.rotation.eulerAngles;

                currMinuteRotation.z = -360*(timePassed/secondsForHour);

                minuteHand.transform.rotation = Quaternion.Euler(currMinuteRotation);
                
                timePassed += Time.deltaTime;

                yield return null;
            }
        }

        GameManager.instance.openContinueScreen();
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
