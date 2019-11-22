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
    public bool EnemyHasFlag = false;
    public bool PlayerHasFlag = false;
    public int OwnerPlayerNumber = -1;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag != "Player") return;            
        var player = col.GetComponent<Player>();
        if(!player.HasFlag)
        {
            PlayerHasFlag = player.ActorNumber == OwnerPlayerNumber;
            EnemyHasFlag = !PlayerHasFlag;
            player.HasFlag = true;
            transform.position = player.BackMountPoint.position;
            transform.SetParent(player.BackMountPoint);
            player.PlayerFlag = this;
        }        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(OwnerPlayerNumber);
            stream.SendNext(EnemyHasFlag);
            stream.SendNext(PlayerHasFlag);
        }
        else
        {
            var num = (int)stream.ReceiveNext();
            OwnerPlayerNumber = num;
            var eFlag = (bool)stream.ReceiveNext();
            EnemyHasFlag = eFlag;
            var pFlag = (bool)stream.ReceiveNext();
            PlayerHasFlag = pFlag;
        }

    }
        
    public void Reset()
    {
        if (Global.DeveloperMode)
        {
            RPC_Reset();
        }
        else
        {
            photonView.RPC("RPC_Reset", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_Reset()
    {
        transform.parent = null;
        EnemyHasFlag = false;
        PlayerHasFlag = false;
    }

}
