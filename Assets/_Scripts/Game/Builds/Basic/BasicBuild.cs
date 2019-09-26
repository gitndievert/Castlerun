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

using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BasicBuild : Build
{
    //public List<SnapPoints> SnapPoints = new List<SnapPoints>();

    private ResourceType _pickType;    

    protected override void Awake()
    {
        base.Awake();
        //SnapPoints = GetComponents<SnapPoints>().ToList();        
    }

    protected override void Start()
    {
        base.Start();        
        IsBasic = true;        
    }
    
    public override bool ConfirmPlacement()
    {        
        return base.ConfirmPlacement();
    }   

    /*public Vector3[] SnapPointPos
    {
        get { return SnapPoints.Select(a => a.transform.position).ToArray(); }
    }*/

    //Old code
    /*protected override void OnCollisionEnter(Collision col)
    {
        var colObj = col.gameObject;
        switch (colObj.tag)
        {
            default:
                return;            
            case "Projectile":
            case "Smasher":
                //Random change on damage??
                int damage = col.gameObject.GetComponent<IDamager>().GetDamage();
                SetHit(damage);
                break;
            case "Player":
                if (!_isPlaced) return;
                break;
        }
    }*/

    /*public Transform GetCloseSnapByBuild(BasicBuild collidingbuild)
    {
        foreach (SnapPoints point in SnapPoints)
        {
            if (point.Snapped) continue;            
            foreach(var colpoint in collidingbuild.SnapPoints)
            {
                float dist = Vector3.Distance(point.Position, colpoint.Position);
                if (dist < 1f) return colpoint.PointTransform;              
            }          
        }       

        return null;
    }*/
}
