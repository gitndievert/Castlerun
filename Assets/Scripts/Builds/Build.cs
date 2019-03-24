using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Build : BasePrefab, IBuild
{
    private bool _isPlaced = false;

    //public float GridSnap = 0.5f;  
    protected abstract float BuildTime { get; }
        
    public virtual void ConfirmPlacement()
    {
        _isPlaced = true;
    }    
}
