using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SBK.Unity;

public class PlacementController : MonoBehaviour
{
    public GameObject PlaceableObjectPrefab = null;    

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

    private GameObject _currObj;    
    private Material[] _saveMaterial;
    private ResourceType _selectedResource;

    private float mouseWheelRotation;
    private bool _triggerBuild = false;
    private bool _rotating = false;
    private Player _player;
    private GameObject _grid;

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
        _grid = _player.transform.Find("Grid").gameObject;
        _playerBuilds = _player.PlayerWorldItems.transform;
        SetResource(ResourceType.Wood);
    }

    public void LoadObject(GameObject obj)
    {        
        PlaceableObjectPrefab = obj;
        _placeObjectMeshRend = PlaceableObjectPrefab.GetComponentInChildren<MeshRenderer>();        
        _triggerBuild = true;
    }

    public void SetResource(ResourceType resource)
    {
        _selectedResource = resource;
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

    private Transform[] GetResourcePlacements()
    {
        if (_player == null) return null;
        return _player.PlayerPad.ResourcePoints;
    }

    public bool SetGrid
    {
        set
        {
            if (_grid != null)
            {
                _grid.SetActive(value);
                string status = _grid.activeSelf ? "ON" : "OFF";
                UIManager.Instance.Messages.text = $"Build Mode {status}";
            }
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

                if (!_currObj.GetComponent<IBuild>().Locked)
                {
                    MoveCurrentObjectToMouse();
                }
            }
            else
            {
                BuildMode = false;
            }
        }       

        _rotating = RotateFromMouseWheel();
        LockCursorPos();

        if (Input.GetMouseButtonDown(KeyBindings.LEFT_MOUSE_BUTTON) && PlaceableObjectPrefab != null)
        {
            if (_currObj != null)
            {
                if (Vector3.Distance(_currObj.transform.position, _player.transform.position) > 20f) return;

                PlayerCollision(false);
                _currObj.transform.GetComponentInChildren<Renderer>().materials = _saveMaterial;
                _currObj.gameObject.layer = Global.DEFAULT_LAYER;

                _currObj.transform.parent = _playerBuilds; //sets the player 1 parent  
                var build = _currObj.transform.GetComponent<IBuild>();
                
                ResourceType rt = _selectedResource;

                bool cancelBuild = false;

                if (build.SetResourceType(rt))
                {                    
                    Inventory inv = _player.Inventory;
                    int invcount = inv.GetCount(rt);
                    if (invcount > 0 && (invcount - build.PlacementCost >= 0))
                    {
                        build.SetPlayer(_player);
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
            LoadObject(PlaceableObjectPrefab);
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
        if (_currObj == null) return;
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
                Debug.Log($"Hit range {hit.point.y}");
                Debug.Log(GetDistToGround());
                _currObj.transform.position = new Vector3(Mathf.Round(hit.point.x) + SnapSize, hit.point.y + GetDistToGround(), Mathf.Round(hit.point.z) + SnapSize);
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