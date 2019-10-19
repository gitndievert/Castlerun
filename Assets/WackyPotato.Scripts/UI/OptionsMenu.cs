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

using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider MusicSlider;
    public Slider AudioSlider;
	
    /*void OnEnable()
    {
        GameState.DragEnabled = false;
        MusicSlider.value = Music.Instance.VolumeLevel;
        AudioSlider.value = SoundManager.Instance.VolumeLevel;
    }

    void OnDisable()
    {
        GameState.DragEnabled = true;
    }

    public void ChangeMusicVol()
    {
        Music.Instance.Volume(MusicSlider.value);
    }
    public void ChangeSoundVol()
    {
        SoundManager.Instance.Volume(AudioSlider.value);
    }*/
    
}
