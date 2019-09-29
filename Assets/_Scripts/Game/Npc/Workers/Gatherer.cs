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
using UnityEngine;

public class Gatherer : Troop
{
    public const int WoodQuantity = 40;
    public const int RockQuantity = 20;
    public const int MetalQuantity = 20;

    public ResourceType HarvestingSelection;
    public float HarvestTime;

    [Space(5)]
    public bool IsHarvesting = false;

    public TroopFactory AssociatedFactory { get; private set; }

    public override string DisplayName => "Gatherer";

    /// <summary>
    /// Random set of trips
    /// </summary>
    [Space(5)]
    [Header("# of trips (random)")]
    public int HarvestTrips = 1;

    protected int DestPoint;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SelectionTargetStatus(true);        
        anim.Play("Walk");
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!IsHarvesting)
        {
            if (!nav.pathPending && nav.remainingDistance < 0.5f)
            {
                GoToNextPoint();
            }
        }
    }

    public void SetFactory(TroopFactory factory)
    {
        AssociatedFactory = factory;
    }
    
    private IEnumerator Wait()
    {

        //For Harvesting....
        //TroopPlayer.Inventory.Set(ResourceType.Wood, 15);

        yield return new WaitForSeconds(3f);
    }

    public override void Fire()
    {
        throw new System.NotImplementedException();
    }

    protected void GoToNextPoint()
    {
        if (points.Count == 0) return;
        nav.destination = points[DestPoint].position;
        //DestPoint = (DestPoint + 1) % points.Count;
        int newpoint = Random.Range(0, points.Count);
        DestPoint = newpoint == DestPoint ? (DestPoint + 1) % points.Count : newpoint;        
    }
}
