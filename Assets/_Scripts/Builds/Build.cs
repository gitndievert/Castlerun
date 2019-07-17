using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[RequireComponent(typeof(Collider))]
public abstract class Build : BasePrefab, IBuild
{
    public int PlacementCost;   

    protected bool _isPlaced = false;
    
    private Vector3 _offset;

    //public float GridSnap = 0.5f;  
    protected abstract float BuildTime { get; }
    protected abstract ResourceType ResourceType { get; }

    protected Rigidbody RigidBody;

    protected virtual void Start()
    {                
        RigidBody = GetComponent<Rigidbody>();
        //Replace HealthText with UI elements
        HealthText.gameObject.SetActive(false);
        if (Health == 0) Health = 20;
        SetHealthText(Health);
        MaxHealth = Health;
    }
    
    protected void OnMouseOver()
    {
        HealthText.gameObject.SetActive(true);
    }

    protected void OnMouseExit()
    {
        HealthText.gameObject.SetActive(false);
    }

    public virtual void ConfirmPlacement()
    {
        _isPlaced = true;
        TagPrefab("Build");        
        if (BuildTime > 0)
        {   
            StartCoroutine(RunBuild());
        }
    }

    public abstract bool SetResourceType(ResourceType type);

    protected IEnumerator RunBuild()
    {
        Debug.Log("Start Build");
        Global.BuildMode = false;        
        yield return new WaitForSeconds(BuildTime);
        SoundManager.PlaySound(SoundList.Instance.BuildSound);
        Debug.Log("Finish Build");        
        Global.BuildMode = true;
    }

    protected void OnCollisionEnter(Collision col)
    {        
        switch(col.gameObject.tag)
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
