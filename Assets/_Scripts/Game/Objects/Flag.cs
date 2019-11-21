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

using Photon.Pun;
using UnityEngine;

public class Flag : MonoBehaviourPun, IPunObservable
{
    public int PlayerNumber = 0;

    /*private void OnTriggerEnter(Collider col)
    {
        if (col.tag != "Player") return;
        var player = col.GetComponent<Player>();
        if(PlayerNumber == 0 && !player.HasFlag)
        {
            PlayerNumber = player.ActorNumber;
            player.PlayerFlag = this;
            player.PickUpFlag(this);
        }        
    }*/

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(PlayerNumber);
        }
        else
        {
            var num = (int)stream.ReceiveNext();
            PlayerNumber = num;
        }

    }

    public void Reset()
    {
        PlayerNumber = 0;
        transform.parent = null;
    }
}
