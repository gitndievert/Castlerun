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

public abstract class BasePrefab : MonoBehaviour
{
    /// <summary>
    /// Health Tracker for all Base Prefabs
    /// </summary>
    public int Health;
    /// <summary>
    /// Progress Bar for Troops
    /// </summary>
    public ProgressBar HealthBar;

    public AudioClip DestroySound;
    public TextMeshPro HealthText;

    protected int MaxHealth;

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
      
    }
    
    protected void TagPrefab(string tag)
    {
        transform.tag = tag;
    }
    
    protected void OnMouseOver()
    {
        if(transform.tag == "Player") return;
        TargetPanel(true);
        TargetUI.Target.text = $"{Health}/{MaxHealth}";
    }

    protected void OnMouseExit()
    {
        ClearTarget();
    }

    private void ClearTarget()
    {
        TargetUI.Target.text = "";
        TargetPanel(false);
    }

    protected void TargetPanel(bool show)
    {
        UIManager.Instance.TargetPanel.gameObject.SetActive(show);
    }

    public virtual void SetHit(int amount)
    {
        if (Health - amount > 0)
        {
            Health -= amount;            
            if(HealthBar != null)
            {

            }
        }
        else
        {
            if (DestroySound != null)
                SoundManager.PlaySoundOnGameObject(gameObject, DestroySound);
            Destroy(gameObject);
            ClearTarget();
        }
    }       

    public override string ToString()
    {
        return Health.ToString();
    }
}
