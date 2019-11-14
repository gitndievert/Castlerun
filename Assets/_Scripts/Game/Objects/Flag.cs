using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour, IPunObservable, IAttachable
{
    public int PlayerNumber { get; set; }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
