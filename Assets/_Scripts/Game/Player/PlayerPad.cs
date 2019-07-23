using SBK.Unity;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPad : MonoBehaviour
{
    [Range(1,4)]
    public int PlayerSlot;

    public Vector3 PlayerSpawnPosition;
    public Vector3 CastleSpawnPosition;
    public Quaternion CastleRotation;
    /// <summary>
    /// This parent object contains all the side resources
    /// </summary>
    public GameObject SideResources;

    [SerializeField]
    private Transform[] _resources;

    private void Start()
    {
        //Load in the reources that are on the curent players territory
        if (SideResources != null)
        {
            _resources = SideResources.FindComponentsInChildrenWithTag<Transform>(Global.RESOURCE_TAG);                 
        }
    }
}
