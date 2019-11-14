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


using System.Collections.Generic;
using UnityEngine;
using SBK.Unity;

public class Selection : DSingle<Selection>
{
    /// <summary>
    /// List for Mass Friendly Selections
    /// </summary>
    public List<ISelectable> MassSelectionList = new List<ISelectable>();

    /// <summary>
    /// If one target (enemy or friendly) it goes here
    /// </summary>
    public ISelectable SingleTargetSelected = null;

    /// <summary>
    /// If one target (enemy or friendly) it goes here
    /// </summary>
    public ISelectable EnemyTargetSelected = null;

    public Color SelectionBoxColor;
    public Color BorderColor;

    public static bool IsSelecting = false;    
        
    private Vector3 mousePosition1;
    private UIManager _ui;
    
    public static Vector3 GroundPoint
    {
        get
        {
            if (Physics.Raycast(SelectionRayHit, out RaycastHit hit))
            {
                if (hit.transform.gameObject.layer == Global.GROUND_LAYER && hit.point != null) return hit.point;
            }

            return Vector3.zero; // Some Top Level Point of player, might be a big bug
        }
    }

    public static Ray SelectionRayHit
    {
        get
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

    public int SelectionListCount
    {
        get { return MassSelectionList.Count; }
    }
        
    protected override void PAwake()
    {
        
    }

    protected override void PDestroy()
    {

    }

    private void Start()
    {
        _ui = UIManager.Instance;
    }

    private void Update()
    {
        bool ctrlkey = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);        

        //Selection mouse events
        if (Input.GetMouseButtonDown(KeyBindings.LEFT_MOUSE_BUTTON))
        {
            if (ctrlkey)
            {
                IsSelecting = true;
                mousePosition1 = Input.mousePosition;
            }

            LeftClickActions();
        }        

        //Stop Selection
        if (Input.GetMouseButtonUp(KeyBindings.LEFT_MOUSE_BUTTON))
        {
            IsSelecting = false;
        }               

    }

    private void OnGUI()
    {
        if (!IsSelecting) return;
        //Need to hold down ctrl for mass selector        
        var rect = SelectionBoxRect.GetScreenRect(mousePosition1, Input.mousePosition);
        SelectionBoxRect.DrawScreenRect(rect, SelectionBoxColor);
        SelectionBoxRect.DrawScreenRectBorder(rect, 2, BorderColor);        
    }

    private void LeftClickActions()
    {
        if (Physics.Raycast(SelectionRayHit, out RaycastHit hit))
        {
            //Deselect on ground on building selection
            if (hit.point != null)
            {
                if (hit.GetLayer() == Global.GROUND_LAYER && SingleTargetSelected != null)
                {                    
                    SingleTargetSelected.UnSelect();
                    ClearSingleTarget();
                    return;                                        
                }

                //Check for death
                var selectable = hit.transform.GetComponent<ISelectable>();
                if (selectable != null)
                {
                    if (selectable.IsDead) return;
                    
                    if (hit.transform.tag == Global.ENEMY_TAG)
                    {
                        UpdateEnemyTarget(selectable);
                    }                                            
                }
            }
        }
    }
    
        
    public bool IsWithinSelectionBounds(GameObject gameObject)
    {
        if (!IsSelecting) return false;       

        var camera = Camera.main;
        var viewportBounds =
            SelectionBoxRect.GetViewportBounds(camera, mousePosition1, Input.mousePosition);

        bool insideBox = viewportBounds.Contains(camera.WorldToViewportPoint(gameObject.transform.position));

        return insideBox && gameObject.GetComponent<ISelectable>() != null;
    }

    public void UpdateMassList(ISelectable selection)
    {        
        MassSelectionList.Add(selection);
        selection.Highlight(true, 1);        

        //Also update the SingleTarget with First Selection
        SingleTargetSelected = MassSelectionList[0];
        _ui.SingleTargetBox.SetTargetBox(SingleTargetSelected);

    }

    public void UpdateSingleTarget(ISelectable selection)
    {
        if (selection == SingleTargetSelected) return;
        if (SingleTargetSelected != null) ClearSingleTarget();        
        SingleTargetSelected = selection;
        _ui.SingleTargetBox.SetTargetBox(SingleTargetSelected);
        
        ClearAll();

        SingleTargetSelected.Highlight(true, 1);
    }

    public void SelectSingleTarget(ISelectable selection)
    {
        //We want this to switch the selection and retain the main list
        //allowing single selection of troops FROM the list
        
        /*if (selection == SingleTargetSelected) return;
        if (SingleTargetSelected != null) _ui.SingleTargetBox.ClearTarget();
        SingleTargetSelected = selection;
        _ui.SingleTargetBox.SetTarget(SingleTargetSelected);*/
    }

    public void UpdateEnemyTarget(ISelectable selection)
    {
        if (selection == EnemyTargetSelected) return;
        if (EnemyTargetSelected != null) ClearEnemyTarget();
        EnemyTargetSelected = selection;
        _ui.EnemyTargetBox.SetTargetBox(EnemyTargetSelected);        
        EnemyTargetSelected.Highlight(true, 0);
    }

    public void PreMenuClear()
    {
        ClearAll();
        if (SingleTargetSelected != null)
            ClearSingleTarget();
    }
        
    public void ClearSingleTarget()
    {
        if (SingleTargetSelected != null)
        {
            SingleTargetSelected.UnSelect();
            SingleTargetSelected.Highlight(false);            
            SingleTargetSelected = null;
            _ui.SingleTargetBox.ClearTargetBox();            
        }
    }

    public void ClearEnemyTarget()
    {
        if (EnemyTargetSelected != null)
        {
            EnemyTargetSelected.UnSelect();
            EnemyTargetSelected.Highlight(false);
            EnemyTargetSelected = null;
            _ui.EnemyTargetBox.ClearTargetBox();            
        }
    }   

    public void ClearAll()
    {
        if (SelectionListCount > 0)
        {
            foreach (var select in MassSelectionList)
            {
                select.UnSelect();
                select.Highlight(false);
            }                       
        }        
    }   

    private void TargetPanel(bool show)
    {
        UIManager.Instance.TargetPanel.gameObject.SetActive(show);
    }

}
