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
    
    public bool IsDying { get; set; }

    /// <summary>
    /// All Troops targeting this gameObject
    /// </summary>
    public HashSet<Troop> TargetingMe { get; set; }

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

    //private List<MeshExploder> _explodables = new List<MeshExploder>();
    
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
        /*var renders = gameObject.GetComponentsInChildren<Renderer>();
        foreach(var render in renders)
        {
            var T = render.GetType();
            if (T != typeof(MeshRenderer) && T != typeof(SkinnedMeshRenderer))
                continue;

            if (render.gameObject.GetComponent<MeshExploder>() == null)
            {
                var exploder = render.gameObject.AddComponent<MeshExploder>();
                //Set Defaults on Exploder     
                exploder.useGravity = true;

                _explodables.Add(exploder);
            }
            else
            {
                _explodables.Add(render.gameObject.GetComponent<MeshExploder>());                
            }            
        }*/
        
        TargetingMe = new HashSet<Troop>();
        IsDying = false;

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
    
    protected void Explode()
    {
        /*if(_explodables.Count > 0)
        {
            foreach(var boom in _explodables)
            {
                boom.Explode();
            }
        }*/
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

    public virtual void SetHit(int amount)
    {
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

    public virtual void Die()
    {
        IsDying = true;

        float timer = CanExplode ? 0 : DestroyTimer;
        
        //Stop Attacks
        foreach (var targets in TargetingMe)
        {
            targets.StopAttack();
        }

        Destroy(gameObject, timer);               
    }        

}
