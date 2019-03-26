using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveBuild : Build
{
    public AudioClip[] FireSounds;    
    public GameObject Projectile;
    public GameObject FireEffect;

    public float FireTimer = 0.3f;
    public float FireRate = 0.5f;
    public float FirePower = 5.5f;
    public Transform FirePosition;
    [HideInInspector]
    public bool IsFiring = false;

    [SerializeField]
    private float _ConstructionTime;
    private float _trackFireTimer;

    protected override float BuildTime => _ConstructionTime;
    protected override ResourceType ResouceType => ResourceType.Metal;

    protected override void Start()
    {
        base.Start();
        if (FirePosition == null)
            throw new System.Exception("Prefab needs a fire spot");
    }
        
    void Update()
    {
        if (_trackFireTimer != FireRate && IsFiring)
        {
            CancelInvoke("FireProjectile");
            Fire();
        }

    }

    public override void ConfirmPlacement()
    {
        base.ConfirmPlacement();
        if(Projectile != null)
        {
            Fire();
        }
            
    }

    public void Fire()
    {
        _trackFireTimer = FireRate;
        InvokeRepeating("FireProjectile", FireTimer, FireRate * 2);
    }

    protected void FireProjectile()
    {
        IsFiring = true;
        var proj = Instantiate(Projectile, FirePosition.position, Quaternion.identity);
        proj.name = "Incoming";
        proj.GetComponent<Rigidbody>().velocity = FirePosition.TransformDirection(Vector3.up * FirePower);        
        SoundManager.PlaySoundOnGameObject(gameObject, FireSounds);
        if (FireEffect != null)
        {
            Instantiate(FireEffect, FirePosition.position, Quaternion.identity);            
        }
        //proj.transform.parent = transform.GetChild(0);     
    }

    protected static Vector3 RandomFire()
    {
        int random = Random.Range(0, 100);
        if (random >= 0 && random < 25)
            return Vector3.left;
        else if (random > 25 && random <= 50)
            return Vector3.right;
        else if (random > 50 && random <= 75)
            return Vector3.forward;
        else
            return Vector3.down;
    }

    public override bool SetResourceType(ResourceType type)
    {
        return type == ResouceType;
    }

}
