using UnityEngine;

public class Projectile : BasePrefab
{
    public AudioClip TravelSound;
    public AudioClip ImpactSound;
    public int Damage;   
    
    protected override void Awake()
    {
        base.Awake();        
        TagPrefab("Projectile");
    }
    
    private void OnDestroy()
    {
        
    }

    public int GetDamage()
    {
        return Damage;
    }

    private void PlayTravelSound()
    {
        if (TravelSound == null) return;
        //audioSource.loop = true;
        //audioSource.clip = TravelSound;
        //audioSource.Play();
    }

    private void PlayHitSound()
    {
        if (ImpactSound == null) return;
        SoundManager.PlaySoundOnGameObject(gameObject, ImpactSound);
    }

    private void OnCollisionEnter(Collision col)
    {
        PlayHitSound();        
        Destroy(gameObject);        
    }
}
