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

public abstract class Build : BasePrefab, IBuild, ISelectable
{ 
    public int PlacementCost { get; set; }

    public bool IsBasic { get; set; }

    [Space(5)]
    public GameObject BuildEffect;

    protected bool isPlaced = false;
    protected Player Player = null;

    private Vector3 _offset;
    

    //public float GridSnap = 0.5f;  
    protected abstract float BuildTime { get; }
    protected abstract ResourceType ResourceType { get; }
    public bool IsSelected { get; set; }

    public GameObject GameObject => gameObject;

    protected virtual void Start()
    {                    
        if (Health == 0) Health = 20;        
        MaxHealth = Health;
        IsBasic = false;
    }

    public virtual bool ConfirmPlacement()
    {
        isPlaced = true;
        TagPrefab("Build");
        return isPlaced;
    }
    
    public abstract bool SetResourceType(ResourceType type);

    public void SetPlayer(Player player)
    {
        Player = player;
    }

    public void StartBuildEffect()
    {
        if (BuildEffect != null && !BuildEffect.activeSelf)
            BuildEffect.SetActive(true);
    }

    public void OnMouseOver()
    {
        SelectionUI.UpdateSingleTarget(this);
        switch (GetTag)
        {
            case Global.ARMY_TAG:
                //glow green
                break;
            case Global.BUILD_TAG:
                //glow yellow
                break;
            case Global.ENEMY_TAG:
                //glow red
                break;
        }
    }

    public void OnMouseExit()
    {
        SelectionUI.ClearSingleTarget();
    }

    protected virtual void OnCollisionEnter(Collision col)
    {
        var colObj = col.gameObject;
        switch (colObj.tag)
        {
            default:
                return;
            case "Build":
                Debug.Log("Hitting the Target");                
                break;
            case "Projectile":
            case "Smasher":
                //Random change on damage??
                int damage = col.gameObject.GetComponent<IDamager>().GetDamage();
                SetHit(damage);
                break;
            case "Player":
                if (!isPlaced) return;
                break;
        }        
    }

    /// <summary>
    /// Called Method on Target Selection
    /// </summary>
    public void Select()
    {
        if (!IsSelected)
        {
            IsSelected = true;
        }
    }

    /// <summary>
    /// Called Method on Target UnSelection
    /// </summary>
    public void UnSelect()
    {
        if (IsSelected)
        {
            IsSelected = false;
        }
    }
}
