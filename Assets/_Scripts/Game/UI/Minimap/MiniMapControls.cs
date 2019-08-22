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

public class MiniMapControls : MonoBehaviour
{
    /// <summary>
    /// MiniMap Camera
    /// </summary>
    public Camera MiniMapCamera;
    
    /// <summary>
    /// Max Zoom Factor for Minimap
    /// </summary>
    public float MaxZoomFactor = 160f;

    /// <summary>
    /// Max Zoom Factor for Minimap
    /// </summary>
    public float MinZoomFactor = 60f;

    [SerializeField]
    private float _zoomFactorIncrement = 20f;
    [SerializeField]
    private bool _camFollow = false;

    private Vector3 _origPos;    
    private Transform _cameraTransformParent;
    private Vector3 _velocity = Vector3.zero;

    public static Transform PlayerTransform;

    public float SetZoomFactor
    {
        set {
            if(value % 2 == 0 && value > 0f && value <= 160f)
                _zoomFactorIncrement = value;
        }
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
        //Need to figure out handlers and events for inside the castle
        if(PlayerTransform != null)
        {   
            MiniMapCamera.transform.rotation = Quaternion.Euler(90,0,0);
            Vector3 point = MiniMapCamera.WorldToViewportPoint(PlayerTransform.position);
            Vector3 delta = PlayerTransform.position - MiniMapCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = MiniMapCamera.transform.position + delta;
            MiniMapCamera.transform.position = Vector3.SmoothDamp(MiniMapCamera.transform.position, destination, ref _velocity, 0.15f);
        }

        if (Global.InsideCastle)
        {
            MiniMapCamera.gameObject.SetActive(false);
        }
        else
        {
            MiniMapCamera.gameObject.SetActive(true);
        }        
    }

    public void ZoomIn()
    {
        if ((MiniMapCamera.orthographicSize - _zoomFactorIncrement) <= MinZoomFactor) return;
        //MiniMapCamera.transform.parent = PlayerTransform;        
        //_camFollow = true;
        MiniMapCamera.orthographicSize -= _zoomFactorIncrement;
        //Need to COME BACK and position this correctly
        //MiniMapCamera.ScreenToWorldPoint(new Vector2(PlayerTransform.position.x * 2, PlayerTransform.position.z * 2));        
    }

    public void ZoomOut()
    {
        if ((MiniMapCamera.orthographicSize + _zoomFactorIncrement) >= MaxZoomFactor)
        {
            //MiniMapCamera.transform.parent = _cameraTransformParent;
            //_camFollow = false;
            return;
        }
        else
        {
            MiniMapCamera.orthographicSize += _zoomFactorIncrement;
        }
    }
}
