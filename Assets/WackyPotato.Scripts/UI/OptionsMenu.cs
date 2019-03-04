using System.Collections;
using System.Collections.Generic;
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
