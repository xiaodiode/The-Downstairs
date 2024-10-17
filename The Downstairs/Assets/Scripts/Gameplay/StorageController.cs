using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageController : MonoBehaviour
{
    public Storage currentStorage;

    [Header("Storage Features")]
    [SerializeField] [Range(0.0f, 1.0f)] private float matchChance;
    
    [Header("Storage Meter UI")]
    [SerializeField] private Slider holdMeter;
    [SerializeField] private float holdSeconds;
    [SerializeField] public Slider cooldownMeter;

    [Header("Meter Placement Structure")]
    [SerializeField] private Canvas UICanvas;
    [SerializeField] private Camera gameCamera;
    [SerializeField] private RectTransform holdMeterRect;
    [SerializeField] private RectTransform cooldownMeterRect;
    private RectTransform screenRect;
    private Vector3 storageScreenPosition;
    private Vector2 localPosition;

    private float holdStartTime, secondsPressed;

    private bool pressed;

    public static StorageController instance {get; private set;}

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
        currentStorage = null;

        screenRect = holdMeterRect.transform.parent as RectTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentStorage != null)
        {
            checkInteraction();
        }
        else
        {
            hideMeters();
        }
    }

    public void displayMeter()
    {
        storageScreenPosition = gameCamera.WorldToScreenPoint(currentStorage.transform.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(screenRect, storageScreenPosition, gameCamera, out localPosition);
        
        if(currentStorage.onCooldown)
        {
            holdMeterRect.gameObject.SetActive(false);
            cooldownMeterRect.gameObject.SetActive(true);

            cooldownMeter.value = currentStorage.cooldownCurrValue;

            cooldownMeterRect.anchoredPosition = localPosition;   
        } 
        
        else
        {
            cooldownMeterRect.gameObject.SetActive(false);
            holdMeterRect.gameObject.SetActive(true);

            holdMeterRect.anchoredPosition = localPosition;    
        } 
    }

    public void hideMeters()
    {
        holdMeterRect.gameObject.SetActive(false);
        cooldownMeterRect.gameObject.SetActive(false);
    }

    private void checkInteraction()
    {
        if(!currentStorage.onCooldown)
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

        holdMeter.value = 0;

        while(pressed && secondsPressed < holdSeconds)
        {
            if(GameManager.instance.gamePaused) yield return null;

            else
            {
                secondsPressed += Time.deltaTime;

                holdMeter.value = holdMeter.maxValue*(secondsPressed/holdSeconds);

                yield return null;
            }
        }

        if(!pressed)
        {
            hideMeters();
        }

        else
        {
            collect();

            StartCoroutine(currentStorage.updateCooldownMeter());

            // Debug.Log("pressing completed, on cooldown now");
        }
    }

    public void collect()
    {
        float randomChance = Random.Range(0.0f, 1.0f);
        Debug.Log("randomChance: " + randomChance);

        if(MatchController.instance.matchesFull && CandleController.instance.candlesFull)
        {
            Dialogue.instance.addToDialogue("Don't think I can hold any more");
        }
        else if(MatchController.instance.matchesFull)
        {
            CandleController.instance.pickUpCandle();
        }
        else if(CandleController.instance.candlesFull)
        {
            MatchController.instance.pickUpMatches();
        }
        else
        {
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
}
