using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Build : BasePrefab, IBuild
{
    public int PlacementCost;    
    private bool _isPlaced = false;

    //public float GridSnap = 0.5f;  
    protected abstract float BuildTime { get; }
    protected abstract ResourceType ResourceType { get; }

    protected virtual void Start()
    {        
        if(Health == 0) Health = 20;
        if(BuildTime > 0)
        {
            //Come back, I need to have the timers run 
            //StartCoroutine(RunBuild());
        }
    }

    public virtual void ConfirmPlacement()
    {
        _isPlaced = true;
        TagPrefab("Build");
        SoundManager.PlaySound(SoundList.Instance.BuildSound);
    }

    public abstract bool SetResourceType(ResourceType type);

    protected IEnumerator RunBuild()
    {
        Debug.Log("Start Build");
        yield return new WaitForSeconds(BuildTime);
        Debug.Log("Finish Build");
    }
    
}
