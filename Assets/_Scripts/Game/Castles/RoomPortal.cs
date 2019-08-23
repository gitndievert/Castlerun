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

public enum WarpDirection
{
    North,
    South,
    East,
    West
}

public enum WarpType
{
    Entrance,
    Exit
}

[RequireComponent(typeof(BoxCollider))]
public class RoomPortal : MonoBehaviour
{    
    public RoomPortal DestinationPortal;
    [Space(10)]
    public WarpDirection WarpOutDirection = WarpDirection.North;
    [Space(10)]
    public WarpType WarpType = WarpType.Entrance;
        
    private Collider _col;    
    
    private void Awake()
    {
        _col = GetComponent<Collider>();
        _col.isTrigger = true;        
    }
   
    /// <summary>
    /// Teleport the player to selected destination
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerEnter(Collider col)
    {
        if (WarpType == WarpType.Exit) return;
        if (col.transform.tag == "Player" && DestinationPortal != null 
            && WarpType == WarpType.Entrance)
        {
            //CC On Player will jack with position, need to disable and re-enable
            col.transform.GetComponent<CharacterController>().enabled = false;            
            col.transform.position = DestinationPortal.GetComponent<Collider>().bounds.center;
            //Rotate(col.transform);
            col.transform.GetComponent<CharacterController>().enabled = true;                     
        }        
    }
        
    
    private void Rotate(Transform target)
    {
        float yDir = 0f;
        switch (WarpOutDirection)
        {            
            case WarpDirection.East:
                yDir = 90f;
                break;
            case WarpDirection.South:
                yDir = 180f;
                break;
            case WarpDirection.West:
                yDir = 270f;
                break;
        }

        target.Rotate(0, yDir, 0);
    }
}