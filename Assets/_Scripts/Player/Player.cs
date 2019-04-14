using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Player : BasePrefab
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


    protected override void Awake()
    {
        base.Awake();
        Inventory = GetComponent<Inventory>();        
        _anim = GetComponent<Animator>();                
        Castle = null;

        //Temporary for now
        _plans = GetComponent<GenericPlans>();
    }

    // Start is called before the first frame update
    protected void Start()
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
        //_playerUI.HealthBar.UpdateBar(50f, PlayerStats.Health);
        PlayerStats.MoveSpeed = 10f;
        PlayerStats.BuildSpeed = 10f;
        PlayerStats.HitAmount = 10;        
    }
        
    private void Update()
    {   
        //Temporary, work out the details for build mappings later

        if(Input.GetKeyDown(KeyCode.Q))
        {
            Global.BuildMode = !Global.BuildMode;
            
            if(Global.BuildMode)
            {
                //Annouce build mode on
                UIManager.Instance.Messages.text = "Build Mode ON";
                PlacementController.Instance.LoadObject(_plans.Plan1);
            }
            else
            {
                //Annouce build mode off
                UIManager.Instance.Messages.text = "Build Mode OFF";
                PlacementController.Instance.ClearObject();
            }
            
        }
        if (Global.BuildMode)
        {
            //TODO: Need something to manage all the Plans in a Planmanager or something similar
            //The manager will handle both generic, and complex plans

            if (Input.GetKeyDown(KeyBindings.BuildKey1))
            {
                PlacementController.Instance.LoadObject(_plans.Plan1);
            }
            else if (Input.GetKeyDown(KeyBindings.BuildKey2))
            {
                PlacementController.Instance.LoadObject(_plans.Plan2);
            }
            else if (Input.GetKeyDown(KeyBindings.BuildKey3))
            {
                PlacementController.Instance.LoadObject(_plans.Plan3);
            }          
        }        
        else if (Input.GetMouseButton(KeyBindings.LEFT_MOUSE_BUTTON) && !_swinging && !Global.BuildMode)
        {
            _swinging = true;
            _anim.SetBool("Swing", true);
        }
        
    }

    public void Init()
    {
        //Initialize and check everything on start of player


    }

    public void Swing()
    {        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == null) return;
            if (!TransformHelper.DistanceLess(transform, hit.transform, Global.STRIKE_DIST)) return;
            
            switch (hit.transform.tag)
            {
                case "Resource":
                    var resource = hit.transform.GetComponent<IResource>();
                    int health = resource.GetHealth();
                    resource.SetHit(PlayerStats.HitAmount);                    
                    break;
                case "Build":
                    var build = hit.transform.GetComponent<IBuild>();
                    build.SetHit(PlayerStats.HitAmount);
                    break;
                case "Player":                    
                case "Npc":
                    var character = hit.transform.GetComponent<ICharacter>();
                    character.SetHit(PlayerStats.HitAmount);                    
                    break;
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
