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
            MoveCurrentObjectToMouse();
        }
    }

    void Update()
    {
        _rotating = RotateFromMouseWheel();

        if (Input.GetMouseButtonDown(0) && PlaceableObjectPrefab != null)
        {
            if (_currObj != null)
            {
                _currObj.transform.GetComponentInChildren<Renderer>().material = _saveMaterial;
                _currObj.gameObject.layer = Global.DEFAULT_LAYER;
                _currObj.transform.GetComponent<Collider>().enabled = true;
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
            
            _currObj = Instantiate(PlaceableObjectPrefab);
            _saveMaterial = _currObj.transform.GetComponentInChildren<Renderer>().material;
            _currObj.transform.GetComponent<Collider>().enabled = false;
            _currObj.transform.GetComponentInChildren<Renderer>().material = LayMaterial;
            _currObj.transform.parent = _player1Builds; //sets the player 1 parent     
            _currObj.gameObject.layer = Global.IGNORE_LAYER;

            _triggerBuild = false;
        }       
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

    private Inventory ReturnPlayerInventory(int playerPos)
    {
        return GameManager.Instance.GetPlayer(playerPos).Inventory;
    }

}