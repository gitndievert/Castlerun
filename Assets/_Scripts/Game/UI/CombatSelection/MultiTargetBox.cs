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

using SBK.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MultiTargetBox : DSingle<MultiTargetBox>
{
    /// <summary>
    /// Unsorted list UI Transform
    /// </summary>
    public Transform UnsortedList;

    /// <summary>
    /// List of GameObjects
    /// </summary>    
    private List<SelectionFeatures> _selections = new List<SelectionFeatures>();
    
    

    protected override void PAwake()
    {        
        foreach (Transform t in UnsortedList)
        {
            var sf = t.GetComponent<SelectionFeatures>();
            sf.gameObject.SetActive(false);
            _selections.Add(sf);
        }
        
        //So on awake, get all 20 list objects and store them
        //Need something to triger enable and disable, so when the list gets refreshed
        //Count of MassSelectionList must equal the total number in visible sort list
    }

    protected override void PDestroy()
    {
        //
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateList(ISelectable selection)
    {
        int count = _selections.Count;
        _selections[count].TargetText.text = selection.DisplayName;
        _selections[count].TargetIcon.sprite = selection.GetIcon();
        _selections[count].HealthBar.BarValue = selection.GetCurrentHealth();
        _selections[count].gameObject.SetActive(true);
    }

    public void UpdateList(List<ISelectable> list)
    {
        ClearList();
        for (int i = 0; i < list.Count; i++)
        {
            _selections[i].TargetText.text = list[i].DisplayName;
            _selections[i].TargetIcon.sprite = list[i].GetIcon();
            _selections[i].HealthBar.BarValue = list[i].GetCurrentHealth();
            _selections[i].gameObject.SetActive(true);
        }
    }

    public void ClearList()
    {
        if (_selections != null && _selections.Count > 0)
        {
            foreach (var selection in _selections)
            {
                selection.gameObject.SetActive(false);
            }
        }
    }

    public void ClearSingleSelectable(ISelectable selectable)
    {

    }
}
