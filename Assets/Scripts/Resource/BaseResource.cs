using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseResource : BasePrefab
{
    public ResourceType ResourceType;
    public int Durability = 100;    

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

}
