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

public class Selection : MonoBehaviour
{
    public List<ISelectable> SelectionList = new List<ISelectable>();
    
    public Color SelectionBoxColor;
    public Color BorderColor;
    public static bool IsSelecting = false;
    public GameObject SelectionTargetObj;

    private Vector3 mousePosition1;

    public static Vector3 GroundPoint
    {
        get
        {
            RaycastHit hit = SelectionRayHit;
            if (hit.transform.gameObject.layer == Global.GROUND_LAYER) return hit.point;            
            return Vector3.zero;
        }
    }

    public static RaycastHit SelectionRayHit
    {
        get
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hit);
            return hit;            
        }
    }

    private void Awake()
    {
        if (SelectionTargetObj == null)
            throw new System.Exception("You must bind a selection target object to selectables!");
        SelectionTargetObj.SetActive(false);
    }

    private void Update()
    {
        //Start selection        

        //Get the hit
        RaycastHit hit = SelectionRayHit;

        if (Input.GetMouseButtonDown(KeyBindings.LEFT_MOUSE_BUTTON))
        {
            //Deselect on ground on building selection
            if (hit.GetLayer() == Global.GROUND_LAYER || hit.GetTag() == Global.BUILD_TAG)
            {
                foreach(var select in SelectionList)
                {
                    select.UnSelect();                    
                }

                ClearList();
            }

            IsSelecting = true;
            mousePosition1 = Input.mousePosition;               
        }
        else if (Input.GetMouseButtonDown(KeyBindings.RIGHT_MOUSE_BUTTON) 
            && !SelectionTargetObj.activeSelf)
        {
            StartCoroutine("SelectionCursor");            
            
            if(hit.GetLayer() == Global.GROUND_LAYER)
            {
                foreach(var selection in SelectionList)
                {
                    var character = selection.GameObject.GetComponent<ICharacter>();
                    if (character != null)
                    {
                        character.Move(hit.point);
                    }
                }
            }
            else
            {
                if(hit.GetTag() == Global.ARMY_TAG)
                {
                    Debug.Log("Be... All that you can be! "+hit.transform.name);
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
        SelectionList.Add(selection);
    }

    public void ClearList()
    {
        if (SelectionList.Count > 0)
            SelectionList.Clear();
    }

    public void ClearList(ISelectable selection)
    {
        SelectionList.Remove(selection);
    }

    private IEnumerator SelectionCursor()
    {
        SelectionTargetObj.SetActive(true);
        SelectionTargetObj.transform.position = GroundPoint;
        yield return new WaitForSeconds(.5f);
        SelectionTargetObj.SetActive(false);
    }    
    
}
