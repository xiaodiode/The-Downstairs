using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class EnterFont : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float timeDilate;
    private TextMeshProUGUI tmp;

    void Start()
    {   
        tmp = gameObject.GetComponent<TextMeshProUGUI>();
        tmp.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, -1.0f); 
    }

    void SetFaceDilate(float x)
    {
        tmp.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, x);
    }
    
    float GetFaceDilate()
    {
        return tmp.fontMaterial.GetFloat(ShaderUtilities.ID_FaceDilate);
    }

    public void StartDilation()
    {
        DOTween.To(GetFaceDilate, SetFaceDilate, 0.0f, timeDilate);
    }

    public void EndDilation()
    {
        DOTween.To(GetFaceDilate, SetFaceDilate, -1.0f, timeDilate);
    }

}
