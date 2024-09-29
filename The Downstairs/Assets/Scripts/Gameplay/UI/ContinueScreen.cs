using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContinueScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nightCounter;
 
    [Header("Resources")]
    [SerializeField] private TextMeshProUGUI fridgeUsage;
    [SerializeField] private TextMeshProUGUI pitcherUsage;
    [SerializeField] private TextMeshProUGUI laundryUsage;
    [SerializeField] private TextMeshProUGUI matchCount;
    [SerializeField] private TextMeshProUGUI candleCount;

    public static ContinueScreen instance {get; private set;}

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null && instance != this){
            Destroy(this);
        }
        else{
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateText()
    {
        nightCounter.text = "Night " + GameManager.instance.nightCount.ToString() + " Survived";

        fridgeUsage.text = Fridge.instance.foodQuantity.ToString() + "Uses Left";
        pitcherUsage.text = WaterPitcher.instance.waterQuantity.ToString() + "Uses Left";
        laundryUsage.text = Laundry.instance.laundryQuantity.ToString() + "Uses Left";

        matchCount.text = MatchController.instance.matchCount.ToString() + "Left";
        candleCount.text = CandleController.instance.candleCount.ToString() + "Left";
    }
}
