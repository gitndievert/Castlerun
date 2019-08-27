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

using UnityEngine;



//NATE NOTE: I MIGHT NEED TO RETHINK THESE
//Moving a lot of projectile pieces to it's own class for ALL 
public class OffensiveBuild : Build
{     
    
    public GameObject FireEffect;
    public float ConstructionTime = 2f;
       
    protected override float BuildTime => ConstructionTime;
    protected override ResourceType ResourceType => ResourceType.Metal;

    protected override void Start()
    {
        base.Start();        
    }
    
    public override bool SetResourceType(ResourceType type)
    {
        return type == ResourceType;
    }    
}
