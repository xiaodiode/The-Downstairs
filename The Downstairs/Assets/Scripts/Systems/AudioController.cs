using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [Header("Music Clips")]
    [SerializeField] private AudioClip MainMenuBGM;
    [SerializeField] private AudioClip DownstairsBGM;
    [SerializeField] private AudioClip GameOverBGM;

    [Header("SFX Clips")]


    [Header("Music Components")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private float musicVolume;
    [SerializeField] private AudioSource musicSource;
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

    public void setMusic()
    {
        musicVolume = musicSlider.value*100f;

        musicSource.volume = musicVolume;
    }

    public void setSFX()
    {
        sfxVolume = sfxSlider.value*100f;

        sfxSource.volume = sfxVolume;
    }

    public void playGameplayMusic()
    {
        transitionMusic(DownstairsBGM);
        musicSource.loop = false;
    }

    public void playMainMenuMusic()
    {
        transitionMusic(MainMenuBGM);
        musicSource.loop = true;
    }

    private void transitionMusic(AudioClip newClip)
    {
        musicSource.Stop();

        musicSource.clip = newClip;
        currMusicClip = newClip;

        musicSource.volume = musicVolume;
        musicSource.Play();
    }
}
