using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SBK.Unity;

public class PlacementController : PSingle<PlacementController>
{
    public GameObject PlaceableObjectPrefab = null;
    public Player MyPlayer;

    [Tooltip("This is the transparent lay material")]
    public Material LayMaterial;
    [Tooltip("This is the transparent lay material if you cannot build in a zone")]
    public Material ErrorMaterial;

    [Header("Build Properties")]
    public bool BuildMode = false;
    public float RotateAmount = 45f;
    public bool SnapOnGrid = true;
    public float SnapSize = 1f;    
    public bool MoveOnMouse = false;

    [SerializeField]
    private MeshRenderer _placeObjectMeshRend;

    private GameObject _currObj;    
    private Material[] _saveMaterial;    

    private float mouseWheelRotation;
    private bool _triggerBuild = false;
    private bool _rotating = false;

    /// <summary>
    /// Object to parent on for player 1
    /// </summary>
    [SerializeField]
    private Transform _playerBuilds = null;

    private bool _triggerPlacement = false;

    protected override void PAwake()
    {        
        _playerBuilds = GameObject.Find("Player_Builds").transform;
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

    public void ClearObject()
    {        
        Destroy(_currObj);
        _triggerBuild = false;
        PlaceableObjectPrefab = null;
    }

    private void FixedUpdate()
    {
        if (_currObj != null && !_rotating)
        {            
            BuildMode = true;
            
            if (MoveOnMouse)
                MoveCurrentObjectToMouse();
            else
            {
                //_currObj.transform.parent = MyPlayer.PlacementSpawn.transform;
                //ModeCurrentObjectOnScreen();
                //PlaceObjectToGround();
            }
                
        }
    }

    void Update()
    {
        _rotating = RotateFromMouseWheel();

        if (Input.GetMouseButtonDown(0) && PlaceableObjectPrefab != null)
        {
            if (_currObj != null)
            {
                _currObj.transform.GetComponentInChildren<Renderer>().materials = _saveMaterial;
                _currObj.gameObject.layer = Global.DEFAULT_LAYER;
                _currObj.transform.GetComponent<Collider>().isTrigger = false;
                _currObj.transform.parent = _playerBuilds; //sets the player 1 parent  
                var build = _currObj.transform.GetComponent<Build>();
                                
                //Might need to move this down to the layprefab part so we can highlight it red
                //TODO: Come Back later
                var rt = ResourceType.Metal;
                bool cancelBuild = false;
                if (build.SetResourceType(rt))
                {
                    Inventory inv = MyPlayer.Inventory;
                    int invcount = inv.GetCount(rt);
                    if (invcount > 0 && (invcount - build.PlacementCost >= 0))
                    {
                        build.ConfirmPlacement();
                        inv.Set(rt, -build.PlacementCost);
                    }
                    else
                    {
                        cancelBuild = true;
                        _currObj.transform.GetComponentInChildren<Renderer>().material = ErrorMaterial;
                        UIManager.Instance.Messages.text = $"You will need to gather more {rt.ToString()}";
                    }
                }
                else
                {
                    cancelBuild = true;
                    UIManager.Instance.Messages.text = "That resource will not work to build this plan";
                }

                if (cancelBuild)
                {
                    Destroy(_currObj);
                }

            }
            _currObj = null;
            BuildMode = false;
            LoadObject(PlaceableObjectPrefab);
            return;
        }

        if (_triggerBuild)
        {
            if (_currObj != null)
            {
                Destroy(_currObj);
            }

            _currObj = Instantiate(PlaceableObjectPrefab,MyPlayer.transform.position,Quaternion.identity);            
            _saveMaterial = _currObj.transform.GetComponentInChildren<Renderer>().materials;
            _currObj.transform.GetComponent<Collider>().isTrigger = true;
            

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

    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }    
 
    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distToGround = _placeObjectMeshRend.bounds.extents.y;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.gameObject.layer == Global.GROUND_LAYER)
            {
                if (SnapOnGrid)
                    _currObj.transform.position = new Vector3(Mathf.Round(hit.point.x) * SnapSize, hit.point.y + distToGround, Mathf.Round(hit.point.z) * SnapSize);
                else
                    _currObj.transform.position = hit.point;
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