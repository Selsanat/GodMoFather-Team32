using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct AudioStruct 
{
    public string name;

    [Range(0f, 1f)]
    public float volume;

    [Range(0f, 1f)]
    public float pitch;

    public bool loop;
    public AudioClip[] audioClips;
}
