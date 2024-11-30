using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleQTE : MonoBehaviour
{
    public KeyCode key;
    public GameObject DKJAnimationMC;
    public GameObject KeyTarget;
    public GameObject ClickHighlighter;
    private DKJAnimator dkjanimator;
    private ClickHilite clkhlt;
    public float hitWindow = 0.3f;
    public float inputCooldownTime = 1f;
    public float targetDistance = 5;
    public float targetTime = 2;
    private float overshootDistance;
    private float overshootTime;
    private Vector3 overshootPosition;
    private Vector3 startPosition;
    private float opaqueTime;
    private float hitVanishTime;
    private float missVanishTime;
    private bool done = false;
    private bool miss = false;
    private float missPenalty;
    private float scaleAmtS;
    private float shakeMagnitude;
    // Start is called before the first frame update
    void Start()
    {
        QTEController qtec = FindObjectOfType<QTEController>();
        if (qtec != null) 
        {
            DKJAnimationMC = qtec.DKJAnimationMC;
            ClickHighlighter = qtec.ClickHighlighter;
            KeyTarget = qtec.KeyTarget;
            hitWindow = qtec.hitWindow;
            inputCooldownTime = qtec.inputCooldownTime;
            targetDistance = qtec.targetDistance;
            hitVanishTime = qtec.hitVanishTime;
            missVanishTime = qtec.missVanishTime;
            missPenalty = qtec.missPenalty;
            scaleAmtS = qtec.scaleAmtS;
            shakeMagnitude = qtec.shakeMagnitude;
        }
        dkjanimator = DKJAnimationMC.GetComponent<DKJAnimator>();
        clkhlt = ClickHighlighter.GetComponent<ClickHilite>();
        overshootDistance = targetDistance / targetTime;
        overshootPosition = KeyTarget.transform.position + new Vector3(-overshootDistance, 0, 0);
        overshootTime = 1f + targetTime;
        startPosition = KeyTarget.transform.position + new Vector3(targetDistance, 0, 0);
        opaqueTime = targetTime/4;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isDone()
    {
        return done;
    }

    private void recordMiss()
    {
        dkjanimator.StartAnimateMiss();
        MetersController.instance.sanityMeter.changeByAmount(-missPenalty);
        AudioManager.instance.playQTEMiss();
    }
    private void recordHit()
    {
        dkjanimator.IncrementOffset();
        AudioManager.instance.playQTEHit();
    }

    public IEnumerator StartQTE()
    {
        float startTime = Time.time;
        float lastKeyPressTime = -Mathf.Infinity;
        float hitWindowStartTime = startTime + targetTime - hitWindow/2;
        float hitWindowEndTime = startTime + targetTime + hitWindow/2;

        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
        Color opaque = renderer.color;
        Color transparent = new Color(opaque.r, opaque.g, opaque.b, 0f);
        Color greentransparent = new Color(0f, opaque.g, 0f, 0f);
        Color redtransparent = new Color(opaque.r, 0f, 0f, 0f);

        transform.position = startPosition;
        transform.localScale = new Vector3(scaleAmtS, scaleAmtS, 0);
        renderer.color = transparent;
        renderer.enabled = true;

        Vector3 shake = Vector3.zero;
        Vector3 originalScale = transform.localScale;

        while (Time.time - startTime < overshootTime)
        {
            // Linearly translate
            float t = (Time.time - startTime) / overshootTime;  
            transform.position = Vector3.Lerp(startPosition, overshootPosition, t) + shake;
            if (Time.time - startTime < opaqueTime) {
                // Fade in
                float c = (Time.time - startTime) / opaqueTime;  
                renderer.color = Color.Lerp(transparent, opaque, c);
            } else if (Time.time - hitWindowEndTime < missVanishTime && Time.time > hitWindowEndTime) {
                // Fade out on miss
                if (!miss) {
                    recordMiss();
                }
                miss = true;
                float c = (Time.time - hitWindowEndTime) / missVanishTime;  
                renderer.color = Color.Lerp(opaque, redtransparent, c);
                transform.localScale = Vector3.Lerp(originalScale, originalScale*.7f, c);
                shake = new Vector3(
                    Random.Range(-shakeMagnitude, shakeMagnitude),
                    Random.Range(-shakeMagnitude, shakeMagnitude),
                    0);
            } else if (Time.time - hitWindowEndTime > missVanishTime) {
                renderer.enabled = false;
                done = true;
                yield break;
            }
            // Validate key inputs
            if(Input.anyKeyDown)
            {
                if (Time.time - lastKeyPressTime >= inputCooldownTime) {
                    clkhlt.Click();
                    if (Input.GetKeyDown(key) && Time.time >= hitWindowStartTime && Time.time <= hitWindowEndTime) {
                        recordHit();
                        float hitTime = Time.time;
                        while (Time.time - hitTime < hitVanishTime) {
                            // Fade out and stop movement on hit
                            float c = (Time.time - hitTime) / hitVanishTime;  
                            renderer.color = Color.Lerp(opaque, greentransparent, c);
                            transform.localScale = Vector3.Lerp(originalScale, originalScale*1.5f, c);
                            yield return null;
                        }
                        renderer.enabled = false;
                        done = true;
                        yield break;
                    }
                }
                lastKeyPressTime = Time.time;
            }
            yield return null;
        }
        transform.position = overshootPosition;
    }
}
