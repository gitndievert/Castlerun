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

using Mirror;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NetworkIdentity))]
[RequireComponent(typeof(NavMeshObstacle))]
public abstract class Build : BasePrefab, IBuild, ISelectable
{
    public BuildingLabelTypes BuildingLabelType = BuildingLabelTypes.None;

    public int PlacementCost { get; set; }
    public bool IsBasic { get; set; }

    /// <summary>
    /// This shows it being selectable or not to player
    /// </summary>
    public bool EnableFromBuilder = false;

    //public float GridSnap = 0.5f;     

    public bool IsSelected { get; set; }
    public GameObject GameObject => gameObject;
    public string DisplayName { get; set; }
        
    /// <summary>
    /// Time it takes to build this building
    /// </summary>
    public float ConstructionTime = 0f;

    protected bool isPlaced = false;
    protected Player Player = null;
    protected bool isFinished = false;

    private Vector3 _offset;
    

    protected virtual void Start()
    {                    
        if (Health == 0) Health = 20;        
        MaxHealth = Health;
        IsBasic = false;
        DisplayName = BuildingLabelType.ToString();
        if (Costs.CostFactors.Length == 0)
            throw new System.Exception("Please add a cost");
        var net = GetComponent<NetworkIdentity>();
        net.serverOnly = true;
    }

    protected virtual void Update()
    {
        if(!EnableFromBuilder)
        {
            //Set Icon Colors;
        }
    }

    public float GetConstructionTime()
    {
        return ConstructionTime;
    }

    public virtual bool ConfirmPlacement()
    {
        isPlaced = true;
        TagPrefab("Build");
        return isPlaced;
    }
    
    public void SetPlayer(Player player)
    {
        Player = player;
    }

    /*public void StartBuildEffect()
    {
        if (BuildEffect != null && !BuildEffect.activeSelf && !_isFinished)
            BuildEffect.SetActive(true);
    }*/

    public void FinishBuild()
    {
        isFinished = true;
        SoundManager.PlaySound(SoundList.Instance.BuildSound);
    }

    public virtual void OnMouseDown()
    {
        if (!isFinished) return;
        switch (GetTag)
        {
            case Global.ARMY_TAG:
                SelectionUI.UpdateSingleTarget(this);
                break;
            case Global.BUILD_TAG:
                SelectionUI.UpdateSingleTarget(this);
                break;
            case Global.ENEMY_TAG:
                SelectionUI.UpdateEnemyTarget(this);
                break;
        }
    }
    
    protected virtual void OnCollisionEnter(Collision col)
    {
        var colObj = col.gameObject;
        if (!isFinished) return;
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
        if (!IsSelected && isFinished)
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
            BuildManager.Instance.ShowBuildPanel();
        }
    }
}
