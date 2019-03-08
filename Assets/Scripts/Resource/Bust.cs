using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bust : BasePrefab
{
    public GameObject WoodItem;
    public GameObject RockItem;
    public GameObject MetalItem;
    public GameObject GemsItem;    
    
    private int _amount;
    private ResourceType _rt;
    private GameObject _bustObj;

    protected override void Start()
    {
        base.Start();
    }

    public void SetValues(ResourceType type, int amount)
    {
        _rt = type;
        if (amount < 0) _amount = 0;
        _amount = amount;        
        /*switch(_rt)
        {
            case ResourceType.Wood:
                

        }*/
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag != "Player") return;

    
    }
}

