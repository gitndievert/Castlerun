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
using SBK.Unity;
using TMPro;

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

    /// <summary>
    /// Selection target ground visual
    /// </summary>
    public GameObject SelectionTargetObj;

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
        //Selection mouse events
        if (Input.GetMouseButtonDown(KeyBindings.LEFT_MOUSE_BUTTON))
        {
            IsSelecting = true;
            mousePosition1 = Input.mousePosition;            

            if (Physics.Raycast(SelectionRayHit, out RaycastHit hit))
            {
                //Deselect on ground on building selection
                if (hit.point != null)
                {
                    if(hit.transform.tag == Global.ARMY_TAG && SingleTargetSelected != null)
                    {                        
                        UpdateSingleTarget(hit.transform.GetComponent<ISelectable>());
                    }
                    else if (hit.transform.tag == Global.ENEMY_TAG && EnemyTargetSelected != null)
                    {
                        UpdateEnemyTarget(hit.transform.GetComponent<ISelectable>());
                    }
                    else if (hit.transform.gameObject.layer == Global.GROUND_LAYER
                        || ((hit.transform.tag == Global.ARMY_TAG) && SelectionListCount < 1))
                    {
                        ClearAll();

                        if (SingleTargetSelected != null)
                        {
                            SingleTargetSelected.UnSelect();
                            ClearSingleTarget();
                        }

                        if (EnemyTargetSelected != null)
                        {
                            EnemyTargetSelected.UnSelect();
                            ClearEnemyTarget();
                        }
                    }
                }
            }
        }
        //Select on the ground if not mouselooking and there are selections in queue
        else if (Input.GetMouseButtonDown(KeyBindings.RIGHT_MOUSE_BUTTON) 
            && !Global.MouseLook && SelectionListCount > 0)
        {
            StartCoroutine("SelectionCursor");

            if (Physics.Raycast(SelectionRayHit, out RaycastHit hit))
            {
                if (hit.transform != null)
                {
                    if (hit.transform.gameObject.layer == Global.GROUND_LAYER || hit.transform.tag == Global.ENEMY_TAG)
                    {
                        foreach (var select in MassSelectionList)
                        {
                            var character = select.GameObject.GetComponent<Troop>();
                            if (character != null)
                            {                                
                                character.Move(hit.point);                                

                                if (hit.transform.gameObject.layer == Global.GROUND_LAYER)
                                {
                                    character.ClearEnemyTargets();                                    
                                }
                                else if (hit.transform.tag == Global.ENEMY_TAG /*&& !character.IsAttacking*/)
                                {
                                    Debug.Log("Attacking!");
                                    var enemy = hit.transform.GetComponent<ISelectable>();
                                    EnemyTargetSelected = enemy;
                                    _ui.EnemyTargetBox.SetTarget(EnemyTargetSelected);
                                    UpdateEnemyTarget(EnemyTargetSelected);                                    
                                    enemy.TargetingMe.Add(character);
                                    character.Target(enemy);
                                }
                            }
                        }
                    }                   
                }
            }            
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
        var rect = SelectionBoxRect.GetScreenRect(mousePosition1, Input.mousePosition);        
        SelectionBoxRect.DrawScreenRect(rect, SelectionBoxColor);        
        SelectionBoxRect.DrawScreenRectBorder(rect, 2, BorderColor);
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
        _ui.MultiTargetBox.UpdateList(MassSelectionList);

        //Also update the SingleTarget with First Selection
        SingleTargetSelected = MassSelectionList[0];
        _ui.SingleTargetBox.SetTarget(SingleTargetSelected);

    }

    public void UpdateSingleTarget(ISelectable selection)
    {
        if (selection == SingleTargetSelected) return;
        if (SingleTargetSelected != null)
        {
            ClearSingleTarget();
        }
        SingleTargetSelected = selection;
        _ui.SingleTargetBox.SetTarget(SingleTargetSelected);
        ClearAll();
        UpdateMassList(SingleTargetSelected);       
    }

    public void UpdateSingleTarget(GameObject gameObject)
    {
        UpdateSingleTarget(gameObject.GetComponent<ISelectable>());
    }

    public void UpdateEnemyTarget(ISelectable selection)
    {
        if (selection == EnemyTargetSelected) return;
        if (EnemyTargetSelected != null)
        {
            ClearEnemyTarget();
        }
        EnemyTargetSelected = selection;
        _ui.EnemyTargetBox.SetTarget(EnemyTargetSelected);
    }

    public void UpdateEnemyTarget(GameObject gameObject)
    {
        UpdateEnemyTarget(gameObject.GetComponent<ISelectable>());
    }

    public void ClearSingleTarget()
    {
        SingleTargetSelected.UnSelect();
        ClearList(SingleTargetSelected);
        SingleTargetSelected = null;
        _ui.SingleTargetBox.ClearTarget();
    }

    public void ClearEnemyTarget()
    {
        EnemyTargetSelected.UnSelect();
        EnemyTargetSelected = null;
        _ui.EnemyTargetBox.ClearTarget();
    }

    public void ClearList()
    {
        if (MassSelectionList.Count > 0)
        {
            MassSelectionList.Clear();
            _ui.MultiTargetBox.ClearList();
        }
    }

    public void ClearList(ISelectable selection)
    {
        MassSelectionList.Remove(selection);
        _ui.MultiTargetBox.ClearList();
    }

    public void ClearAll()
    {
        if (SelectionListCount > 0)
        {
            foreach (var select in MassSelectionList)
            {
                select.UnSelect();
            }

            ClearList();
        }
    }

    private IEnumerator SelectionCursor()
    {
        SelectionTargetObj.SetActive(true);
        SelectionTargetObj.transform.position = GroundPoint;
        yield return new WaitForSeconds(.5f);
        SelectionTargetObj.SetActive(false);
    }

    private void TargetPanel(bool show)
    {
        UIManager.Instance.TargetPanel.gameObject.SetActive(show);
    }

}
