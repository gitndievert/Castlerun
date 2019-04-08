using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public PlayerStats PlayerStats;

    private string _playerName;

    public string PlayerName
    {
        get { return _playerName; }
        set
        {
            if (value.Length > 20)
                _playerName = value.Substring(0, 20);
            else
                _playerName = value;
        }
    }

    public int PlayerNumber;

    [Tooltip("For Testing Changes on Castles")]
    public CastleType CastleType = CastleType.None;

    #region Player Components
    public Companion Companion { get; private set; }
    public Inventory Inventory { get; private set; }
    public int ActorNumber { get; internal set; }    
    public Castle Castle { get; private set; }    
    #endregion

    private GameObject _mainHand;
    private GameObject _offHand;    
    private Animator _anim;
    private bool _swinging = false;    
    private PlayerUI _playerUI;

    //Temporary for now
    private GenericPlans _plans;


    private void Awake()
    {
        Inventory = GetComponent<Inventory>();        
        _anim = GetComponent<Animator>();                
        Castle = null;

        //Temporary for now
        _plans = GetComponent<GenericPlans>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        SetBasicPlayerStats();

        _playerUI = UIManager.Instance.PlayerUIPanel;

        var castlemanger = CastleManager.Instance;
                
        //Set Nane
        _playerUI.PlayerName.text = "Krunchy";

        //Set the castle Properties
        if (Castle == null)
        {
            //var castleType = CastleType == CastleType.None ? CastleType.Castle3 : CastleType;

            Castle = castlemanger.GetCastleByType(CastleType);
            Castle.Level = 2;
            Castle.Experience = 4050f;
            Castle.CastleOwner = this;
        }
        
        CastleManager.Instance.SpawnCastle(Castle, Castle.CastleOwner);
        _playerUI.CastleLevel.text = Castle.Level.ToString();
    }

    private void SetBasicPlayerStats()
    {
        PlayerStats.Health = 100;
        PlayerStats.MoveSpeed = 10f;
        PlayerStats.BuildSpeed = 10f;
        PlayerStats.HitAmount = 10;        
    }
        
    private void Update()
    {
        //Temporary, work out the details for build mappings later
        if(Input.GetKeyDown(KeyBindings.BuildKey1))
        {
            PlacementController.Instance.LoadObject(_plans.Plan1);
        }
        else if(Input.GetKeyDown(KeyBindings.BuildKey2))
        {
            PlacementController.Instance.LoadObject(_plans.Plan2);
        }
        else if (Input.GetKeyDown(KeyBindings.BuildKey3))
        {
            PlacementController.Instance.LoadObject(_plans.Plan3);
        }
        else if (Input.GetKeyDown(KeyBindings.BuildKey4))
        {
            PlacementController.Instance.LoadObject(_plans.Plan4);
        }
        else
        {
            if (Input.GetMouseButton(KeyBindings.LEFT_MOUSE_BUTTON) && !_swinging)
            {                                
                _swinging = true;
                _anim.SetBool("Swing", true);                
            }
        }

        
    }

    private void Init()
    {
        //Initialize and check everything on start of player


    }

    public void Swing()
    {        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform != null && hit.transform.tag == "Resource")
            {

                Debug.Log(TransformHelper.DistanceCheck(hit.transform, transform));

                if (TransformHelper.DistanceLess(transform, hit.transform, Inventory.HARVEST_DISTANCE))
                {

                    var resource = hit.transform.GetComponent<IResource>();
                    int health = resource.GetHealth();
                    resource.SetHit(PlayerStats.HitAmount);
                    resource.PlayHitSounds();

                    //To Debug things for a resource type
                    //var rt = resource.GetResourceType();
                    //Debug.Log(durability + " " + rt.ToString());

                }
            }
        }
    }        
    
    public void SwingStop()
    {
        _swinging = false;
        _anim.SetBool("Swing", false);
    }

    
    private void OnDestroy()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        
    }

}
