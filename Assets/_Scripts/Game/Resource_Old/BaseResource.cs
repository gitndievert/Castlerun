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

public abstract class BaseResource : BasePrefab, IResource
{
    public ResourceType ResourceType;
    public GameObject BustResourceObj;
    public AudioClip BustSound;
    public int Value = 10;    

    public int GetHealth()
    {
        return Health;
    }

    public ResourceType GetResourceType()
    {
        return ResourceType;
    }

    public void PlayHitSounds()
    {
        if(HitSounds != null)
            SoundManager.PlaySound(HitSounds);
    }

    public override void SetHit(int amount)
    {
        PlayHitSounds();
        Health -= amount;
        if (Health <= 0) BustResource();        
    }
    
    private void Start()
    {
        TagPrefab("Resource");
    }

    protected void BustResource()
    {
        if (BustResourceObj != null)
        {
            var bust = Instantiate(BustResourceObj, new Vector3(transform.position.x,
                transform.position.y, transform.position.z), Quaternion.identity);
            //Sounds
            //Effects                   
            var bustValues = bust.GetComponent<Bust>();
            bustValues.SetValues(ResourceType, Value, BustSound);
            bustValues.Spawn();
            Destroy(gameObject);
        }
    }



}
