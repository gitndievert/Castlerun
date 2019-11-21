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

using Photon.Pun;
using UnityEngine;

public class Castle : MonoBehaviour, IPunObservable
{
    public int PlayerNumber;

    public int DoorBustHealth = 10;
    public CastleType CastleType = CastleType.Default;    
    
    private Vector3 _spawnPos;
    private Quaternion _spawnRotation;
    

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(PlayerNumber);
        }
        else
        {
            var n = (int)stream.ReceiveNext();
            PlayerNumber = n;
        }
    }  

}
