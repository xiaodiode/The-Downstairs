using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class Storage : MonoBehaviour
{
    [Header("Cooldown Mechanics")]
    [SerializeField] private float cooldownTotalSeconds;
    [SerializeField] public float cooldownCurrValue;
    [SerializeField] public bool onCooldown;
    public float cooldownStartTime, cooldownTimePassed;

    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        collider.isTrigger = true;

        onCooldown = false;

        cooldownTimePassed = 0;
        cooldownCurrValue = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.GetComponent<TopdownPlayerController>()  != null || 
            other.gameObject.GetComponent<SidescrollPlayerController>() != null)
        {
            StorageController.instance.currentStorage = this;
        }
    }

    public void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.GetComponent<TopdownPlayerController>()  != null || 
            other.gameObject.GetComponent<SidescrollPlayerController>() != null)
        {
            StorageController.instance.currentStorage = null;
        }
    }

    public IEnumerator updateCooldownMeter()
    {
        cooldownTimePassed = 0;

        onCooldown = true;
        while(cooldownTimePassed < cooldownTotalSeconds)
        {
            if(GameManager.instance.gamePaused) yield return null;

            else
            {
                cooldownTimePassed += Time.deltaTime;

                cooldownCurrValue = StorageController.instance.cooldownMeter.maxValue*(1 - cooldownTimePassed/cooldownTotalSeconds);

                yield return null;
            }
        }

        onCooldown = false;

        StorageController.instance.hideMeters();

    }

}
