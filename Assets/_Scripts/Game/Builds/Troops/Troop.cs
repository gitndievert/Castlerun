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

public abstract class Troop : BasePrefab, ICharacter, ISelectable
{
    [Header("All the points that this Troop will Follow")]
    public Transform[] points;

    public Light SelectionTarget;

    //Selectable
    public bool IsSelected { get; set; }

    protected static Color SelectedColor = Color.green;
    protected static Color DamageColor = Color.red;

    private void Start()
    {
        SelectionTargetStatus(false);
    }

    //No Idea what to do, maybe create another BasePrefab class for this???
    protected virtual void Update()
    {
        //For Selectable Troops
        if (UIManager.Instance.SelectableComponent.IsWithinSelectionBounds(gameObject) && !IsSelected)
        {
            Select();   
        }
    }

    public void UnSelect()
    {
        if (IsSelected)
        {
            IsSelected = false;
            SelectionTargetStatus(false);
        }
    }

    public void Select()
    {
        if (!IsSelected)
        {
            IsSelected = true;
            Debug.Log("Hit on " + gameObject.name);
            SelectionTargetStatus(true, SelectedColor);
        }
    }
    
    private void SelectionTargetStatus(bool status)
    {
        if (SelectionTarget == null) return;
        SelectionTarget.gameObject.SetActive(status);
    }

    private void SelectionTargetStatus(bool status, Color color)
    {
        if (SelectionTarget == null) return;
        SelectionTargetStatus(status);
        SelectionTarget.color = SelectedColor;
    }

}
