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

    private void Start()
    {
        if (offsetObject == null)
        {
            Debug.LogError("Offset Object is not assigned!");
            return;
        }
        Vector3 offset = offsetObject.transform.position - transform.position;
        offsets = new List<Vector3>();
        for (int i = 0; i < bottomStairIndex; i++)
        {
            offsets.Add(transform.position + offset * (i));
        }
        offsetObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    private void SetOffset(int off)
    {
        currentOffset = off;
        transform.position = offsets[off];
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

}
