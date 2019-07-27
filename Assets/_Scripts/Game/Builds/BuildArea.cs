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

[RequireComponent(typeof(Collider))]
public class BuildArea : MonoBehaviour
{
    public bool CanBuild { get; private set; }
    
    [Header("Collider Colors")]
    public Color ValidSpot = Color.green;
    public Color InvalidSpot = Color.red;

    private Collider _col;
    private Renderer _rend;
    
    // Start is called before the first frame update
    void Start()
    {
        _col = GetComponent<Collider>();
        _col.isTrigger = true;
        _rend = GetComponent<Renderer>();
        transform.tag = Global.BUILDAREA_TAG;
        //Initialize Builder
        CanBuild = true;
        SetPlaneColor(ValidSpot);
    }

    public void ShowPlane(bool show)
    {        
        _rend.enabled = show;
    }

    public void TogglePlane()
    {
        _rend.enabled = !_rend.enabled;
    }

    public void SetPlaneColor(Color color)
    {
        if(_rend.enabled)
            _rend.material.color = color;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != Global.BUILDAREA_TAG) return;
        CanBuild = false;
        SetPlaneColor(InvalidSpot);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.transform.tag != Global.BUILDAREA_TAG) return;
        CanBuild = true;
        SetPlaneColor(ValidSpot);
    }

    
   
}
