using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using Unity.VisualScripting;
using UnityEngine;

public class ClickHilite : MonoBehaviour
{
    public float flashInterval = .10f;
    private SpriteRenderer rdr;
        
    void Start()
    {
        rdr = GetComponentInChildren<SpriteRenderer>();
        rdr.enabled = false;
    }

    public void Click()
    {
        StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        rdr.enabled = true;
        yield return new WaitForSeconds(flashInterval);
        rdr.enabled = false;
    }
}
