using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DKJAnimator : MonoBehaviour
{

    public int bottomStairIndex = 5; // Number of duplicates to create
    public GameObject offsetObject; // The object used to determine the offset/spacing
    private List<Vector3> offsets;
    private int currentOffset;
    private bool isGoingDown;
    public Sprite[] sprites;
    public Sprite missSprite;
    private SpriteRenderer rdr;
    public Color missColor;
    public float flashDuration = 0.4f;  // Flash duration in seconds
    public float flashInterval = 0.1f;  // Interval between color changes (for flashing effect)

    private void Start()
    {
        if (offsetObject == null)
        {
            Debug.LogError("Offset Object is not assigned in DKJ Animator!");
            return;
        }
        Vector3 offset = (offsetObject.transform.position - transform.position)/bottomStairIndex;
        offsets = new List<Vector3>();
        for (int i = 0; i < bottomStairIndex; i++)
        {
            offsets.Add(transform.position + offset * (i));
        }
        offsetObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        rdr = GetComponentInChildren<SpriteRenderer>();
    }

    private void SetOffset(int off)
    {
        currentOffset = off;
        transform.position = offsets[off];
        rdr.sprite = sprites[off % sprites.Length];
    }

    public void ResetGoingDown()
    {
        isGoingDown = true;
        SetOffset(0);
    }

    public void ResetGoingUp()
    {
        isGoingDown = false;
        SetOffset(bottomStairIndex-1);
    }

    public void IncrementOffset()
    {
        int newOffset = currentOffset;
        if (isGoingDown) 
            newOffset++;
        else 
            newOffset--;

        if (newOffset < 0 || newOffset >= bottomStairIndex) {
            Debug.LogWarning("Incrementing out of range.");
        } else {
            SetOffset(newOffset);
        }
    }

    public void StartAnimateMiss() 
    {
        StartCoroutine(AnimateMiss());
    }

    private IEnumerator AnimateMiss()
    {

        rdr.sprite = missSprite;
        
        for (float time = 0; time < flashDuration; time += flashInterval)
        {
            rdr.color = (rdr.color == missColor) ? Color.black : missColor;
            yield return new WaitForSeconds(flashInterval);
        }
        
        rdr.color = Color.black;
        IncrementOffset();

    }

}
