using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class GraphicsSettings : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown qualityOptions;
    [SerializeField] private TMP_Dropdown resolutionOptions;
    [SerializeField] private TMP_Dropdown framerateOption;
     

    private float _animDistance = 400f;  
    private float _animTime = 1.1f; //.95f;   anim 'glitch' on enter mainly
 
    void Start() {
        transform.localScale = new Vector3(0f, 0f, 0f);
     }
    void OnEnable() {   
        transform.position = new Vector3(transform.position.x, transform.position.y-_animDistance, transform.position.z); 
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.75f).SetUpdate(true);
        transform.DOMoveY(transform.position.y+_animDistance, _animTime).SetEase(Ease.OutBounce).SetUpdate(true);
    }

    public void CloseGraphics() {
        transform.DOScale( new Vector3(0f, 0f, 0f), .75f).SetUpdate(true);  
        transform.DOMoveY(transform.position.y-_animDistance, .75f).SetUpdate(true).SetEase(Ease.OutBack).onComplete = SetOff;

    }

    private void SetOff(){        
        transform.position = new Vector3(transform.position.x, transform.position.y+_animDistance, transform.position.z); 
        transform.localScale = new Vector3(0f, 0f, 0f);
        gameObject.SetActive(false);
    }

    public void SetQuality(int index) {
        if(index == 1) index = 2; // set only to performance or graphics
        QualitySettings.SetQualityLevel(index, false);
    }

    public void SetResolution() {
        int index = resolutionOptions.value;
        if(index==0) Screen.fullScreen = false;
        if(index==1) Screen.fullScreen = true; 
    }

    public void SetFrameRate() {
        int index = framerateOption.value;
        if(index==0) Application.targetFrameRate = 120;
        if(index==1) Application.targetFrameRate = 90;
        if(index==2) Application.targetFrameRate = 60;
        if(index==3) Application.targetFrameRate = 30; 
    } 
 

    public void setFontSize(){
        
    }
}