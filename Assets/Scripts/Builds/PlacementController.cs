using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SBK.Unity;

public class PlacementController : PSingle<PlacementController>
{
    const int GROUND_LAYER = 8;

    public GameObject placeableObjectPrefab = null;

    [Tooltip("This is the transparent lay material")]
    public Material LayMaterial;
    [Tooltip("This is the transparent lay material if you cannot build in a zone")]
    public Material ErrorMaterial;
    
    public bool BuildMode = false;
    public float RotateAmount = 45f;
    public bool SnapOnGrid = true;

    [SerializeField]
    private MeshRenderer _placeObjectMeshRend;

    private GameObject _currObj;
    private Material _saveMaterial;    

    private float mouseWheelRotation;
    private bool _triggerBuild = false;

    /// <summary>
    /// Object to parent on for player 1
    /// </summary>
    [SerializeField]
    private Transform _player1Builds = null;
    /// <summary>
    /// Object to parent on for player 2
    /// </summary>
    [SerializeField]
    private Transform _player2Builds = null;
    /// <summary>
    /// Object to parent on for player 3
    /// </summary>
    [SerializeField]
    private Transform _player3Builds = null;
    /// <summary>
    /// Object to parent on for player 4
    /// </summary>
    [SerializeField]
    private Transform _player4Builds = null;

    protected override void PAwake()
    {        
        _player1Builds = GameObject.Find("Player_1_Builds").transform;
        //_player2Builds = GameObject.Find("Player_2_Builds").transform;
        //_player3Builds = GameObject.Find("Player_3_Builds").transform;
        //_player4Builds = GameObject.Find("Player_4_Builds").transform;
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
        
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(_currObj != null)
                _currObj.transform.GetComponent<Renderer>().material = _saveMaterial;
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
                _saveMaterial = _currObj.transform.GetComponent<Renderer>().material;
                _currObj.transform.GetComponent<Renderer>().material = LayMaterial;
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
                if(SnapOnGrid)
                    _currObj.transform.position = new Vector3(Mathf.Round(hit.point.x),hit.point.y,Mathf.Round(hit.point.z));
                else
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
