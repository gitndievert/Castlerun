using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Build : BasePrefab, IBuild
{
    public int PlacementCost;    
    protected bool _isPlaced = false;

    private Vector3 _offset;

    //public float GridSnap = 0.5f;  
    protected abstract float BuildTime { get; }
    protected abstract ResourceType ResourceType { get; }

    protected virtual void Start()
    {        
        if(Health == 0) Health = 20;        
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
