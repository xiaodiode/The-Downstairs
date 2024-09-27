using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class EnterFont : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI tmp;
    public float timeDilate = 2.0f;

    void OnEnable()
    {
        tmp = this.GetComponent<TextMeshProUGUI>();
        this.tmp.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, -1.0f); 


        DOTween.To(GetFaceDilate, SetFaceDilate, 0.0f, timeDilate);
    }

    void SetFaceDilate(float x)
    {
        this.tmp.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, x);
    }
    float GetFaceDilate()
    {
        return this.tmp.fontMaterial.GetFloat(ShaderUtilities.ID_FaceDilate);
    }

}
