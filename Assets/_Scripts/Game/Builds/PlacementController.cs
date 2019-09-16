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

using System.Collections;
using UnityEngine;

public class PlacementController : MonoBehaviour
{
    [Tooltip("Loaded Prefab for Placement")]
    public GameObject PlaceableObjectPrefab = null;    

    [Header("Materials for Placements")]
    [Tooltip("This is the transparent lay material")]
    public Material LayMaterial;
    [Tooltip("This is the transparent lay material if you cannot build in a zone")]
    public Material ErrorMaterial;

    [Header("Build Properties")]
    public bool BuildMode;
    public float RotateAmount = 90f;    
    public float SnapSize = 1f;        

    public static ResourceType[] ResourceIndex = {
        ResourceType.Wood,
        ResourceType.Rock,
        ResourceType.Metal
    };

    [SerializeField]
    private MeshRenderer _placeObjectMeshRend;

    //TODO MOVE TO QUEUES
    private GameObject _currObj;
    private GameObject _buildingObj;

    private Material[] _saveMaterial;
    //private ResourceType _selectedResource;

    private float mouseWheelRotation;
    private bool _triggerBuild = false;
    private bool _rotating = false;
    private Player _player;    
    private bool _outsideGrid;        

    /// <summary>
    /// Object to parent on for player
    /// </summary>
    [SerializeField]
    private Transform _playerBuilds = null;    

    private void Awake()
    {
        _player = GetComponent<Player>();        
    }

    private void Start()
    {
        BuildMode = false;        
        _playerBuilds = _player.PlayerWorldItems.transform;        
    }

    public void LoadObject(GameObject obj, bool outsidegrid = false)
    {       
        PlaceableObjectPrefab = obj;
        _placeObjectMeshRend = PlaceableObjectPrefab.GetComponentInChildren<MeshRenderer>();        
        _triggerBuild = true;
        _outsideGrid = outsidegrid;        
        CameraRotate.BuildCamMode = outsidegrid;
    }    
        
    public void ClearObject()
    {
        KillBuild();
        _triggerBuild = false;
        PlaceableObjectPrefab = null;        
    }

    private void KillBuild()
    {
        Destroy(_currObj);        
    }  

    public bool SetGrid
    {
        set
        {                
            string status = value ? "ON" : "OFF";
            UIManager.Instance.Messages.text = $"Build Mode {status}";            
        }
    }
    
    void Update()
    {
        //Hide 
        if (_currObj != null)
        { 
            if (Global.MouseLook)
            {
                _currObj.SetActive(false);
            }
            else if (!Global.MouseLook && !_currObj.activeInHierarchy)
            {
                _currObj.SetActive(true);
            }

            if (!_rotating)
            {
                BuildMode = true;                
                MoveCurrentObjectToMouse();                
            }
            else
            {
                BuildMode = false;
            }
        }

        //Don't allow rotations on non-basics
        if (!_outsideGrid)
            _rotating = RotateFromMouseWheel();

        LockCursorPos();

        if (Input.GetMouseButtonDown(KeyBindings.LEFT_MOUSE_BUTTON) && PlaceableObjectPrefab != null)
        {
            if (_currObj != null)
            {
                if (Vector3.Distance(_currObj.transform.position, _player.transform.position) > 20f && !_outsideGrid) return;

                var build = _currObj.transform.GetComponent<IBuild>();
                
                
                bool cancelBuild = false;
                Inventory inv = _player.Inventory;

                bool metCosts = false;
                var costs = build.GetCosts();
                foreach (var cost in costs.CostFactors)
                {
                    int invcount = inv.GetCount(cost.Resource);
                    metCosts = invcount > 0 && (invcount - cost.Amount >= 0);
                }

                if (metCosts)
                {
                    //Confirm that placement can be made on build
                    if (!build.ConfirmPlacement())
                    {
                        cancelBuild = true;
                        UIManager.Instance.Messages.text = "You cannot build here, try again";
                    }
                    else
                    {
                        inv.Set(costs);
                        StartCoroutine(CorLoadBuilding(build));
                    }
                }
                else
                {
                    cancelBuild = true;
                    _currObj.transform.GetComponentInChildren<Renderer>().material = ErrorMaterial;
                    //Come Back... Get Messages for Resource Types
                    //UIManager.Instance.Messages.text = $"You will need to gather more {rt.ToString()}";
                    UIManager.Instance.Messages.text = "You will need to gather more Resources";
                }                
                

                if (cancelBuild)
                {
                    Destroy(_currObj);
                }

            }
                        
            _currObj = null;
            LoadObject(PlaceableObjectPrefab, _outsideGrid);
            return;
        }

        if (_triggerBuild)
        {
            if (_currObj != null) KillBuild();

            _currObj = Instantiate(PlaceableObjectPrefab,_player.transform.position * 2,Quaternion.identity);            
            _saveMaterial = _currObj.transform.GetComponentInChildren<Renderer>().materials;
                                    
            PlayerCollision(true);

            //_currObj.transform.GetComponentInChildren<Renderer>().material = LayMaterial;
            var mats = _currObj.transform.GetComponentInChildren<Renderer>().materials;
            Material[] laymats = new Material[mats.Length];
            for(int i = 0; i < mats.Length; i++)
            {
                laymats[i] = LayMaterial;
            }
            _currObj.transform.GetComponentInChildren<Renderer>().materials = laymats;               
            _currObj.gameObject.layer = Global.IGNORE_LAYER;                      

            _triggerBuild = false;
        }       
    }

    private IEnumerator CorLoadBuilding(IBuild build)
    {
        build.SetPlayer(_player);
        _buildingObj = _currObj;
        _buildingObj.transform.parent = _playerBuilds; //sets the player 1 parent          
        _buildingObj.gameObject.layer = Global.DEFAULT_LAYER;
        yield return new WaitForSeconds(build.GetConstructionTime());
        PlayerCollision(false);
        _buildingObj.transform.GetComponentInChildren<Renderer>().materials = _saveMaterial;                        
        yield return null;
    }

    private void PlayerCollision(bool collide)
    {
        if (_currObj == null) return;
        Physics.IgnoreCollision(_currObj.GetComponent<Collider>(), _player.GetComponent<Collider>(), collide);
    }

    private float GetDistToGround()
    {
        return (_placeObjectMeshRend != null) ? _placeObjectMeshRend.bounds.extents.y : 0f;
    }

    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }   
    
    private void LockCursorPos()
    {
        if (_currObj == null || _outsideGrid) return;
        if (Vector3.Distance(_currObj.transform.position, _player.transform.position) > 15f)
        {
            Vector3 playerP = _player.transform.position;
            _currObj.transform.position = new Vector3(playerP.x,playerP.y + GetDistToGround(), playerP.z + 3f);
            //_currObj.transform.position = _currObj.transform.position + Vector3.back;
        }
    }   
 
    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.gameObject.layer == Global.GROUND_LAYER)
            {
                //Debug.Log($"Hit point y {hit.point.y}");
                //Debug.Log($"Dist to ground{GetDistToGround()}");

                //EXPIRIMENT - Get render bounds from mesh rather than collider
                float halfHeight = _currObj.GetComponent<Collider>().bounds.size.y / 2;
                //Don't need to put in the halfheight
                //float halfHeight = 0f;

                _currObj.transform.position = new Vector3(Mathf.Round(hit.point.x) * SnapSize, Mathf.Round(hit.point.y) + halfHeight, Mathf.Round(hit.point.z) * SnapSize);
            }
        }
    }
  
    private bool RotateFromMouseWheel()
    {
        if (_currObj == null) return false;
        float rotation = mouseWheelRotation + Input.mouseScrollDelta.y;

        if (rotation != mouseWheelRotation)
        {
            mouseWheelRotation = rotation;
            _currObj.transform.Rotate(Vector3.down, mouseWheelRotation * RotateAmount);
            return true;
        }

        return false;
    }   
   

}