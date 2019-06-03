using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Player : BasePrefab, IPunObservable
{
    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    public float MoveSpeed { get; set; }
    public float BuildSpeed { get; set; }
    public int HitAmount { get; set; }
    public CastleStats CastleStats { get; set; }

    public bool CompanionOut = false;
    public bool IsDead = false;
    public int PlayerNumber;

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
    #endregion

    //This is the network sync
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            //stream.SendNext(IsFiring);
            //stream.SendNext(Health);
        }
        else
        {
            // Network player, receive data
            //this.IsFiring = (bool)stream.ReceiveNext();
            //this.Health = (float)stream.ReceiveNext();
        }
    }

#if UNITY_5_4_OR_NEWER
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
    {
        CalledOnLevelWasLoaded(scene.buildIndex);
    }
#endif

#if UNITY_5_4_OR_NEWER
    public override void OnDisable()
    {
        // Always call the base to remove callbacks
        base.OnDisable();
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }
#endif

    protected override void Awake()
    {
        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
        }
        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(gameObject);

        base.Awake();
        Inventory = GetComponent<Inventory>();        
        _anim = GetComponent<Animator>();        

        //Temporary for now
        _plans = GetComponent<GenericPlans>();
        _oPlans = GetComponent<OffensivePlans>();

        PlacementController.Instance.MyPlayer = this;        
    }

    // Start is called before the first frame update
    protected void Start()
    {

        CameraWork _cameraWork = gameObject.GetComponent<CameraWork>();

        if (_cameraWork != null)
        {
            if (photonView.IsMine)
            {
                _cameraWork.OnStartFollowing();
            }
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
        //Pulls the first castle out for now (just one castle)
        Castle castle = castlemanger.GetCastleByType(CastleType);               
        CastleManager.Instance.SpawnCastle(castle, this);     

        PlacementSpawn = transform.Find("PlacementSpawn");

        if (CompanionOut && CompanionType != CompanionType.None)
        {
            SetCompanion(CompanionType);
        }

#if UNITY_5_4_OR_NEWER
        // Unity 5.4 has a new scene management. register a method to call CalledOnLevelWasLoaded.
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
#endif
    }

    #if !UNITY_5_4_OR_NEWER
    /// <summary>See CalledOnLevelWasLoaded. Outdated in Unity 5.4.</summary>
    void OnLevelWasLoaded(int level)
    {
        this.CalledOnLevelWasLoaded(level);
    }
    #endif


    void CalledOnLevelWasLoaded(int level)
    {
        // check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
        if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
        {
            transform.position = new Vector3(0f, 5f, 0f);
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
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true) return;
        //Temporary, work out the details for build mappings later
        MovementInput.Lock = IsDead;        

        if(Input.GetKeyDown(KeyCode.Q))
        {
            Global.BuildMode = !Global.BuildMode;
            
            if(Global.BuildMode)
            {
                //Annouce build mode on
                UIManager.Instance.Messages.text = "Build Mode ON";
                PlacementController.Instance.LoadObject(_plans.Wall);
            }
            else
            {
                //Annouce build mode off
                UIManager.Instance.Messages.text = "Build Mode OFF";
                PlacementController.Instance.ClearObject();
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

        if (Global.BuildMode)
        {
            //TODO: Need something to manage all the Plans in a Planmanager or something similar
            //The manager will handle both generic, and complex plans

            if (Input.GetKeyDown(KeyBindings.BuildKey1))
            {
                PlacementController.Instance.LoadObject(_plans.Wall);
            }
            else if (Input.GetKeyDown(KeyBindings.BuildKey2))
            {
                PlacementController.Instance.LoadObject(_plans.Floor);
            }
            else if (Input.GetKeyDown(KeyBindings.BuildKey3))
            {
                PlacementController.Instance.LoadObject(_plans.Ramp);
            }  
            else if(Input.GetKeyDown(KeyCode.B))
            {
                PlacementController.Instance.LoadObject(_oPlans.Barracks);
            }
        }        
        else if (Input.GetMouseButton(KeyBindings.LEFT_MOUSE_BUTTON) && !_swinging && !Global.BuildMode)
        {
            //TODO: I think I need something that checks for target on swing here later
            Debug.Log("Swining for attack");
            _swinging = true;
            _anim.SetBool("Swing", true);
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

    
    private void OnDestroy()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {

        
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
