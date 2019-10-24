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
using Photon.Pun;
using cakeslice;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class BasePrefab : MonoBehaviourPunCallbacks, IBase, IPunObservable
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
    /// This is the Icon Representing the Base Prefab
    /// </summary>
    public Sprite Icon;
    #endregion
    
    public Costs Costs;

    public string GetTag
    {
        get { return tag; }
    }
        
    public abstract string DisplayName { get; }

    protected int MaxHealth;
    protected float DestroyTimer = 1.5f;
    protected Player Player = null;
    protected GameManager GameManager;

    //Colors
    protected static readonly Color SelectedColor = Color.green;
    protected static readonly Color DamageColor = Color.red;
    protected static readonly Color PassiveColor = Color.yellow;
    
    protected Rigidbody RigidBody;
    protected Collider Collider;
    protected List<Outline> Outlines = new List<Outline>();


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
        IsDead = false;

        if (GetComponent<Rigidbody>() == null)
            gameObject.AddComponent<Rigidbody>();

        RigidBody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();        

        //Set the GameManager
        GameManager = GameManager.LocalGameManagerInstance;

        //Set the outlines
        SetOutline();
    }   

    protected void TagPrefab(string tag)
    {
        transform.tag = tag;
        if (tag == Global.ENEMY_TAG)
            gameObject.layer = Global.ENEMY_LAYER;
    }

    protected virtual void Start()
    {
        MaxHealth = Health;        

        if (!Global.DeveloperMode)
        {
            if (photonView != null && !photonView.IsMine)
            {
                TagPrefab(Global.ENEMY_TAG);
            }
        }

        Highlight(false);
    }

    /// <summary>
    /// Player this belongs too
    /// </summary>
    /// <param name="player"></param>
    public void SetPlayer(Player player)
    {
        Player = player;
        transform.parent = player.PlayerWorldItems.transform;
    }

    [PunRPC]
    protected virtual void RPC_TakeHit(int amount, bool takehit)
    {
        Health -= amount;        
    }

    protected void SetOutline()
    {
        var renders = gameObject.GetComponentsInChildren<Renderer>();
        foreach (var render in renders)
        {
            var T = render.GetType();
            
            //Exclusions
            if (T != typeof(MeshRenderer) && T != typeof(SkinnedMeshRenderer))
                continue;
            if (render.transform.tag == Global.BUILDAREA_TAG)
                continue;
            if (render.gameObject.layer == Global.MINIMAP_ICON_LAYER)
                continue;
            if (render.transform.GetComponent<TextMeshPro>() != null)
                continue;

            if (render.gameObject.GetComponent<Outline>() == null)
            {
                render.gameObject.AddComponent<Outline>();
            }
            //Set defaults on outline
            var outline = render.gameObject.GetComponent<Outline>();
            outline.enabled = false;
            Outlines.Add(outline);
        }
    }

    //Damage and Death    
    public virtual void SetHit(int min, int max)
    {
        if (Health <= 0 || IsDead) return;
        int amount = CalcDamage(min, max, out bool crit);

        if (Health - amount > 0)
        {
            Health -= amount;
            if (HitSounds.Length > 0)
                SoundManager.PlaySound(HitSounds);

            if (!Global.DeveloperMode)
                photonView.RPC("RPC_TakeHit", RpcTarget.Others, amount);

            if (photonView.IsMine || Global.DeveloperMode)
                UIManager.Instance.FloatCombatText(TextType.Damage, amount, crit, transform);
        }
        else
        {            
            if (DestroySound != null)
                SoundManager.PlaySound(DestroySound);
            if (CanExplode) Explode();
            Die();
        }
    }

    public Vector3 DistanceToEdge(Vector3 point)
    {
        return Collider.bounds.ClosestPoint(point);
    }

    public virtual void Die()
    {
        IsDead = true;
        Destroy(gameObject, DestroyTimer);
    }

    /// <summary>
    /// Enable or disable outline around the gameobject
    /// </summary>
    /// <param name="action"></param>
    public void Highlight(bool action)
    {
        Highlight(action, 0);
    }

    /// <summary>
    /// Enable or disable outline around the gameobject
    /// </summary>
    /// <param name="action"></param>
    /// <param name="color"></param>
    public void Highlight(bool action, byte color)
    {
        if (Outlines.Count <= 0) return;
        foreach (var outline in Outlines)
        {
            outline.color = color;
            outline.enabled = action;
        }        
    }
        
    protected int CalcDamage(int min, int max, out bool crit)
    {
        var dmg = Random.Range(min, max);
        crit = false;
        if (Random.Range(0, 20) > 17)
        {
            dmg *= 2;
            crit = true;
        }
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

    /// <summary>
    /// Pushes data back and forth in stream
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="info"></param>
    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        /*if(stream.IsWriting)
        {            
            stream.SendNext(Health);
        }
        else
        {
            Health = (int)stream.ReceiveNext();
        }*/
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;                
    }

    


}
