using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class CrawlingController : MonoBehaviour
{
    public bool ready;

    [Header("Animation Settings")]
    [SerializeField] public float crawlTime;
    [SerializeField] public float tripTime;
    [SerializeField] public float holdTime;
    [SerializeField] public float pickupTime;

    [Header("Animation Mechanics")]
    [SerializeField] private Animator animator;
    [SerializeField] private AnimatorController animController;
    [SerializeField] private bool goingDown;
    [SerializeField] private Queue<CrawlingState> statesQueue = new();

    public enum CrawlingState 
    {
        Crawl,
        Trip,
        DownCrawl,
        DownTrip,
        DownHold,
        DownPickup,
        UpCrawl,
        UpTrip,
        UpHold,
        UpPickup
    }

    private Dictionary<CrawlingState, string> stateNames;

    public static CrawlingController instance {get; private set;}

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
        stateNames = new()
        {
            { CrawlingState.DownCrawl, "Down Crawl" },
            { CrawlingState.DownTrip, "Down Trip" },
            { CrawlingState.DownHold, "Down Hold" },
            { CrawlingState.DownPickup, "Down Pickup" },
            { CrawlingState.UpCrawl, "Up Crawl" },
            { CrawlingState.UpTrip, "Up Trip" },
            { CrawlingState.UpHold, "Up Hold" },
            { CrawlingState.UpPickup, "Up Pickup" }
        };

        ready = false;

        setAnimSpeeds();

        goingDown = false;

        // AddCrawl();
        // AddCrawl();
        // AddTrip();
        // AddCrawl();
        // StartCoroutine(StartCrawling());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setAnimSpeeds()
    {
        foreach (var clip in animController.animationClips)
        {   
            float clipLength = clip.length; 

            switch(clip.name)
            {
                case "Down Crawl":
                    animator.SetFloat("Down Crawl Multiplier", clipLength/crawlTime);
                    break;

                case "Down Trip":
                    animator.SetFloat("Down Trip Multiplier", clipLength/tripTime);
                    break;
                
                case "Down Hold":
                    animator.SetFloat("Down Hold Multiplier", clipLength/holdTime);
                    break;

                case "Down Pickup":
                    animator.SetFloat("Down Pickup Multiplier", clipLength/pickupTime);
                    break;

                case "Up Crawl":
                    animator.SetFloat("Up Crawl Multiplier", clipLength/crawlTime);
                    break;

                case "Up Trip":
                    animator.SetFloat("Up Trip Multiplier", clipLength/tripTime);
                    break;

                case "Up Hold":
                    animator.SetFloat("Up Hold Multiplier", clipLength/holdTime);
                    break;

                case "Up Pickup":
                    animator.SetFloat("Up Pickup Multiplier", clipLength/pickupTime);
                    break;
            }
        
        }

        ready = true;
    }

    public void AddCrawl()
    {
        statesQueue.Enqueue(CrawlingState.Crawl);
    }

    public void AddTrip()
    {
         statesQueue.Enqueue(CrawlingState.Trip);
    }

    public IEnumerator StartCrawling()
    {
        while(statesQueue.Count == 0)
        {
            yield return null;
        }

        while(statesQueue.Count != 0)
        {
            if(statesQueue.Peek() == CrawlingState.Crawl)
            {
                if(goingDown) animator.Play(stateNames[CrawlingState.DownCrawl], 0, 0f);
                
                else animator.Play(stateNames[CrawlingState.UpCrawl], 0, 0f);

                yield return new WaitForSeconds(crawlTime);


                Debug.Log("finished crawling");

            }
            else if(statesQueue.Peek() == CrawlingState.Trip)
            {
                if(goingDown)
                {
                    animator.Play(stateNames[CrawlingState.DownTrip], 0, 0f);

                    yield return new WaitForSeconds(tripTime);

                    Debug.Log("tripped");

                    animator.Play(stateNames[CrawlingState.DownHold], 0, 0f);

                    yield return new WaitForSeconds(holdTime);

                    Debug.Log("hold");

                    animator.Play(stateNames[CrawlingState.DownPickup], 0, 0f);

                    yield return new WaitForSeconds(pickupTime);

                    Debug.Log("picked up");
                }
                else
                {
                    animator.Play(stateNames[CrawlingState.UpTrip], 0, 0f);

                    yield return new WaitForSeconds(tripTime);

                    Debug.Log("tripped");

                    animator.Play(stateNames[CrawlingState.UpHold], 0, 0f);

                    yield return new WaitForSeconds(holdTime);

                    Debug.Log("hold");

                    animator.Play(stateNames[CrawlingState.UpPickup], 0, 0f);

                    yield return new WaitForSeconds(pickupTime);

                    Debug.Log("picked up");
                }
                

            }

            statesQueue.Dequeue();
        }
    }

    public float getTripDelay()
    {
        return tripTime + holdTime + pickupTime;
    }

}
