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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DestructibleWall : BasePrefab, ISelectable
{
    #region ISelectable Properties
    public bool IsSelected { get; set; }
    public GameObject GameObject => gameObject;
    public string DisplayName { get; set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        CanExplode = true;
    }

    public void OnMouseDown()
    {
        /*IsSelected = true;
        switch (GetTag)
        {
            case Global.BUILD_TAG:
                SelectionUI.UpdateSingleTarget(this);
                break;
            case Global.ENEMY_TAG:
                SelectionUI.UpdateEnemyTarget(this);
                break;
        }*/
    }

    public void Select()
    {
        if (!IsSelected)
        {
            IsSelected = true;
        }
    }

    public void UnSelect()
    {
        if (IsSelected)
        {
            IsSelected = false;
        }
    }

    public void SetTargetedByPlayer(Player player)
    {
        TargetByPlayer = player;
    }
}
