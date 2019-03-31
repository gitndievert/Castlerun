using UnityEngine;
using System.Collections;
using SBK.Unity;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : PSingle<SoundManager>, IAudio
{    
    private static AudioSource _audio;
    private static AudioSource _audio2;    

    public static bool IsPlaying
    {
        get { return _audio.isPlaying; }        
    }    

    public float VolumeLevel { get; set; }

    protected override void PAwake()
    {
        var audioSources = GetComponents<AudioSource>();
        if (audioSources.Length != 2)
            throw new System.Exception("Missing Audio Source Components");
        _audio = audioSources[0];
        _audio2 = audioSources[1];
        _audio.playOnAwake = false;
        _audio2.playOnAwake = false;   
    }

    protected override void PDestroy()
    {
                
    }
    
    public static void PlaySound(AudioClip clip, int channel = 1)
    {
        AudioChan(channel).PlayOneShot(clip);        
    }

    public static void PlaySound(AudioClip[] clips, int channel = 1)
    {
        int rand = Random.Range(0, clips.Length);
        PlaySound(clips[rand],channel);
    }

    public static void PlaySoundOnGameObject(GameObject obj, AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, obj.transform.position);
    }

    public static void PlaySoundOnGameObject(GameObject obj, AudioClip[] clips)
    {
        int rand = Random.Range(0, clips.Length);
        PlaySoundOnGameObject(obj, clips[rand]);
    }

    public static void PlaySoundWithDelay(AudioClip[] clips, float delaySec, int channel = 1)
    {
        int rand = Random.Range(0, clips.Length);
        PlaySoundWithDelay(clips[rand],delaySec,channel);
    }

    public static void PlaySoundWithDelay(AudioClip clip, float delaySec, int channel = 1)
    {        
        var audio = AudioChan(channel);
        audio.clip = clip;
        audio.PlayDelayed(delaySec);
    }

    public static void QueueSounds(AudioClip[] clips, float delayBetween = 0.0f, int channel = 1)
    {
        ((SoundManager)Instance).Queue(clips, delayBetween);        
    }

    public void Queue(AudioClip[] clips, float delay, int channel = 1)
    {
        StartCoroutine(PlayQueue(clips, delay, channel));
    }

    public void StopAllSounds()
    {
        StopCoroutine("playQueue");
        _audio.Stop();
        _audio2.Stop();
    }
       
    public void Volume(float percent)
    {
        _audio.volume = percent;
        _audio2.volume = percent;
        //GameState.SettingsData.SoundVolume = percent;
        VolumeLevel = percent;
    }

    private IEnumerator PlayQueue(AudioClip[] clips, float delayBetween, int channel)
    {
        foreach(var clip in clips)
        {
            var audio = AudioChan(channel);
            audio.clip = clip;
            audio.Play();
            yield return new WaitForSeconds(audio.clip.length);
            yield return new WaitForSeconds(delayBetween);
        }        
    }    

    private static AudioSource AudioChan(int channel)
    {
        if (_audio == null && _audio2 == null)
            throw new System.Exception("Missing Audio Channels from Game Scene");
        switch (channel)
        {
            default:
            case 1:
                return _audio;                
            case 2:
                return _audio2;
        }        
    }    
}