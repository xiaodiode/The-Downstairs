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

    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        collider.isTrigger = true;
        
        triggerable = false;

        onCooldown = false;

        screenRect = holdMeterRect.transform.parent as RectTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if(triggerable)
        {
            displayMeter();
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
        }
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

}
