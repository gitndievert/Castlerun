using System.Collections;
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
                _currObj.transform.parent = MyPlayer.transform;
                // ModeCurrentObjectOnScreen();
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
                _currObj.transform.GetComponent<Collider>().enabled = true;
                _currObj.transform.parent = _player1Builds; //sets the player 1 parent  
                var build = _currObj.transform.GetComponent<Build>();

                //For Testing 
                //Might need to move this down to the layprefab part so we can highlight it red
                //TODO: Come Back later
                var rt = ResourceType.Metal;
                bool cancelBuild = false;
                if (build.SetResourceType(rt))
                {
                    Inventory inv = ReturnPlayerInventory();
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
            _currObj.transform.GetComponent<Collider>().enabled = false;
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

    private void ModeCurrentObjectOnScreen()
    {
        Vector3 playerPos = MyPlayer.transform.position;
        Vector3 playerDirection = MyPlayer.transform.forward;
        Quaternion playerRotation = MyPlayer.transform.rotation;

        Vector3 posOffset = playerPos + playerDirection * 10f; //the spawn distance

        _placeObjectMeshRend.transform.position = new Vector3(Camera.main.transform.forward.x, playerPos.y, posOffset.z);
        //_placeObjectMeshRend.transform.position = new Vector3(playerPos.x, playerPos.y, posOffset.z);
        _placeObjectMeshRend.transform.rotation = playerRotation;

        
        /*
            if (SnapOnGrid)
                    _currObj.transform.position = new Vector3(Mathf.Round(hit.point.x) * SnapSize, distToGround, Mathf.Round(hit.point.z) * SnapSize);
                else
                    _currObj.transform.position = hit.point;

                //_currObj.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
           
        }*/
    }
 
    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distToGround = _placeObjectMeshRend.bounds.extents.y;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.gameObject.layer == Global.GROUND_LAYER)
            {
                //Debug.Log("Offset " + _placeObjectMeshRend.bounds.min.y / 2);
                //Debug.Log("Local " + _currObj.transform.localPosition.y);

                //float offset = hit.point.y + (_placeObjectMeshRend.bounds.min.y / 2);

                //_currObj.transform.position = new Vector3(hit.point.x, offset + 2, hit.point.z);   
                //Debug.Log(hit.point);                

                if (SnapOnGrid)
                    _currObj.transform.position = new Vector3(Mathf.Round(hit.point.x) * SnapSize, hit.point.y + distToGround, Mathf.Round(hit.point.z) * SnapSize);
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
        float rotation = mouseWheelRotation + Input.mouseScrollDelta.y;

        if (rotation != mouseWheelRotation)
        {
            mouseWheelRotation = rotation;
            _currObj.transform.Rotate(Vector3.down, mouseWheelRotation * RotateAmount);
            return true;
        }

        return false;
    }

    private Inventory ReturnPlayerInventory()
    {
        return MyPlayer.Inventory;
    }

    private void ChangeMaterial()
    {
        /*void ChangeMaterial(Material newMat)
        {
            Renderer[] children;
            children = GetComponentsInChildren<Renderer>();
            foreach (Renderer rend in children)
            {
                var mats = new Material[rend.materials.Length];
                for (var j = 0; j < rend.materials.Length; j++)
                {
                    mats[j] = newMat;
                }
                rend.materials = mats;
            }
        }*/
    }

}