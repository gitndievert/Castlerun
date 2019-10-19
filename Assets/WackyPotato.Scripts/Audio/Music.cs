﻿// ********************************************************************
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

using UnityEngine;
using SBK.Unity;
using System;

public class Music : PSingle<Music>, IAudio
{
    public AudioClip[] MusicClips;
    public static int MusicIndex { get; set; }
    public float VolumeLevel { get; set; }

    public float FadeOutTime = 3f;
    public float FadeInTime = 3f;    

    public const float FadeStopTime = 0.5f;

    [HideInInspector]
    public bool MusicEnabled = true;

    //Remove when ready
    public void Volume(float percent)
    {
        throw new NotImplementedException();
    }

    
    protected override void PAwake()
    {
                
    }

    protected override void PDestroy()
    {

    }
    /*
    void Update()
    {
        if (SceneSelector.Instance == null) return;
        int jungleTrack = (int)MusicTracks.Jungle;
        switch (SceneSelector.Instance.CurrentBoard)
        {
            case BoardType.Adventure:
            case BoardType.FreeMap:
                if (MusicIndex == 0 || MusicIndex == 2)
                {
                    UnPauseTrack();
                    MusicIndex = jungleTrack;
                }
                break;
            case BoardType.Puzzle:
                if (MusicIndex == jungleTrack)
                {
                    PauseTrack();
                    MusicIndex = 0;
                }
                break;
        }
    }*/
    
    public void PlayMusicTrack(int index)
    {
        MusicIndex = index;

        if (MusicEnabled)
        {
            MusicManager.play(MusicClips[MusicIndex], FadeOutTime, FadeInTime);
        }
    }

    public void PauseTrack()
    {
        MusicManager.pause();
    }

    public void UnPauseTrack()
    {
        MusicManager.unpause();
    }

    public void StopMusic()
    {
        MusicManager.stop(FadeStopTime);
    }

    /*public void Volume(float percent)
    {
        MusicManager.setVolume(percent);
        GameState.SettingsData.MusicVolume = percent;
        VolumeLevel = percent;
    }*/
}