using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContinueScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nightCounter1;
    [SerializeField] private TextMeshProUGUI nightCounter2;

    [Header("Resources")]
    [SerializeField] private TextMeshProUGUI fridgeUsage;
    [SerializeField] private TextMeshProUGUI pitcherUsage;
    [SerializeField] private TextMeshProUGUI laundryUsage;
    [SerializeField] private TextMeshProUGUI matchCount;
    [SerializeField] private TextMeshProUGUI candleCount;

    public static ContinueScreen instance {get; private set;}

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
        nightCounter1.text = GameManager.instance.nightCount.ToString() + " NIGHTS \nSURVIVED";
        nightCounter2.text = GameManager.instance.nightCount.ToString() + " NIGHTS \nSURVIVED";

        fridgeUsage.text = Fridge.instance.foodQuantity.ToString() + "Uses Left";
        pitcherUsage.text = WaterPitcher.instance.waterQuantity.ToString() + "Uses Left";
        laundryUsage.text = Laundry.instance.laundryQuantity.ToString() + "Uses Left";

        matchCount.text = MatchController.instance.matchCount.ToString() + "Left";
        candleCount.text = CandleController.instance.candleCount.ToString() + "Left";

        GameManager.instance.increaseNightCount();
    }
}
