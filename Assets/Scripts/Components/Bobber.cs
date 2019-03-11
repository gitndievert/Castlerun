using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    public float BobHeight = 0.5f;
    public float RotateSpeed = 1f;
    public float BobSpeed = 1f;

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
        if(AutoStartBob)
        {
            StartBob(Rotate);
        }
    }

    private void Update()
    {
        if (_isBobbing)
        {
            float sinY = Mathf.Sin(Time.time * BobSpeed);
            transform.position = new Vector3(transform.position.x, sinY, transform.position.z) * BobHeight;
        }
        if(_isRotating)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * RotateSpeed);
        }
    }

    public void StartBob(bool rotate = false)
    {
        _isRotating = rotate;
        StartCoroutine(Bob());
    }

    public void StopBob()
    {
        StopCoroutine(Bob());
    }

    private IEnumerator Bob()
    {
        float transSeconds = 0.3f;
        //Vector3 pos = transform.position;
        //transform.position = new Vector3(pos.x, pos.y + BobHeight, pos.z);
        yield return new WaitForSeconds(transSeconds);
        _isBobbing = true;
        yield return new WaitForSeconds(transSeconds);
    }

}
