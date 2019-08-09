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
    
    public Color SelectionBoxColor;
    public Color BorderColor;
    public static bool IsSelecting = false;
    public GameObject SelectionTargetObj;    

    private Vector3 mousePosition1;
    private TroopUI _troopUI;

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

    protected override void PAwake()
    {
        if (SelectionTargetObj == null)
            throw new System.Exception("You must bind a selection target object to selectables!");
        SelectionTargetObj.SetActive(false);
    }

    protected override void PDestroy()
    {

    }

    private void Start()
    {
        _troopUI = UIManager.Instance.TroopUI;
    }      

    private void Update()
    {
        //Selection mouse events
        if (Input.GetMouseButtonDown(KeyBindings.LEFT_MOUSE_BUTTON))
        {
            IsSelecting = true;
            mousePosition1 = Input.mousePosition;
            int listcount = MassSelectionList.Count;

            if (Physics.Raycast(SelectionRayHit, out RaycastHit hit))
            {
                //Deselect on ground on building selection
                if (hit.point != null)
                {
                    if (hit.transform.gameObject.layer == Global.GROUND_LAYER
                        || (hit.transform.tag == Global.ARMY_TAG && listcount < 1))
                    {
                        if (listcount > 0)
                        {
                            foreach (var select in MassSelectionList)
                            {
                                select.UnSelect();
                            }
                                                                                   
                            ClearList();
                        }

                        if (SingleTargetSelected != null)
                        {
                            SingleTargetSelected.UnSelect();
                            ClearSingleTarget();
                        }
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(KeyBindings.RIGHT_MOUSE_BUTTON) 
            && !SelectionTargetObj.activeSelf && !Global.MouseLook)
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

                                if (hit.transform.tag == Global.ENEMY_TAG)
                                {
                                    Debug.Log("Attacking!");
                                    character.Target(hit.transform.GetComponent<ISelectable>());
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
        var rect = SelectionBox.GetScreenRect(mousePosition1, Input.mousePosition);        
        SelectionBox.DrawScreenRect(rect, SelectionBoxColor);        
        SelectionBox.DrawScreenRectBorder(rect, 2, BorderColor);
    }

    private TargetUI TargetUI
    {
        get
        {
            return UIManager.Instance.TargetUI;
        }
    }

    public bool IsWithinSelectionBounds(GameObject gameObject)
    {
        if (!IsSelecting) return false;       

        var camera = Camera.main;
        var viewportBounds =
            SelectionBox.GetViewportBounds(camera, mousePosition1, Input.mousePosition);

        bool insideBox = viewportBounds.Contains(camera.WorldToViewportPoint(gameObject.transform.position));        

        return insideBox && gameObject.GetComponent<ISelectable>() != null;        
    }

    public void UpdateMassList(ISelectable selection)
    {
        MassSelectionList.Add(selection);
        _troopUI.UnsortedListText.text += selection + "\r\n";
    }

    public void UpdateSingleTarget(ISelectable selection)
    {
        SingleTargetSelected = selection;
        Debug.Log("Single Target " + selection.ToString());
        TargetUI.SingleTargetBox.GetComponentInChildren<TextMeshProUGUI>().text = SingleTargetSelected.ToString();

        //COME BACK - update the ui here
        //Need to make this friendly to access somehow
        //TargetUI.SingleTargetBox.
    }

    public void ClearSingleTarget()
    {
        SingleTargetSelected = null;
        TargetUI.SingleTargetBox.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
    }

    public void ClearList()
    {
        if (MassSelectionList.Count > 0)
        {
            MassSelectionList.Clear();
            _troopUI.UnsortedListText.text = string.Empty;
        }
    }

    public void ClearList(ISelectable selection)
    {
        MassSelectionList.Remove(selection);
        _troopUI.UnsortedListText.text = string.Empty;
    }

    private IEnumerator SelectionCursor()
    {
        SelectionTargetObj.SetActive(true);
        SelectionTargetObj.transform.position = GroundPoint;
        yield return new WaitForSeconds(.5f);
        SelectionTargetObj.SetActive(false);
    }
       

    
}
