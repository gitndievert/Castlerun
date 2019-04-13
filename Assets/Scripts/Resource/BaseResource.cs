using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseResource : BasePrefab, IResource
{
    public ResourceType ResourceType;
    public GameObject BustResourceObj;
    public AudioClip[] HitSounds;
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
        Health -= amount;
        if (Health <= 0) BustResource();        
    }
    
    protected override void Awake()
    {
        base.Awake();        
        TagPrefab("Resource");
    }

    protected override void Start()
    {
        base.Start();
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
