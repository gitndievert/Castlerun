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

public class Selectable : MonoBehaviour
{
    public List<GameObject> SelectionList = new List<GameObject>();
    
    public Color SelectionBoxColor;
    public Color BorderColor;

    public static bool IsSelecting = false;
    private Vector3 mousePosition1;

    private void Update()
    {
        //Start selection
        if (Global.BuildMode || Global.BattleMode)
        {
            if (Input.GetMouseButtonDown(KeyBindings.LEFT_MOUSE_BUTTON))
            {
                IsSelecting = true;
                mousePosition1 = Input.mousePosition;               
            }

            //Stop Selection
            if (Input.GetMouseButtonUp(KeyBindings.LEFT_MOUSE_BUTTON))
            {
                IsSelecting = false;
            }
        }
    }

    private void OnGUI()
    {
        if (!IsSelecting) return;               
        var rect = SelectionBox.GetScreenRect(mousePosition1, Input.mousePosition);        
        SelectionBox.DrawScreenRect(rect, SelectionBoxColor);        
        SelectionBox.DrawScreenRectBorder(rect, 2, BorderColor);
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

    public void UpdateList(ISelectable selection)
    {
        SelectionList.Add(selection.GameObject);
    }

    public void ClearList()
    {
        if (SelectionList.Count > 0)
            SelectionList.Clear();
    }

    public void ClearList(ISelectable selection)
    {
        SelectionList.Remove(selection.GameObject);
    }
}
