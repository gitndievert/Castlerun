using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour, IPunObservable
{
    public int PlayerNumber = 0;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag != "Player") return;
        var player = col.GetComponent<Player>();
        if(PlayerNumber == 0 && !player.HasFlag)
        {
            PlayerNumber = player.ActorNumber;
            player.PlayerFlag = this;
            player.PickUpFlag(this);
        }
        /*else if (PlayerNumber != player.ActorNumber && !player.PickedUpFlag)
        {
            player.PickUpFlag(this);
        }*/
    }

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
