﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCursor : MonoBehaviour
{
    public GameObject BattleCursorObj;
    public float Distance = 0f;
    public bool CursorOn;

    private Vector3 _mousePosition;
    private Vector3 _targetPosition;    
    private GameObject _cursor;    
    
    // Start is called before the first frame update
    void Start()
    {
        CursorOn = false;
        _cursor = Instantiate(BattleCursorObj);        
    }

    // Update is called once per frame
    void Update()
    {
        if (!CursorOn) return;
        MoveCurrentObjectToMouse();
    }
      
    public void On()
    {
        MakeActive(true);
    }

    public void Off()
    {
        MakeActive(false);
    }

    public void Toggle()
    {
        CursorOn = !CursorOn;
        MakeActive(CursorOn);
    }

    private void MakeActive(bool set)
    {
        CursorOn = set;
        _cursor.SetActive(set);
    }

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.gameObject.layer == Global.GROUND_LAYER)
            {
                //_cursor.transform.position = hit.point;
                _cursor.transform.position = new Vector3(Mathf.Round(hit.point.x), hit.point.y + 5f, Mathf.Round(hit.point.z));
            }
            else if(hit.transform.gameObject.layer == Global.DEFAULT_LAYER)
            {
                _cursor.transform.position = new Vector3(Mathf.Round(hit.point.x), hit.point.y + 5f, Mathf.Round(hit.point.z));
                //Figure out how to rotate
                //_cursor.transform.rotation = Quaternion.LookRotation(hit.point);
            }
        }
    }

    /*private float GetDistToGround()
    {
        return (_placeObjectMeshRend != null) ? _placeObjectMeshRend.bounds.extents.y : 0f;
    }*/
}
