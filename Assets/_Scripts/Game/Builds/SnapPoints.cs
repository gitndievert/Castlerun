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

public enum SnapDirection
{
    None,
    Top,
    Bottom,
    Left,
    Right
}

[RequireComponent(typeof(BoxCollider))]
public class SnapPoints : MonoBehaviour
{
    public bool Snapped;    
    public SnapDirection SnapDirection = SnapDirection.None;

    private Collider _col;
    private IBuild _build;

    protected void Awake()
    {
        Snapped = false;
        _col = GetComponent<Collider>();
        _col.isTrigger = true;
        _build = transform.parent.GetComponent<BasicBuild>();
    }
  
    protected void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == Global.SNAP_POINT_TAG)
        {
            var snap = col.GetComponent<SnapPoints>();

            Debug.Log($"We got a hit on {snap.ToString()}");
            if (snap.SnapDirection == SnapDirection.None) return;
            switch(snap.SnapDirection)
            {
                case SnapDirection.Left:
                case SnapDirection.Right:
                    if(CheckSnapDirections(snap.SnapDirection, SnapDirection))
                    {
                        //Commented out for now
                        //_build.Lock(true);
                    }
                    break;
                case SnapDirection.Top:
                    break;
                case SnapDirection.Bottom:
                    break;
                default:
                    break;

            }
        }
    }
       
    private bool CheckSnapDirections(SnapDirection sideone, SnapDirection sidetwo)
    {        
        return sideone != sidetwo;
    }

    public override string ToString()
    {
        return SnapDirection.ToString();
    }

}
