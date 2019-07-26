using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[RequireComponent(typeof(Collider))]
public abstract class Build : BasePrefab, IBuild
{
    [Tooltip("Turn on the Builder")]
    [Header("Turn On Builder")]
    public bool EnableTroopBuilder = false;    

    public int PlacementCost { get; set; }

    protected bool _isPlaced = false;
    protected Player Player = null;

    private Vector3 _offset;
    

    //public float GridSnap = 0.5f;  
    protected abstract float BuildTime { get; }
    protected abstract ResourceType ResourceType { get; }
        
    protected virtual void Start()
    {                    
        if (Health == 0) Health = 20;        
        MaxHealth = Health;
    }

    public virtual bool ConfirmPlacement()
    {
        _isPlaced = true;
        TagPrefab("Build");
        return _isPlaced;
    }

    public abstract bool SetResourceType(ResourceType type);

    public void SetPlayer(Player player)
    {
        Player = player;
    }

    protected virtual void OnCollisionEnter(Collision col)
    {
        var colObj = col.gameObject;
        switch (colObj.tag)
        {
            default:
                return;
            case "Build":
                Debug.Log("Hitting the Target");                
                break;
            case "Projectile":
            case "Smasher":
                //Random change on damage??
                int damage = col.gameObject.GetComponent<IDamager>().GetDamage();
                SetHit(damage);
                break;
            case "Player":
                if (!_isPlaced) return;
                break;
        }        
    }

  

    //Old Hitblocker code
    /*protected void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Build")
        {
            Debug.Log("We got a hit");
            HitBlocker = true;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        Debug.Log("We left");
        HitBlocker = false;
    }*/

    /*protected void OnTriggerEnter(Collider col)
    {        
        if (col.gameObject.tag != "Build") return;
        Debug.Log("This Hits");
        if (!_isPlaced)
        {
            Debug.Log("This Hits");
            transform.position = transform.position - col.transform.position;            
        }
    }*/

}
