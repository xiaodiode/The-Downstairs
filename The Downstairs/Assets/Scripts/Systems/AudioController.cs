using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    [Header("Music Components")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private float musicVolume;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip mmMusicClip;
    [SerializeField] private AudioClip gameplayMusicClip;
    [SerializeField] private AudioClip currMusicClip;

    [SerializeField] private float transitionDuration;

    [Header("Sound FX Components")]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private float sfxVolume;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip sfxClip;

    public static AudioController instance {get; private set;}

    // Start is called before the first frame update
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playGameplayMusic(){

        transitionMusic(gameplayMusicClip);
        musicSource.loop = false;
    }

    public void playMainMenuMusic(){
        transitionMusic(mmMusicClip);
        musicSource.loop = true;
    }

    private void transitionMusic(AudioClip newClip){
        musicSource.Stop();

        musicSource.clip = newClip;
        currMusicClip = newClip;

        musicSource.volume = musicVolume;
        musicSource.Play();
    }
}
