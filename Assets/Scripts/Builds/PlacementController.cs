﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SBK.Unity;

public enum BuildDirections
{
    North,
    East,
    South,
    West
}

public class PlacementController : PSingle<PlacementController>
{
    const int GROUND_LAYER = 8;

    public GameObject PlaceableObjectPrefab = null;
    public GameObject CameraRig;

    [Tooltip("This is the transparent lay material")]
    public Material LayMaterial;
    [Tooltip("This is the transparent lay material if you cannot build in a zone")]
    public Material ErrorMaterial;
    
    public bool BuildMode = false;
    public float RotateAmount = 45f;
    public bool SnapOnGrid = true;
    public float SnapSize = 1f;

    [SerializeField]
    private MeshRenderer _placeObjectMeshRend;

    private GameObject _currObj;
    private Material _saveMaterial;
    private InventoryUI _ui;

    private float mouseWheelRotation;
    private bool _triggerBuild = false;
    private CameraRotate _cam;


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
        _ui = UIManager.Instance.InventoryUIPanel;
        _player1Builds = GameObject.Find("Player_1_Builds").transform;
        //_player2Builds = GameObject.Find("Player_2_Builds").transform;
        //_player3Builds = GameObject.Find("Player_3_Builds").transform;
        //_player4Builds = GameObject.Find("Player_4_Builds").transform;
        _cam = CameraRig.GetComponent<CameraRotate>();
    }

    protected override void PDestroy()
    {
        
    }

    public void LoadObject(GameObject obj)
    {
        PlaceableObjectPrefab = obj;
        _placeObjectMeshRend = PlaceableObjectPrefab.GetComponentInChildren<MeshRenderer>();
        _triggerBuild = true;
    }
        
    void Update()
    {        
        var rotating = RotateFromMouseWheel();

        if (Input.GetMouseButtonDown(0))
        {
            if (_currObj != null)
            {
                _currObj.transform.GetComponentInChildren<Renderer>().material = _saveMaterial;
                var build = _currObj.transform.GetComponent<Build>();

                //For Testing 
                //Might need to move this down to the layprefab part so we can highlight it red
                //TODO: Come Back later
                var rt = ResourceType.Metal;
                bool cancelBuild = false;
                if (build.SetResourceType(rt))
                {
                    Inventory inv = ReturnPlayerInventory(0);
                    int invcount = inv.GetCount(rt);
                    if (invcount > 0 && (invcount - build.PlacementCost >= 0))
                    {
                        build.ConfirmPlacement();
                        inv.Set(rt, -build.PlacementCost);
                    }
                    else
                    {
                        cancelBuild = true;
                        _ui.Messages.text = $"You will need to gather more {rt.ToString()}";
                    }
                }
                else
                {
                    cancelBuild = true;
                    _ui.Messages.text = "That resource will not work to build this plan";
                }

                if(cancelBuild)
                {
                    Destroy(_currObj);
                }
                
            }
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
                _currObj = Instantiate(PlaceableObjectPrefab);
                _saveMaterial = _currObj.transform.GetComponentInChildren<Renderer>().material;
                _currObj.transform.GetComponentInChildren<Renderer>().material = LayMaterial;
                _currObj.transform.parent = _player1Builds; //sets the player 1 parent
            }

            _triggerBuild = false;
        }

        if (_currObj != null && !rotating)
        {
            _cam.FreezeCamera = false;
            BuildMode = true;
            MoveCurrentObjectToMouse();               
        }
    }   

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
               
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.gameObject.layer == GROUND_LAYER)
            {
                //float offset = hit.point.y + (_placeObjectMeshRend.bounds.min.y / 2);
                //_currObj.transform.position = new Vector3(hit.point.x, offset + 2, hit.point.z);                
                if(SnapOnGrid)
                    _currObj.transform.position = new Vector3(Mathf.Round(hit.point.x) * SnapSize, (hit.point.y + (_currObj.transform.localScale.y * 0.5f)) * SnapSize, Mathf.Round(hit.point.z) * SnapSize);
                else
                    _currObj.transform.position = hit.point;                
                
                //_currObj.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            }
        }
    }

    private void SnapToGround()
    {

    }

    private bool RotateFromMouseWheel()
    {
        if (_currObj == null) return false;
        _cam.FreezeCamera = true;
        float rotation = mouseWheelRotation + Input.mouseScrollDelta.y;

        if (rotation != mouseWheelRotation)
        {
            mouseWheelRotation = rotation;
            _currObj.transform.Rotate(Vector3.up, mouseWheelRotation * RotateAmount);
            return true;
        }

        return false;        
    }   

    private Inventory ReturnPlayerInventory(int playerPos)
    {
        return GameManager.Instance.GetPlayer(playerPos).Inventory;
    }    

}
