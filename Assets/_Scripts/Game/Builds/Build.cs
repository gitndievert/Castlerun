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
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
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

    public GameObject ConstructionZone;
    
    //public float GridSnap = 0.5f;     

    public bool IsSelected { get; set; }
    public GameObject GameObject => gameObject;
    public override string DisplayName => BuildingLabelType.TypeToString();

    /// <summary>
    /// Time it takes to build this building
    /// </summary>
    public float ConstructionTime = 0f;

    #region AudioClips For Troops
    [Space(5)]
    [Header("Audio Clips for Buildings")]
    public AudioClip Construction;
    public AudioClip ConstructionComplete;    
    #endregion;

    protected bool isPlaced = false;    
    protected bool isFinished = false;
    protected bool p_Finished = false;
    protected bool p_ConfirmPlacement = false;

    private Vector3 _offset;

    protected override void Awake()
    {
        base.Awake();
        EnableLayModel();
        GetComponent<BoxCollider>().isTrigger = true;
    }

    protected override void Start()
    {
        base.Start();
        RigidBody.isKinematic = true;
        if (Health == 0) Health = 20;        
        MaxHealth = Health;
        IsBasic = false;        
        if (Costs.CostFactors.Length == 0)
            throw new System.Exception("Please add a cost");
        if (ConstructionZone != null)
            ConstructionZone.SetActive(false);
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
    
    public void EnableConstructionZone()
    {        
        if (ConstructionZone != null)
        {
            TransparentModel.SetActive(false);
            ConstructionZone.SetActive(true);            
        }
        else
        {
            EnableLayModel();
        }        
    }

    public virtual bool ConfirmPlacement()
    {
        isPlaced = true;
        p_ConfirmPlacement = true;
        TagPrefab(Global.BUILD_TAG);
        EnableConstructionZone();
        return isPlaced;
    } 

    public void EnableLayModel()
    {
        if(ConstructionZone != null)
            ConstructionZone.SetActive(false);
        TransparentModel.SetActive(true);
        FinalModel.SetActive(false);
    }

    public void EnableFinalModel()
    {
        TransparentModel.SetActive(false);
        if (ConstructionZone != null)
            ConstructionZone.SetActive(false);
        FinalModel.SetActive(true);
    }

    public void FinishBuild()
    {
        isFinished = true;
        EnableFinalModel();
        p_Finished = true;
        //SoundManager.PlaySound(SoundList.Instance.BuildSound);
    }

    public virtual void OnMouseDown()
    {
        if (!isFinished) return;        
        //Ignore all UI targets
        if (EventSystem.current.IsPointerOverGameObject()) return;
        IsSelected = true;
        SelectionUI.UpdateSingleTarget(this);        
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
