// ********************************************************************
// CONFIDENTIAL - DO NOT DISTRIBUTE
// COPYRIGHT 2019-2020 Wacky Potato Games, LLC. All Rights Reserved.
// 
// If you send, receive, or use this file for any purpose other than
// internal use by Wacky Potato Games, it is without permission and an act of theft.
// Report any misuse of this file immediately to contact@wackypotato.com
// Misuse or failure to report misuse will subject you to legal action.
// 
// The intellectual and technical concepts contained herein are
// proprietary and are protected by trade secret and/or copyright law.
// Dissemination or reproduction of this material is forbidden.
// ********************************************************************

using System.Collections;
using Photon.Pun;
using UnityEngine;

public class Gatherer : Troop
{
    public const int WoodQuantity = 40;
    public const int RockQuantity = 20;
    public const int MetalQuantity = 20;

    /// <summary>
    /// 9 Second Harvest Time
    /// </summary>
    public const float HarvestTimeWood = 9f;
    /// <summary>
    /// 16 Second Harvest Time
    /// </summary>
    public const float HarvestTimeRock = 16f;
    /// <summary>
    /// 20 Second Harvest Time
    /// </summary>
    public const float HarvestTimeMetal = 20f;

    [Header("Harvesting Properties")]
    public ResourceType HarvestingSelection;
        
    [HideInInspector]
    public bool IsHarvesting = false;    

    public override string DisplayName => "Gatherer";

    /// <summary>
    /// For a Gatherer this is the distance to harvesting on a node (not really attacking)
    /// </summary>
    protected override float AttackDistance => 0.5f;
    /// <summary>
    /// If Gatherer gets attack, he needs to fight back but only if people get really close!
    /// </summary>
    protected override float AgroDistance => 1f;

    protected int DestPoint;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SelectionTargetStatus(true);        
        anim.Play("Walk");
        if (points.Count == 0) return;
        nav.SetDestination(RandomNavPoint());
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!IsHarvesting)
        {
            if (!nav.pathPending && nav.remainingDistance < AttackDistance)
            {
                StartCoroutine(HarvestWait());                
            }            
        }
    }

    private IEnumerator HarvestWait()
    {
        IsHarvesting = true;            
        nav.isStopped = true;
        nav.velocity = Vector3.zero;

        int qty;
        float hTime;

        switch (HarvestingSelection)
        {
            default:
            case ResourceType.Wood:
                qty = WoodQuantity;
                hTime = HarvestTimeWood;
                break;
            case ResourceType.Rock:
                qty = RockQuantity;
                hTime = HarvestTimeRock;
                break;
            case ResourceType.Metal:
                qty = MetalQuantity;
                hTime = HarvestTimeMetal;
                break;
        }

        anim.Play("Swing");
        yield return new WaitForSeconds(hTime);
        if (photonView.IsMine)
        {
            Player.Inventory.Set(HarvestingSelection, qty);
        }
        IsHarvesting = false;
        nav.isStopped = false;
        anim.Play("Walk");
        GoToNextPoint();
    }

    public override void Fire()
    {
        
    }

    protected void GoToNextPoint()
    {
        if (points.Count == 0) return;

        Vector3 p = RandomNavPoint();

        while (p == nav.destination)
        {
            p = RandomNavPoint();            
        }

        nav.destination = p;
    }

    private Vector3 RandomNavPoint()
    {
        if (points.Count <= 0) return Vector3.zero;
        int rPoint = Random.Range(0, points.Count);
        return points[rPoint].position;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        
    }    
}
