using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightEclipse : MonoBehaviour
{
    public float lenShortAxis;
    public float lenLongAxis;
    public GameObject[] objs = null;

    public float angle
    {
        set => candleTransform.localPosition = new Vector3(Mathf.Cos(value) * lenLongAxis, Mathf.Sin(value) * lenShortAxis, 0f);
    }
    
    [SerializeField] private Transform candleTransform;
    
}
