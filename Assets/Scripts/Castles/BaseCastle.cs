using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCastle : BasePrefab
{
    [Range(1,3)]
    public int Level = 1;
    public float DoorBustHealth = 100f;

    public StatModifier StatModifier;


    //Castles will have many passive properties
    //Need something to map materials
    //Need something for icons
    //Need something for ambient noise
    //Need Health
    
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }


}
