using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class CrawlingController : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] public float crawlTime;
    [SerializeField] public float tripTime;
    [SerializeField] public float stayTime;
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
        DownStay,
        DownPickup,
        UpCrawl,
        UpTrip,
        UpStay,
        UpPickup
    }

    private Dictionary<CrawlingState, string> stateNames;

    // Start is called before the first frame update
    void Start()
    {
        stateNames = new()
        {
            { CrawlingState.DownCrawl, "Down Crawl" },
            { CrawlingState.DownTrip, "Down Trip"},
            { CrawlingState.DownStay, "Down Stay"},
            { CrawlingState.DownPickup, "Down Pickup" },
            { CrawlingState.UpCrawl, "Up Crawl" },
            { CrawlingState.UpTrip, "Up Trip" },
            { CrawlingState.UpStay, "Up Stay" },
            { CrawlingState.UpPickup, "Up Pickup" }
        };

        setAnimSpeeds();

        goingDown = true;

        AddCrawl();
        AddCrawl();
        AddTrip();
        AddCrawl();
        StartCoroutine(StartCrawling());
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

                case "Up Crawl":
                    animator.SetFloat("Up Crawl Multiplier", clipLength/crawlTime);
                    break;

                case "Up Trip":
                    animator.SetFloat("Up Trip Multiplier", clipLength/tripTime);
                    break;
            }
        
        }
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

        // animator.Play(stateNames[CrawlingState.UpCrawl]);

        // yield return new WaitForSeconds(4);

        // Debug.Log("playing second");

        // animator.Play(stateNames[CrawlingState.UpCrawl], 0, 0f);

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
                animator.Play(stateNames[CrawlingState.UpTrip], 0, 0f);

                yield return new WaitForSeconds(tripTime);

                Debug.Log("tripped");

                animator.Play(stateNames[CrawlingState.UpTrip], 0, 0f);

                yield return new WaitForSeconds(tripTime);

                Debug.Log("hold");

                animator.Play(stateNames[CrawlingState.UpTrip], 0, 0f);

                yield return new WaitForSeconds(tripTime);

                Debug.Log("picked up");

            }

            statesQueue.Dequeue();
        }
    }

}
