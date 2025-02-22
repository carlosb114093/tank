using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set;}
    [SerializeField] AudioSource sfxAudio, musicAudio;    
    public AudioClip initialMusic;
    [SerializeField] AudioMixer master;
    [Range (-5,5)]
    public float musicVolume, sfxVolume;

    private void Awake() {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        sfxAudio = transform.GetChild(0).GetComponent<AudioSource>();
        musicAudio = transform.GetChild(1).GetComponent<AudioSource>();
        InitialPlayMusic(initialMusic);
    }

    private void Update(){
     MusicVolumeControl (musicVolume);
     SFXVolumeControl (sfxVolume);

    }

    public void PlaySFX(AudioClip clip)
    {
        sfxAudio.PlayOneShot(clip);
    }
    public void PlayMusic(AudioClip clip)
    {
        musicAudio.Stop();
        musicAudio.clip = clip;
        musicAudio.Play();
        musicAudio.loop = true;
    }
    void InitialPlayMusic(AudioClip clip)
    {
        musicAudio.clip = clip;
        musicAudio.Play();
        musicAudio.loop = true;
    }

    public void MusicVolumeControl (float volume){
        master.SetFloat ("Music", Mathf.Log10(volume) * 20);
    }

    public void SFXVolumeControl(float volume){
        master.SetFloat ("SFX", Mathf.Log10(volume) * 20);
    }
}