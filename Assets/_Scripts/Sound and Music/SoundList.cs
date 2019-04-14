using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SBK.Unity;

/// <summary>
/// This class will contain all the basic sounds used throughout the game
/// </summary>
public class SoundList : PSingle<SoundList>
{
    public AudioClip BuildSound;
    public AudioClip[] Swinging;
    public AudioClip[] Death;
    public AudioClip[] Grunts;


    protected override void PAwake()
    {
        
    }

    protected override void PDestroy()
    {
        
    }
}
