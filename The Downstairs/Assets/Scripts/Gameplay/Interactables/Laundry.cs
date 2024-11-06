using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Laundry : MonoBehaviour
{
    public int currLaundryQuantity;
    [SerializeField] private int startingLaundryQuantity;
    [SerializeField] private TextMeshProUGUI laundryQuantityUI;
    private bool triggerable;
    public static Laundry instance {get; private set;}

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
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        checkToilet();

        if(triggerable && Input.GetKeyDown(KeyCode.Space))
        {
            if(MetersController.instance.lockBedroom)
            {
                useLaundry();
            }
            else
            {
                Dialogue.instance.triggerLaundryDialogue("unused");
            }
        }
    }

    private void checkToilet()
    {
        if(MetersController.instance.toiletMeter.isEmpty && !MetersController.instance.sanityMeter.toiletEffect)
        {
            Debug.Log("toilet is empty");
            
            MetersController.instance.sanityMeter.changeMultiplier(0.5f);
            MetersController.instance.lockBedroom = true;

            // StartCoroutine(MetersController.instance.sanityMeter.decreaseMeter());
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<TopdownPlayerController>()  != null) triggerable = true;
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<TopdownPlayerController>()  != null) triggerable = false;
    }

    private void useLaundry()
    {
        if(currLaundryQuantity > 0)
        {
            currLaundryQuantity--;
            updateLaundryText();

            MetersController.instance.lockBedroom = false;
        }
        else
        {
            Dialogue.instance.triggerLaundryDialogue("empty");
        }
    }

    private void updateLaundryText()
    {
        laundryQuantityUI.text = "Laundry Uses: " + currLaundryQuantity.ToString();
    }

    public void resetLaundry()
    {
        triggerable = false;

        currLaundryQuantity = startingLaundryQuantity;

        updateLaundryText();
    }
}
