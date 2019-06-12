using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Player : BasePrefab
{
    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    public float MoveSpeed;
    public float BuildSpeed;
    public int HitAmount;
    public bool CompanionOut = false;
    public bool IsDead = false;

    [Range(1,4)]
    public int PlayerNumber = 1;

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

    public CastleStats CastleStats { get; set; }

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
    #endregion

    #region Private Members
    private GameObject _mainHand;
    private GameObject _offHand;    
    private Animator _anim;
    private bool _swinging = false;    
    private PlayerUI _playerUI;    
    private string _playerName;
    //Temporary for now
    private GenericPlans _plans;
    private OffensivePlans _oPlans;
    private PlacementController _placementController;
    #endregion

    


    protected override void Awake()
    {        
        LocalPlayerInstance = gameObject;
        
        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(gameObject);

        base.Awake();
        Inventory = GetComponent<Inventory>();        
        _anim = GetComponent<Animator>();      
        //Temporary for now
        _plans = GetComponent<GenericPlans>();
        _oPlans = GetComponent<OffensivePlans>();
        _placementController = GetComponent<PlacementController>();
    }

    // Start is called before the first frame update
    protected void Start()
    {
        var cameraWork = gameObject.GetComponent<CameraWork>();
        if (cameraWork != null)
        {
            cameraWork.OnStartFollowing();
        }
        else
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
        }
        
        SetBasicPlayerStats();

        _playerUI = UIManager.Instance.PlayerUIPanel;

        var castlemanger = CastleManager.Instance;
                
        //Set Nane
        _playerUI.PlayerName.text = "Krunchy";

        //NOTE
        //Quick test for two types of castles
        CastleType = PlayerNumber == 1 ? CastleType.Default : CastleType.FortressOfDoom;

        Castle castle = castlemanger.GetCastleByType(CastleType);               
        CastleManager.Instance.SpawnCastle(castle, this);     

        PlacementSpawn = transform.Find("PlacementSpawn");

        if (CompanionOut && CompanionType != CompanionType.None)
        {
            SetCompanion(CompanionType);
        }

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
        
    private void Update()
    {        
        //Temporary, work out the details for build mappings later
        MovementInput.Lock = IsDead;        

        if(Input.GetKeyDown(KeyCode.Q))
        {
            Global.BuildMode = !Global.BuildMode;
            
            if(Global.BuildMode)
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

        if(Input.GetKeyDown(KeyCode.F))
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

        //Cam Shaker
        if(Input.GetKeyDown(KeyCode.O))
        {
            CamShake.Shake();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            CamShake.Shake(1f, 0.5f);
        }


        if (Global.BuildMode)
        {
            //TODO: Need something to manage all the Plans in a Planmanager or something similar
            //The manager will handle both generic, and complex plans

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
            else if(Input.GetKeyDown(KeyCode.B))
            {
                _placementController.LoadObject(_oPlans.Barracks);
            }
        }        
        else if (Input.GetMouseButton(KeyBindings.LEFT_MOUSE_BUTTON) && !_swinging && !Global.BuildMode)
        {
            //TODO: I think I need something that checks for target on swing here later
            Debug.Log("Swining for attack");
            _swinging = true;
            //_anim.SetBool("Swing", true);
        }       
        
    }

    public void Init()
    {
        //Initialize and check everything on start of player
    }

    //These three methods probably need their own class
    public void SetCompanion(CompanionType companion)
    {
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
    }   

    public void Swing()
    {        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(!Inventory.ResourceCheck()) Inventory.ResetHands();        

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            
            if (hit.transform == null) return;
            if (!TransformHelper.DistanceLess(transform, hit.transform, Global.STRIKE_DIST)) return;
            
            switch (hit.transform.tag)
            {
                case "Resource":
                    var resource = hit.transform.GetComponent<IResource>();
                    int health = resource.GetHealth();                    
                    resource.SetHit(HitAmount);                    
                    break;
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
    
    public void SwingStop()
    {
        _swinging = false;
        _anim.SetBool("Swing", false);
    }    
  
    public override void SetHit(int amount)
    {
        //You're dead, go back
        if (Health <= 0 || IsDead) return;

        if (Health - amount > 0)
        {
            Health -= amount;
            _playerUI.HealthText.text = $"{Health}/100";
            UIManager.Instance.HealthBar.BarValue = Health;
            _anim.Play("Hit");
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
        _playerUI.HealthText.text = $"0/100";
        UIManager.Instance.HealthBar.BarValue = 0;
        Debug.Log("I am DEAD!");
        _anim.Play("Death1");        
    }

    
}
