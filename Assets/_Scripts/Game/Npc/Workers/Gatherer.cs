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
using UnityEngine.AI;

public class Gatherer : Troop
{
    protected float Speed;       

    protected override void Awake()
    {
        base.Awake();        
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anim.Play("Walk");
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        if (!nav.pathPending && nav.remainingDistance < 0.5f)
        {            
            GoToNextPoint();
        }
    }
    
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
    }
    
    /// <summary>
    /// Attack the rocks and shrubbery!
    /// </summary>
    /// <param name="target"></param>
    public override void Target(ISelectable target)
    {
        
    }
}
