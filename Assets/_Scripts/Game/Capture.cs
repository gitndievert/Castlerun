﻿// ********************************************************************
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
using System.Collections.Generic;
using UnityEngine;

public class Capture : MonoBehaviour
{

    public Player AttachedPlayer;


    private void OnCollisionEnter(Collision col)
    {
        var player = col.gameObject.GetComponent<Player>();

        if(player && player != AttachedPlayer)
        {
            if(player.photonView != null)
            {
                Global.Message($"{player.PlayerName} is the WINNER!", Color.red);
            }
            
        }        
    }



}
