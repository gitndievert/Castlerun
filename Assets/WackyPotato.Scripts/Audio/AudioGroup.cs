using System;
using UnityEngine;

public class AudioGroup : MonoBehaviour
{
    public string Name;

    public AudioGroupClip[] AudioClip;
    public AudioGroupClips[] AudioClips;    

    private void Start()
    {
        Name = transform.name;
    }
}

[Serializable]
public struct AudioGroupClip
{
    public string Name;
    public AudioClip Clip;
}

[Serializable]
public struct AudioGroupClips
{
    public string Name;
    public AudioClip[] Clips;
}