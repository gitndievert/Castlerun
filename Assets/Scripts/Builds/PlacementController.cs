using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SBK.Unity;

public class PlacementController : PSingle<PlacementController>
{
    const int GROUND_LAYER = 8;

    public GameObject placeableObjectPrefab;
    

    public bool BuildMode = false;
    public float RotateAmount = 45f;

    [SerializeField]
    private readonly KeyCode newObjectHotkey = KeyBindings.BuildKey1;

    [SerializeField]
    private MeshRenderer _placeObjectMeshRend;

    private GameObject _currObj;
    private float mouseWheelRotation;    


    protected override void PAwake()
    {
        LoadObject(placeableObjectPrefab);
    }

    protected override void PDestroy()
    {
        
    }

    public void LoadObject(GameObject obj)
    {
        if (placeableObjectPrefab == null)
            placeableObjectPrefab = obj;

        _placeObjectMeshRend = placeableObjectPrefab.GetComponent<MeshRenderer>();

    }
        
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currObj = null;
            BuildMode = false;
            return;
        }

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
        }
    }   

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
               
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.gameObject.layer == GROUND_LAYER)
            {
                //float offset = hit.point.y + _placeObjectMeshRend.bounds.min.y;
                //_currObj.transform.position = new Vector3(hit.point.x, offset + 2, hit.point.z);                
                _currObj.transform.position = hit.point;
                _currObj.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            }
        }
    }

    private void SnapToGround()
    {

    }

    private void RotateFromMouseWheel()
    {        
        mouseWheelRotation += Input.mouseScrollDelta.y;
        _currObj.transform.Rotate(Vector3.up, mouseWheelRotation * RotateAmount);
    }   
}
