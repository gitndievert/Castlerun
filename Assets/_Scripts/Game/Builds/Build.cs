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
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public abstract class Build : BasePrefab, IBuild, ISelectable
{
    public BuildingLabelTypes BuildingLabelType = BuildingLabelTypes.None;

    public int PlacementCost { get; set; }
    public bool IsBasic { get; set; }

    //Transparent and Final Objects 
    public GameObject TransparentModel;
    public GameObject FinalModel;

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
    protected bool isFinished = false;

    private Vector3 _offset;

    protected override void Awake()
    {
        base.Awake();
        EnableLayModel();
        GetComponent<BoxCollider>().isTrigger = true;
    }

    protected virtual void Start()
    {
        RigidBody.isKinematic = true;
        if (Health == 0) Health = 20;        
        MaxHealth = Health;
        IsBasic = false;
        DisplayName = BuildingLabelType.ToString();
        if (Costs.CostFactors.Length == 0)
            throw new System.Exception("Please add a cost");
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

    /*public void StartBuildEffect()
    {
        if (BuildEffect != null && !BuildEffect.activeSelf && !_isFinished)
            BuildEffect.SetActive(true);
    }*/

    public void EnableLayModel()
    {
        TransparentModel.SetActive(true);
        FinalModel.SetActive(false);
    }

    public void EnableFinalModel()
    {
        TransparentModel.SetActive(false);
        FinalModel.SetActive(true);
    }

    public void FinishBuild()
    {
        isFinished = true;
        EnableFinalModel();
        //SoundManager.PlaySound(SoundList.Instance.BuildSound);
    }

    public virtual void OnMouseDown()
    {
        if (!isFinished) return;        
        //Ignore all UI targets
        //if (EventSystem.current.IsPointerOverGameObject()) return;

        IsSelected = true;
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
    
    /// <summary>
    /// Called Method on Target Selection
    /// </summary>
    public virtual void Select()
    {
        if (!IsSelected && isFinished)
        {
            IsSelected = true;
        }
    }

    /// <summary>
    /// Called Method on Target UnSelection
    /// </summary>
    public virtual void UnSelect()
    {
        if (IsSelected)
        {
            IsSelected = false;
            BuildManager.Instance.RefreshBuilds();
        }
    }
}
