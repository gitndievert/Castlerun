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
using UnityEngine;

public enum MiniTargetSeverity
{
    Low,
    Medium,
    High
}

public class MiniMapControls : PSingle<MiniMapControls>
{
    const float CAMERA_DAMPENING = 0.15f;

    /// <summary>
    /// MiniMap Camera
    /// </summary>
    public Camera MiniMapCamera;
    
    /// <summary>
    /// Max Zoom Factor for Minimap
    /// </summary>
    public float MaxZoomFactor = 120f;

    /// <summary>
    /// Max Zoom Factor for Minimap
    /// </summary>
    public float MinZoomFactor = 40f;

    //Minimap target used to showing activity
    public GameObject MiniMapTarget;

    [SerializeField]
    private float _zoomFactorIncrement = 20f;

    private Vector3 _origPos;    
    private Transform _cameraTransformParent;
    private Vector3 _velocity = Vector3.zero;

    public static Transform PlayerTransform;

    public float SetZoomFactor
    {
        set {
            if(value % 2 == 0 && value > 0f && value <= MaxZoomFactor)
                _zoomFactorIncrement = value;
        }
    }

    protected override void PAwake()
    {

    }

    protected override void PDestroy()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        if (MiniMapCamera == null)
            throw new System.Exception("Cannot find MiniMap in Scene!");
        MiniMapCamera.orthographicSize = MaxZoomFactor;
        _origPos = MiniMapCamera.transform.position;
        _cameraTransformParent = MiniMapCamera.transform.parent;     
    }

    private void Update()
    {      
        //Show camera only outside, otherwise we hide camera and 
        if (Global.InsideCastle)
        {
            if(MiniMapCamera.gameObject.activeSelf) MiniMapCamera.gameObject.SetActive(false);
        }
        else if (!Global.InsideCastle)
        {            
            if(!MiniMapCamera.gameObject.activeSelf) MiniMapCamera.gameObject.SetActive(true);
            
            if (PlayerTransform != null)
            {
                MiniMapCamera.transform.rotation = Quaternion.Euler(90, 0, 0);
                Vector3 point = MiniMapCamera.WorldToViewportPoint(PlayerTransform.position);
                var delta = PlayerTransform.position - MiniMapCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
                var destination = MiniMapCamera.transform.position + delta;
                MiniMapCamera.transform.position = Vector3.SmoothDamp(MiniMapCamera.transform.position, destination, ref _velocity, CAMERA_DAMPENING);
            }
        }        
    }

    public void ZoomIn()
    {
        if ((MiniMapCamera.orthographicSize - _zoomFactorIncrement) <= MinZoomFactor) return;        
        MiniMapCamera.orthographicSize -= _zoomFactorIncrement;        
    }

    public void ZoomOut()
    {
        if ((MiniMapCamera.orthographicSize + _zoomFactorIncrement) >= MaxZoomFactor) return;
        MiniMapCamera.orthographicSize += _zoomFactorIncrement;        
    }
    
    public void MapTarget(GameObject target, MiniTargetSeverity severity)
    {        
        Vector3 targetPos = target.transform.position;
        MiniMapTarget.transform.position = new Vector3(targetPos.x, MiniMapTarget.transform.position.y, targetPos.z);
        switch (severity)
        {

        }
    }
}
