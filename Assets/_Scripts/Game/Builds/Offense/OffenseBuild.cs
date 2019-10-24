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
    public Transform[] FirePositions;
    [Header("Projectile Used")]
    public Projectile Projectile;      

    [Header("Attack")]
    public float AttackRadius = 15f;
    public float AttackDelaySec = 3f;

    private BuildArea _buildArea;
    private float _lastAttacked;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (_buildArea == null)
        {
            _buildArea = GetComponentInChildren<BuildArea>();
        }

        if (!Global.DeveloperMode)
            gameObject.SetActive(photonView.IsMine);

        if (FirePositions.Length == 0)
            throw new System.Exception("Offensive Towers Need Spawn Positions for Projectiles");        
    }

    protected override void Update()
    {
        base.Update();

        if (isFinished)
        {

            if (Time.time > _lastAttacked)
            {
                ResetAttackTimer();

                foreach (var pos in FirePositions)
                {
                    var winnerDist = new Dictionary<ISelectable, float>();

                    //need to add in layer masks here
                    int enemylayer = 1 << Global.ENEMY_LAYER;

                    Collider[] enemycollisions = Physics.OverlapSphere(transform.position, AttackRadius, enemylayer, QueryTriggerInteraction.Ignore);

                    if (enemycollisions.Length > 0)
                    {
                        foreach (var enemy in enemycollisions)
                        {
                            Debug.Log($"Oh no an enemy hit {enemy.GetComponent<IBase>().DisplayName}");
                            float dist = Vector3.Distance(enemy.transform.position, pos.position);
                            if (dist > AttackRadius) continue;
                            var player = enemy.GetComponent<ISelectable>();
                            if (!player.IsDead)
                            {
                                winnerDist.Add(player, dist);
                            }
                        }
                        if (winnerDist.Count > 0)
                        {
                            //One player is the winner!
                            ISelectable keyWinner = winnerDist.OrderBy(k => k.Value).FirstOrDefault().Key;
                            Debug.Log("Tower is shooting at " + keyWinner.DisplayName);
                            pos.LookAt(keyWinner.GameObject.transform.position);
                            var project = PhotonNetwork.Instantiate(Projectile.gameObject.name, pos.position, Quaternion.identity);
                            project.GetComponent<Projectile>().Seek(keyWinner);                            
                        }
                    }
                }
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

    private void ResetAttackTimer()
    {
        _lastAttacked = Time.time + AttackDelaySec;
    }

    public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        base.OnPhotonSerializeView(stream, info);
        if (stream.IsWriting)
        {
            // We own this player: send the others our data            
            stream.SendNext(p_ConfirmPlacement);
            stream.SendNext(p_Finished);
        }
        else
        {
            // Network player, receive data
            var confirm = (bool)stream.ReceiveNext();
            var place = (bool)stream.ReceiveNext();

            if (confirm)
            {
                gameObject.SetActive(true);
                ConfirmPlacement();
                p_ConfirmPlacement = false;
            }

            if (place)
            {
                if (_buildArea != null) _buildArea.ShowPlane(false);
                EnableFinalModel();
                TagPrefab(Global.ENEMY_TAG);
                p_Finished = false;
            }
        }
    }
}
