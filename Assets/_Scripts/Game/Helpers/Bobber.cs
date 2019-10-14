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

[RequireComponent(typeof(Rigidbody))]
public class Bobber : MonoBehaviour
{
    [Range(0,3f)]
    public float BobHeight = 0.5f;
    [Range(1f, 60f)]
    public float RotateSpeed = 1f;
    [Range(0, 10f)]
    public float BobSpeed = 1f;
    [Range(0, 5f)]
    public float BumpForce = 2f;

    public bool AutoStartBob = false;
    public bool Rotate = false;

    private Rigidbody _rb;    
    private bool _isBobbing;
    private bool _isRotating;    

    private void Awake()
    {        
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {        
        if (AutoStartBob)
            StartBob(Rotate);        
    }

    private void Update()
    {               
        if (_isBobbing)
        {
            if (_isRotating)
            {
                transform.Rotate(Vector3.up, Time.deltaTime * RotateSpeed);
            }
            float sinY = Mathf.Sin(Time.time * BobSpeed) * BobHeight;
            transform.position = new Vector3(transform.position.x, sinY + transform.position.y + 1, transform.position.z);            
        }        
    }

    public void StartBob(bool rotate = false)
    {        
        _isRotating = rotate;
        _isBobbing = true;
    }

    public void StopBob()
    {
        _isBobbing = false;
    }

}
