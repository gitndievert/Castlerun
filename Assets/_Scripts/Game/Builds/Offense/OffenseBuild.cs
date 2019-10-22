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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OffenseBuild : Build
{
    const float MAXIMUM_BO_POWER = 2000f;

    public Transform[] FirePositions;
    [Header("Projectile Used")]
    public Projectile Projectile;
    [Header("Projectile Properties")]
    [Range(0f, MAXIMUM_BO_POWER)]
    public float FirePower = 300f;
    public float AttackRadius = 15f;

    private BuildArea _buildArea;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (_buildArea == null)
        {
            _buildArea = GetComponentInChildren<BuildArea>();
        }

        if (FirePositions.Length == 0)
            throw new System.Exception("Offensive Towers Need Spawn Positions for Projectiles");


        
    }

    protected override void Update()
    {
        base.Update();

        foreach(var pos in FirePositions)
        {
            if (!Global.DeveloperMode)
            {                
                var winnerDist = new Dictionary<Player, float>();
                foreach (var player in GameManager.PlayersByActor)
                {
                    //Pool attack targets and distances, pick closest one
                    int localNum = PhotonNetwork.LocalPlayer.ActorNumber;
                    if (player.Key == localNum) continue;
                    Player p = player.Value;                    
                    float dist = Vector3.Distance(p.transform.position, pos.position);
                    if (dist > AttackRadius) continue;                    
                    if (!p.IsDead)
                    {
                        winnerDist.Add(p, dist);
                    }                    
                }
                //Get lowest value for closet target
                Player keyWinner = winnerDist.Min(f => f.Key);

                pos.LookAt(keyWinner.transform.position);
                var project = PhotonNetwork.Instantiate(Projectile.gameObject.name, pos.position, Quaternion.identity);                
                project.GetComponent<Rigidbody>().AddForce(pos.forward * FirePower);

            }
        }
    }

    public override bool ConfirmPlacement()
    {
        if (!_buildArea.CanBuild) return false;
        gameObject.SetActive(true);
        base.ConfirmPlacement();
        _buildArea.ShowPlane(false);
        CamShake.Instance.Shake(1f, .5f);

        return true;
    }
}
