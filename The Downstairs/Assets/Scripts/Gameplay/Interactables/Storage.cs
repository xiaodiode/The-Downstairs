using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class Storage : MonoBehaviour
{
    [Header("Storage Features")]
    [SerializeField] [Range(0.0f, 1.0f)] private float matchChance;

    [Header("Hold Mechanics")]
    [SerializeField] private Slider holdMeter;
    [SerializeField] private float holdSeconds;

    [Header("Cooldown Mechanics")]
    [SerializeField] private Slider cooldownMeter;
    [SerializeField] private float cooldownSeconds;
    [SerializeField] private bool onCooldown;

    [Header("Meter Placement Structure")]
    [SerializeField] private Canvas UICanvas;
    [SerializeField] private Camera gameCamera;
    [SerializeField] private RectTransform holdMeterRect;
    [SerializeField] private RectTransform cooldownMeterRect;
    private RectTransform screenRect;
    private Vector3 storageScreenPosition;
    private Vector2 localPosition;

    private bool triggerable;

    private float holdStartTime, secondsPressed;
    private float cooldownStartTime, secondsPassed;
    private bool pressed;

    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        collider.isTrigger = true;

        triggerable = false;

        onCooldown = false;

        pressed = true;
        secondsPressed = 0;

        screenRect = holdMeterRect.transform.parent as RectTransform;

    }

    // Update is called once per frame
    void Update()
    {
        if(triggerable)
        {
            checkInteraction();
        }
        else
        {
            hideMeters();
        }
    }

    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.GetComponent<TopdownPlayerController>()  != null || 
            other.gameObject.GetComponent<SidescrollPlayerController>() != null)
        {
            triggerable = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.GetComponent<TopdownPlayerController>()  != null || 
            other.gameObject.GetComponent<SidescrollPlayerController>() != null)
        {
            triggerable = false;

            secondsPressed = 0;
        }
    }

    private void checkInteraction()
    {
        if(!onCooldown)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                pressed = true;
                secondsPressed = 0;

                StartCoroutine(updateHoldMeter());
            }
            else if(Input.GetKeyUp(KeyCode.Space))
            {
                pressed = false;
            }
        }
        else
        {
            pressed = false;
            displayMeter();
        }
    }

    private IEnumerator updateHoldMeter()
    {
        displayMeter();

        holdStartTime = Time.time;
        holdMeter.value = 0;

        while(pressed && secondsPressed < holdSeconds)
        {
            secondsPressed = Time.time - holdStartTime;

            holdMeter.value = holdMeter.maxValue*(secondsPressed/holdSeconds);

            yield return null;
        }

        if(!pressed)
        {
            hideMeters();
        }

        else
        {
            collect();

            cooldownStartTime = Time.time;
            secondsPassed = 0;

            onCooldown = true;

            StartCoroutine(updateCooldownMeter());

            Debug.Log("pressing completed, on cooldown now");
        }
    }

    private IEnumerator updateCooldownMeter()
    {
        cooldownMeter.value = cooldownMeter.maxValue;

        while(secondsPassed < cooldownSeconds)
        {
            secondsPassed = Time.time - cooldownStartTime;

            cooldownMeter.value = cooldownMeter.maxValue*(1 - secondsPassed/cooldownSeconds);

            yield return null;
        }

        onCooldown = false;

        hideMeters();

        Debug.Log("can interact again");
    }

    private void displayMeter()
    {
        storageScreenPosition = gameCamera.WorldToScreenPoint(transform.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(screenRect, storageScreenPosition, gameCamera, out localPosition);
        
        if(onCooldown)
        {
            holdMeterRect.gameObject.SetActive(false);
            cooldownMeterRect.gameObject.SetActive(true);

            cooldownMeterRect.anchoredPosition = localPosition;   
        } 
        
        else
        {
            cooldownMeterRect.gameObject.SetActive(false);
            holdMeterRect.gameObject.SetActive(true);

            holdMeterRect.anchoredPosition = localPosition;    
        } 
    }

    private void hideMeters()
    {
        holdMeterRect.gameObject.SetActive(false);
        cooldownMeterRect.gameObject.SetActive(false);
    }

    private void collect()
    {
        float randomChance = Random.Range(0.0f, 1.0f);
        Debug.Log("randomChance: " + randomChance);

        if(randomChance <= matchChance)
        {
            MatchController.instance.pickUpMatches();
            Debug.Log("collected matches");
        }
        else
        {
            CandleController.instance.pickUpCandle();
            Debug.Log("collected candle");
        }

    }

}
