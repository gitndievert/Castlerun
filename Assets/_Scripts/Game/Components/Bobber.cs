using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (!transform.GetComponent(typeof(Rigidbody)))
            gameObject.AddComponent<Rigidbody>();
        _rb = GetComponent<Rigidbody>();        
    }

    private void Start()
    {        
        if (AutoStartBob)
        {
            StartBob(Rotate);
        }
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
