using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public bool isGameOver;

    [Header("Screen Elements")]
    [SerializeField] private RawImage background;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private List<TextMeshProUGUI> otherText;
    

    [Header("Animation Settings")]
    [SerializeField] private float fadeInBgTime;
    [SerializeField] private Vector2 bgAlphaRange;
    [SerializeField] private float textDelay;
    [SerializeField] private float fadeInTextTime, fadeOutTextTime;
    [SerializeField] private float titleDelay;
    [SerializeField] private float fadeInTitleTime, fadeOutTitleTime;
    [SerializeField] private Vector2 textAlphaRange;
    private EnterFont titleTextAnim;
    private float secondsPassed;
    private Color newColor;


    public static GameOver instance {get; private set;}

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
    void OnEnable()
    {
        titleTextAnim = titleText.gameObject.GetComponent<EnterFont>();

        newColor = otherText[0].color;
        newColor.a = 0;

        foreach(TextMeshProUGUI text in otherText)
        {
           text.color = newColor;
        }

        isGameOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator fadeIn()
    {
        secondsPassed = 0;
        Color newBgColor = background.color;

        while(secondsPassed < fadeInBgTime)
        {
            newBgColor.a = Mathf.Lerp(0.0f, 1.0f, secondsPassed/fadeInBgTime);
            background.color = newBgColor;

            secondsPassed += Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(textDelay);

        secondsPassed = 0;
        Color textColor = otherText[0].color;
        Color titleColor = titleText.color;

        titleTextAnim.StartDilation();

        while(secondsPassed < (fadeInTextTime + 2*titleDelay))
        {
            if(secondsPassed <= fadeInTextTime + titleDelay)
            {
                foreach(TextMeshProUGUI text in otherText)
                {
                    textColor.a = Mathf.Lerp(textAlphaRange.x, textAlphaRange.y, secondsPassed/(fadeInTextTime + titleDelay));
                    text.color = textColor;
                }
            }
            

            if(secondsPassed >= titleDelay && (secondsPassed < (fadeInTextTime + 2*titleDelay)))
            {
                titleColor.a = Mathf.Lerp(textAlphaRange.x, textAlphaRange.y, secondsPassed/(fadeInTextTime + titleDelay));
                titleText.color = titleColor;
            }

            secondsPassed += Time.deltaTime;

            yield return null;
        }

        
        
    }


}
