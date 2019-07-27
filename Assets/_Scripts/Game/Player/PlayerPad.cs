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

public class PlayerPad : MonoBehaviour
{
    [Range(1,Global.PLAYER_MAX_SLOTS)]
    public int PlayerSlot;

    public Vector3 PlayerSpawnPosition;
    public Vector3 CastleSpawnPosition;
    public Quaternion CastleRotation;
    /// <summary>
    /// This parent object contains all the side resources
    /// </summary>
    public GameObject SideResources;

    /// <summary>
    /// Gets all the player assigned resource points
    /// </summary>
    public Transform[] ResourcePoints { get; private set; }    

    private void Start()
    {
        //Load in the reources that are on the curent players territory
        if (SideResources != null)
        {
            ResourcePoints = SideResources.FindComponentsInChildrenWithTag<Transform>(Global.RESOURCE_TAG);                 
        }
    }
}
