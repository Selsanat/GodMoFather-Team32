
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioStruct[] AudioStruct;
    public Sounds[] BGSounds;


    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;



    private void Awake()
    {
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
        AudioStruct s = Array.Find(AudioStruct, sound => sound.name == name);

        SFXSource.volume = s.volume;
        SFXSource.pitch = s.pitch;
        SFXSource.loop = s.loop;
        SFXSource.PlayOneShot(s.audioClips[UnityEngine.Random.Range(0, s.audioClips.Length)], SFXSource.volume);
    }

    public void PlayBackground()
    {
        Sounds s = BGSounds[UnityEngine.Random.Range(0, BGSounds.Length)];
        //Sounds s = Array.Find(BGSounds, sound => sound.name == name);

        musicSource.clip = s.audioClip;
        musicSource.volume = s.volume;
        musicSource.pitch = s.pitch;
        musicSource.loop = s.loop;
        musicSource.Play();
    }


   



}
