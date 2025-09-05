
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    public AudioStruct[] AudioStruct;
    public Sounds[] BGSounds;


    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;


    [Header("Sounds Parameters")]
    [SerializeField] string backgroundName;
    [SerializeField] string SFXName;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            
        }
        musicSource = transform.Find("Music").GetComponent<AudioSource>();
        SFXSource = transform.Find("SFX").GetComponent<AudioSource>();
        // 
        //
        // foreach (Sounds s in BGSounds)
        // {
        //     s.audioClip.LoadAudioData();
        // }
        // foreach (AudioStruct s in AudioStruct)
        // {
        //     foreach (var clip in s.audioClips)
        //     {
        //         clip.LoadAudioData();
        //     }
        // }
        //
        // /*foreach (Sounds sound in BGSounds)
        // {
        //
        //     sound.audioSource = gameObject.AddComponent<AudioSource>();
        //     sound.audioSource.clip = sound.audioClip;
        //     sound.audioSource.volume = sound.volume;
        //     sound.audioSource.pitch = sound.pitch;
        //     sound.audioSource.loop = sound.loop;
        //
        //
        // }*/
    }







    private void Start()
    {
        

        //musicSource.clip = BGSounds[0].audioClip;
        //musicSource.Play();

    }

    private void Update()
    {
      // if (!musicSource.isPlaying)
      // {
      //     PlayBackground();
      // }


    }

    /*public void PlaySFXList(AudioClip[] clip)
    {
        SFXSource.PlayOneShot(clip[UnityEngine.Random.Range(0, clip.Length)]);
    }


    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }*/

    
    public void PlaySFX(string name)
    {
        //if (AudioStruct == null || AudioStruct.Length==0 && SFXName.Length <=0) return;
        AudioStruct s = Array.Find(AudioStruct, sound => sound.name == name);

        if (name == "Move")
        {
            SFXSource.pitch = UnityEngine.Random.Range(0.5f, 1f);
        }
        else SFXSource.pitch = s.pitch;
        SFXSource.volume = s.volume;
        SFXSource.loop = s.loop;
        SFXSource.PlayOneShot(s.audioClips[UnityEngine.Random.Range(0, s.audioClips.Length)], SFXSource.volume);
    }

    [NaughtyAttributes.Button]
    public void PlayBackground()
    {
        if(BGSounds == null || BGSounds.Length == 0) return;
        Sounds s;
        if (backgroundName.Length>0)
        {
             s = Array.Find(BGSounds, music => music.name == backgroundName);
        }
        else
        {
            s = BGSounds[UnityEngine.Random.Range(0, BGSounds.Length)];
        }
        Debug.Log("Playing Background: " + s);
        musicSource.clip = s.audioClip;
        musicSource.volume = s.volume;
        musicSource.pitch = s.pitch;
        musicSource.loop = s.loop;
        musicSource.Play();
    }


   



}
