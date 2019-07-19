using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Player : BasePrefab
{
    public float MoveSpeed;
    public float BuildSpeed;
    public int HitAmount;
    public bool CompanionOut = false;
    public bool IsDead = false;    

    [Tooltip("For changing the companion on the player")]
    public CompanionType CompanionType = CompanionType.None;
    [Tooltip("For Testing Changes on Castles")]
    public CastleType CastleType = CastleType.None;

    /// <summary>
    /// Returns the current companion of the player
    /// </summary>
    public Companion PlayerCompanion { get; set; }
    /// <summary>
    /// Returns the current castle being used by the player
    /// </summary>
    public Castle PlayerCastle { get; set; }

    /// <summary>
    /// Placement tranform for the player spawn coords
    /// </summary>
    public Transform PlacementSpawn { get; set; }

    [Range(1, 4)]
    public int PlayerNumber = 1;

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
   
    #region Player Components      
    public Inventory Inventory { get; private set; }    
    public int ActorNumber { get; internal set; }
    public GameObject PlayerWorldItems { get; internal set; }
    #endregion

    #region Private Members
    private GameObject _mainHand;
    private GameObject _offHand;                 
    private string _playerName;
    private BattleCursor _battleCursor;
    //Temporary for now
    private GenericPlans _plans;
    private OffensivePlans _oPlans;
    private PlacementController _placementController;
    private MovementInput _movement;

    private int _resourceIndex = 0;
    private ResourceType _selectedResource;

    //Camera Rig
    private GameObject _camRig;
    #endregion
    

    protected override void Awake()
    {   
        base.Awake();
        Inventory = GetComponent<Inventory>();                    
        //Temporary for now
        _plans = GetComponent<GenericPlans>();
        _oPlans = GetComponent<OffensivePlans>();
        _placementController = GetComponent<PlacementController>();
        _camRig = GameObject.FindGameObjectWithTag(Global.CAM_RIG_TAG);
        _movement = GetComponent<MovementInput>();
        PlayerWorldItems = new GameObject("PlayerWorldItems");
        _battleCursor = GetComponent<BattleCursor>();
    }

    // Start is called before the first frame update
    protected void Start()
    {        
        PlayerUI.PlayerName.text = _playerName;

        //Initialize the object dumps for the world       
        
        //Set player camera
        _camRig.GetComponent<CameraRotate>().target = transform;

        //Player stats
        SetBasicPlayerStats();        

        //NOTE
        //Quick test for two types of castles        
        CastleType = PlayerNumber == 1 ? CastleType.Default : CastleType.FortressOfDoom;
        CastleManager.Instance.SpawnCastle(CastleType, this);     

        PlacementSpawn = transform.Find("PlacementSpawn");

        if (CompanionOut && CompanionType != CompanionType.None)
        {
            SetCompanion(CompanionType);
        }

    }

    public void Init(string name, int playernum)
    {
        PlayerName = name;
        PlayerNumber = playernum;
        Global.BattleMode = false;
    }
 
    private void SetBasicPlayerStats()
    {
        Health = 100;
        
        //Figure out later        
        UIManager.Instance.HealthBar.BarValue = 100f;
        UIManager.Instance.StaminaBar.BarValue = 100f;

        MoveSpeed = 10f;
        BuildSpeed = 10f;
        HitAmount = 10;        
    }

    private ResourceType SwitchResource()
    {       
        if (_resourceIndex >= PlacementController.ResourceIndex.Length - 1)
        {
            _resourceIndex = 0;
        }
        else
        {
            _resourceIndex++;
        }
        
        _selectedResource = PlacementController.ResourceIndex[_resourceIndex];

        return _selectedResource;
    }
        
    private void Update()
    {        
        //Temporary, work out the details for build mappings later
        MovementInput.Lock = IsDead;

        if (Input.GetMouseButton(KeyBindings.LEFT_MOUSE_BUTTON) && !Global.BuildMode)
        {
            _movement.Swing();
        }

        if (Input.GetKeyDown(KeyBindings.BattleToggle))
        {
            if (Global.BuildMode)
            {
                UIManager.Instance.Messages.text = $"Cannot engage battlemode in build mode";
            }
            else
            {
                Global.BattleMode = !Global.BattleMode;
                _battleCursor.Toggle();
                string status = Global.BattleMode ? "ON" : "OFF";
                UIManager.Instance.Messages.text = $"Battle Mode {status}";
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Global.BuildMode = !Global.BuildMode;
                _battleCursor.Off();

                if (Global.BuildMode)
                {
                    //Annouce build mode on                
                    _placementController.SetGrid = true;
                    _placementController.LoadObject(_plans.Wall);
                }
                else
                {
                    //Annouce build mode off                
                    _placementController.SetGrid = false;
                    _placementController.ClearObject();
                }

            }

            if (Global.BuildMode)
            {
                //TODO: Need something to manage all the Plans in a Planmanager or something similar
                //The manager will handle both generic, and complex plans

                if (Input.GetKeyDown(KeyCode.E))
                {
                    _selectedResource = SwitchResource();
                    _placementController.SetResource(_selectedResource);
                    UIManager.Instance.Messages.text = $"Building with {_selectedResource.ToString()}";
                }

                if (Input.GetKeyDown(KeyBindings.BuildKey1))
                {
                    _placementController.LoadObject(_plans.Wall);
                }
                else if (Input.GetKeyDown(KeyBindings.BuildKey2))
                {
                    _placementController.LoadObject(_plans.Floor);
                }
                else if (Input.GetKeyDown(KeyBindings.BuildKey3))
                {
                    _placementController.LoadObject(_plans.Ramp);
                }
                else if (Input.GetKeyDown(KeyCode.B))
                {
                    _placementController.LoadObject(_oPlans.Barracks);
                }
            }
        }
                       

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!IsDead)
            {
                Debug.Log("Doing damage to player!");
                SetHit(HitAmount);
            }
        }

        //Companion Test
        if(Input.GetKeyDown(KeyCode.C))
        {
            SetCompanion(CompanionType.Fox);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            SetCompanion(CompanionType.Fox_S);
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            ReleaseCompanion();
        }

        //Cam Shaker
        if(Input.GetKeyDown(KeyCode.O))
        {
            CamShake.Shake();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            CamShake.Shake(1f, 0.5f);
        }    
        
    }   

    //These three methods probably need their own class
    public void SetCompanion(CompanionType companion)
    {
        CompanionOut = true;
        ReleaseCompanion();
        var mycompanion = Instantiate(CompanionManager.Instance.GetCompanionByType(companion),transform.position,transform.rotation);        
        PlayerCompanion = mycompanion.GetComponent<Companion>();
        GetComponent<MovementInput>().SetPlayerCompanion = PlayerCompanion;
        PlayerCompanion.transform.parent = transform;
        PlayerCompanion.transform.position = transform.position + (Vector3.right * 1.5f);
    }

    public void ReleaseCompanion()
    {
        if (PlayerCompanion == null) return;
        Destroy(PlayerCompanion.gameObject);
        GetComponent<MovementInput>().SetPlayerCompanion = null;
        PlayerCompanion = null;
        CompanionOut = false;
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
                case "Build":
                    var build = hit.transform.GetComponent<IBuild>();
                    build.SetHit(HitAmount);                    
                    break;
                case "Player":                    
                case "Npc":
                    var character = hit.transform.GetComponent<ICharacter>();
                    character.SetHit(HitAmount);                    
                    break;
            }            
        }
    }        
       
  
    public override void SetHit(int amount)
    {
        //You're dead, go back
        if (Health <= 0 || IsDead) return;

        if (Health - amount > 0)
        {
            Health -= amount;
            PlayerUI.HealthText.text = $"{Health}/100";
            UIManager.Instance.HealthBar.BarValue = Health;
            _movement.Hit();
        }
        else
        {
            IsDead = true;
            Die();            
        }
    }

    public void Die()
    {
        Health = 0;
        PlayerUI.HealthText.text = $"0/100";
        UIManager.Instance.HealthBar.BarValue = 0;
        Debug.Log("I am DEAD!");
        _movement.Die();
    }

    
}
