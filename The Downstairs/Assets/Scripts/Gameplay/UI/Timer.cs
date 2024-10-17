using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI time;
    private string leadingZeroH, leadingZeroM, leadingZeroS;
    private string timeText = "00:00:00";
    private int hours, minutes, seconds;
    
    public static Timer instance {get; private set;}

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
        leadingZeroH = "0";
        leadingZeroM = "0";
        leadingZeroS = "0";

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator startTimer()
    {
        float timePassed = 0;
        int secondsPassed;
        hours = 0; minutes = 0; seconds = 0;
        
        while(!GameManager.instance.gameReset)
        {
            if(GameManager.instance.gamePaused) yield return null;

            else
            {
                secondsPassed = Mathf.FloorToInt(timePassed);
                
                seconds = secondsPassed % 60;
                minutes = (secondsPassed/60) % 60;
                hours = (secondsPassed/3600) % 60;

                if(hours > 9){
                    leadingZeroH = "";
                }
                else{
                    leadingZeroH = "0";
                }
                if(minutes > 9){
                    leadingZeroM = ":";
                }
                else{
                    leadingZeroM = ":0";
                }
                if(seconds > 9){
                    leadingZeroS = ":";
                }
                else{
                    leadingZeroS = ":0";
                }
                timeText = leadingZeroH + hours.ToString() + leadingZeroM + minutes.ToString() + leadingZeroS + seconds.ToString();
            
                time.text = timeText;

                timePassed += Time.deltaTime;

                yield return null;
            }

        }
    }

}