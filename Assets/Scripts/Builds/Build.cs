using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Build : BasePrefab, IBuild
{
    public int PlacementCost;    

    private bool _isPlaced = false;

    //public float GridSnap = 0.5f;  
    protected abstract float BuildTime { get; }
    protected abstract ResourceType ResouceType { get; }
               
    public virtual void ConfirmPlacement()
    {
        _isPlaced = true;
        TagPrefab("Build");
        SoundManager.PlaySound(SoundList.Instance.BuildSound);
    }

    public abstract bool SetResourceType(ResourceType type);    
}
