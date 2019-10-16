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
using SBK.Unity;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public enum TextType
{
    Normal,
    Damage,
    Heal
}

public partial class UIManager : PSingle<UIManager>
{
    public Sprite DefaultIcon;

    public TextMeshProUGUI Messages;    

    /// <summary>
    /// Inventory Panel
    /// </summary>
    [Header("Inventory")]
    public InventoryUI InventoryUIPanel;

    [Header("Player")]
    public ProgressBar HealthBar;

    public ProgressBar StaminaBar;
    /// <summary>
    /// Player Panel
    /// </summary>
    public PlayerUI PlayerUIPanel;

    /// <summary>
    /// Panel for selecting Buildings and Troops
    /// </summary>
    [Header("Building")]
    public BuildUI BuildingUIPanel;

 
    [Header("Selection System")]

    /// <summary>
    /// Target Panel
    /// </summary>
    public Transform TargetPanel;

    /// <summary>
    /// Selection Controller for all In Game Mouse Selections
    /// </summary>
    public Selection SelectableComponent;

    /// <summary>
    /// Target Box (Single Selection)
    /// </summary>    
    public SingleTargetBox SingleTargetBox;

    /// <summary>
    /// Multitarget Box Manager (All Selected Targets)
    /// </summary>    
    public MultiTargetBox MultiTargetBox;

    /// <summary>
    /// Enemy Target Box (Single Selection)
    /// </summary>    
    public SingleTargetBox EnemyTargetBox;

    [Header("UI Elements")]
    public PopupText PopText;

    private Canvas _canvas;
    private RectTransform _canvasTransform;

    protected override void PAwake()
    {
        if (SelectableComponent == null)
            SelectableComponent = GetComponent<Selection>();
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        _canvasTransform = _canvas.GetComponent<RectTransform>();
    }
    
    protected override void PDestroy()
    {
        
    }   

    /// <summary>
    /// This keep the mouse on the UI and prevents raycasting from running through. 
    /// Use this in a Physics.Raycast()
    /// </summary>
    /// <returns>bool</returns>
    public bool IsMouseOverUI()
    {
        var curPos = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(curPos, results);
        return results.Count > 0;
    }

    public void FloatText(string text, Transform trans)
    {
        var pt = Instantiate(PopText);
        pt.transform.SetParent(_canvas.transform, false);
    }    

    public void FloatCombatText(TextType type, int amount, bool critical, Transform trans)
    {
        var pt = Instantiate(PopText);
 
        var vp = Camera.main.WorldToViewportPoint(trans.position);
        var textScreenPos = new Vector2(
        ((vp.x * _canvasTransform.sizeDelta.x) - (_canvasTransform.sizeDelta.x * 0.5f)),
        ((vp.y * _canvasTransform.sizeDelta.y) - (_canvasTransform.sizeDelta.y * 0.5f)));

        pt.GetComponent<RectTransform>().anchoredPosition = textScreenPos;
        pt.transform.SetParent(_canvas.transform, false);        
        pt.SetCombatText(type, amount.ToString(), critical);
    }
    
}
