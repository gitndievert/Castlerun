using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetCalls : MonoBehaviourPun
{   

    [PunRPC]
    public void RPC_Damage(ISelectable sel, int min, int max, bool crits = true)
    {        
        //Damage.ApplyDamage(sel, min, max, true);        
    }

}
