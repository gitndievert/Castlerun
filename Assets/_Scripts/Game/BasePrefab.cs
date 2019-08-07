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
using System;
using System.Collections.Generic;

public abstract class BasePrefab : MonoBehaviour
{
    /// <summary>
    /// Health Tracker for all Base Prefabs
    /// </summary>
    public int Health;
    public bool CanExplode = true;
    public AudioClip DestroySound;
    public AudioClip[] HitSounds;
    public TextMeshPro HealthText;

    /// <summary>
    /// This is the Icon Representing the Base Prefab
    /// </summary>
    public Sprite Icon;
        
    protected int MaxHealth;
    protected float DestroyTimer = 2f;
    private List<MeshExploder> _explodables = new List<MeshExploder>();
    
    protected PlayerUI PlayerUI
    {
        get
        {
            return UIManager.Instance.PlayerUIPanel;
        }
    }

    protected TargetUI TargetUI
    {
        get
        {
            return UIManager.Instance.TargetUI;
        }
    }
    
    protected virtual void Awake()
    {
        var renders = gameObject.GetComponentsInChildren<Renderer>();
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
        }
    }   

    protected void TagPrefab(string tag)
    {
        transform.tag = tag;
    }

    //For testing
    /*#if UNITY_EDITOR
    void OnMouseDown()
    {
        Explode();
    }
    #endif*/
        
    protected void OnMouseOver()
    {
        //COME BACK
        //THIS WILL SHOW THE TARGETS WHEN SELECTED
        if (transform.tag == "Player") return;
        TargetPanel(true);
        UpdateHealthText(Health, MaxHealth);
    }

    protected void OnMouseExit()
    {
        ClearTarget();
    }

    protected void ClearTarget()
    {
        TargetUI.Target.text = "";
        TargetPanel(false);
    }

    protected void TargetPanel(bool show)
    {
        UIManager.Instance.TargetPanel.gameObject.SetActive(show);
    }

    protected void Explode()
    {
        if(_explodables.Count > 0)
        {
            foreach(var boom in _explodables)
            {
                boom.Explode();
            }
        }
    }

    protected void UpdateHealthText(int min, int max)
    {
        TargetUI.Target.text = $"{min}/{max}";
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
            UpdateHealthText(0, MaxHealth);
            if (DestroySound != null)
                SoundManager.PlaySound(DestroySound);
            if (CanExplode) Explode();
            Die();
        }
    }       

    public override string ToString()
    {
        return transform.name;
    }

    public virtual void Die()
    {
        float timer = CanExplode ? 0 : DestroyTimer;
        Destroy(gameObject, timer);
        ClearTarget();
    }
}
