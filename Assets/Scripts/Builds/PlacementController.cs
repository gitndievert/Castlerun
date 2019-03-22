using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SBK.Unity;

public class PlacementController : PSingle<PlacementController>
{    
    public GameObject placeableObjectPrefab;
    public bool BuildMode = false;

    [SerializeField]
    private readonly KeyCode newObjectHotkey = KeyBindings.BuildKey1;

    private GameObject _currObj;

    private float mouseWheelRotation;



    protected override void PAwake()
    {
        
    }

    protected override void PDestroy()
    {
        
    }

        
    void FixedUpdate()
    {
        if (Input.GetKeyDown(newObjectHotkey))
        {
            if (_currObj != null)
            {
                Destroy(_currObj);
            }
            else
            {
                _currObj = Instantiate(placeableObjectPrefab);
            }
        }

        if (_currObj != null)
        {
            BuildMode = true;
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();
        }
    }   

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
               
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            _currObj.transform.position = hit.point;
            _currObj.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
    }

    private void RotateFromMouseWheel()
    {        
        mouseWheelRotation += Input.mouseScrollDelta.y;
        _currObj.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currObj = null;
            BuildMode = false;
        }
    }
}
