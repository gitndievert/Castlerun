// ********************************************************************
// CONFIDENTIAL - DO NOT DISTRIBUTE
// COPYRIGHT 2019-2020 Wacky Potato Games, LLC. All Rights Reserved.
// 
// If you send, receive, or use this file for any purpose other than
// internal use by Wacky Potato Games, it is without permission and an act of theft.
// Report any misuse of this file immediately to contact@wackypotato.com
// Misuse or failure to report misuse will subject you to legal action.
// 
// The intellectual and technical concepts contained herein are
// proprietary and are protected by trade secret and/or copyright law.
// Dissemination or reproduction of this material is forbidden.
// ********************************************************************

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