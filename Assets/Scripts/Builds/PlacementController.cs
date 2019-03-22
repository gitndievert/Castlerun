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
    private MeshRenderer _placeObjectMeshRend;

    private GameObject _currObj;
    private float mouseWheelRotation;
    private bool _triggerBuild = false;

    /// <summary>
    /// Object to parent on for player 1
    /// </summary>
    private Transform _player1Builds;

    protected override void PAwake()
    {        
        _player1Builds = GameObject.Find("Player_1_Builds").transform;
    }

    protected override void PDestroy()
    {
        
    }

    public void LoadObject(GameObject obj)
    {
        placeableObjectPrefab = obj;
        _placeObjectMeshRend = placeableObjectPrefab.GetComponent<MeshRenderer>();
        _triggerBuild = true;
    }
        
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currObj = null;
            BuildMode = false;
            return;
        }

        if (_triggerBuild)
        {
            if (_currObj != null)
            {
                Destroy(_currObj);
            }
            else
            {
                _currObj = Instantiate(placeableObjectPrefab);
                _currObj.transform.parent = _player1Builds; //sets the player 1 parent
            }

            _triggerBuild = false;
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
