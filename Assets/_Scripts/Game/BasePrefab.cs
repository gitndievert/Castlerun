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

using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody))]
public abstract class BasePrefab : MonoBehaviourPunCallbacks, IBase
{
    #region Base Stats
    /// <summary>
    /// Health Tracker for all Base Prefabs
    /// </summary>        
    public int Health;
    
    public bool CanExplode = true;
    public AudioClip DestroySound;
    public AudioClip[] HitSounds;
    public TextMeshPro HealthText;
    
    /// <summary>
    /// Check to see if this object is DYING
    /// </summary>
    public bool IsDead { get; set; }

    /// <summary>
    /// All Troops targeting this gameObject
    /// </summary>
    public HashSet<Troop> TargetingMe { get; set; }

    /// <summary>
    /// Being Targeted by a Player
    /// </summary>
    public Player TargetByPlayer { get; set; }

    /// <summary>
    /// This is the Icon Representing the Base Prefab
    /// </summary>
    public Sprite Icon;
    #endregion
    
    public Costs Costs;

    public string GetTag
    {
        get { return tag; }
    }

    protected int MaxHealth;
    protected float DestroyTimer = 2f;
    protected Player Player = null;
    protected GameManager GameManager;

    //Colors
    protected static readonly Color SelectedColor = Color.green;
    protected static readonly Color DamageColor = Color.red;
    protected static readonly Color PassiveColor = Color.yellow;
    
    protected Rigidbody RigidBody;

    //Targeting System
    protected ISelectable EnemyTarget { get; set; }
    protected Transform EnemyTargetTransform { get; set; }


    protected PlayerUI PlayerUI
    {
        get
        {
            return UIManager.Instance.PlayerUIPanel;
        }
    }

    protected SingleTargetBox SingleTargetBox
    {
        get
        {
            return UIManager.Instance.SingleTargetBox;
        }
    }

    protected Selection SelectionUI
    {
        get
        {
            return UIManager.Instance.SelectableComponent;
        }
    }
    
    protected virtual void Awake()
    {       
        TargetingMe = new HashSet<Troop>();
        IsDead = false;

        if (GetComponent<Rigidbody>() == null)
            gameObject.AddComponent<Rigidbody>();

        RigidBody = GetComponent<Rigidbody>();

        //Set the GameManager
        GameManager = GameManager.LocalGameManagerInstance;
    }   

    protected void TagPrefab(string tag)
    {
        transform.tag = tag;
    }

    protected virtual void Start()
    {
        if (!Global.DeveloperMode)
        {
            if (photonView != null && !photonView.IsMine)
            {
                TagPrefab(Global.ENEMY_TAG);
            }
        }
    }

    protected void SetStartHealth(int value)
    {
        Health = value;
        MaxHealth = value;        
    }

    public void SetPlayer(Player player)
    {
        Player = player;
        transform.parent = player.PlayerWorldItems.transform;
    }

    //Damage and Death
    public virtual void SetHit(int min, int max, bool hascritical = false)
    {
        if (Health <= 0 || IsDead) return;
        int amount = CalcDamage(min, max, hascritical);

        if (Health - amount > 0)
        {
            Health -= amount;
            if (HitSounds.Length > 0)
                SoundManager.PlaySound(HitSounds);
        }
        else
        {            
            if (DestroySound != null)
                SoundManager.PlaySound(DestroySound);
            if (CanExplode) Explode();
            Die();
        }
    }

    public virtual void Die()
    {
        IsDead = true;
        
        //Stop Attacks
        foreach (var targets in TargetingMe)
        {
            targets.StopAttack();
        }

        Destroy(gameObject, DestroyTimer);
    }


    protected int CalcDamage(int min, int max, bool hascritical = false)
    {
        var dmg = Random.Range(min, max);
        if (Random.Range(0, 20) > 17) dmg *= 2;
        return dmg;
    }

    protected void Explode()
    {

    }

    public void AddExplosionForce(float power = 10.0f)
    {
        if (RigidBody == null) return;
        RigidBody.AddExplosionForce(power, transform.position + Vector3.forward * 2, 5.0f, 3.0F);
    }
   
    public virtual Sprite GetIcon()
    {
        return Icon ?? UIManager.Instance.DefaultIcon;
    }

    public int GetCurrentHealth()
    {
        return Health;
    }

    public int GetMaxHealth()
    {
        return MaxHealth;
    }

    public Costs GetCosts()
    {
        return Costs ?? null;
    }

    public override string ToString()
    {
        return transform.name;
    }

    //Targeting System
    /// <summary>
    /// This Troops target for attack
    /// </summary>
    /// <param name="target"></param>
    public virtual void Target(ISelectable target)
    {
        EnemyTarget = target;
        EnemyTargetTransform = target.GameObject.transform;
    }

    public void SetTargetedByPlayer(Player player)
    {
        TargetByPlayer = player;
        EnemyTarget = player;
        EnemyTargetTransform = player.transform;
    }

    public void ClearEnemyTargets()
    {
        EnemyTarget = null;
        EnemyTargetTransform = null;
        TargetByPlayer = null;
    }

    /// <summary>
    /// Pushes data back and forth in stream
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="info"></param>
    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(Health);
        }
        else
        {
            Health = (int)stream.ReceiveNext();
        }
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;
    }


}
