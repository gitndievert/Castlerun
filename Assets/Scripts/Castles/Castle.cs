using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : BasePrefab
{
    [Range(1,3)]
    public int Level = 1;
    public float DoorBustHealth = 100f;
    public CastleType CastleType = CastleType.Default;
    public AudioClip AmbientMusic;
    
    //List of NPC the castle can offer
    //public Npc[] Npcs;

    public StatModifier StatsModifier;

    private CastleManager _manager;


    //Castles will have many passive properties
    //Need something to map materials
    //Need something for icons
    //Need something for ambient noise
    //Need Health
    
    protected override void Awake()
    {
        base.Awake();
        _manager = CastleManager.Instance;
    }

    protected override void Start()
    {
        base.Start();
    }   


}
