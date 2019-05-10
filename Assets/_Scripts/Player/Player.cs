using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Player : BasePrefab
{    
    public float MoveSpeed { get; set; }
    public float BuildSpeed { get; set; }
    public int HitAmount { get; set; }
    public CastleStats CastleStats { get; set; }

    public bool IsDead = false;

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

    public Transform PlacementSpawn { get; set; }

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
    private OffensivePlans _oPlans;


    protected override void Awake()
    {
        base.Awake();
        Inventory = GetComponent<Inventory>();        
        _anim = GetComponent<Animator>();                
        Castle = null;

        //Temporary for now
        _plans = GetComponent<GenericPlans>();
        _oPlans = GetComponent<OffensivePlans>();

        PlacementController.Instance.MyPlayer = this;
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
            Castle.CastleOwner = this;
        }
        
        CastleManager.Instance.SpawnCastle(Castle, Castle.CastleOwner);        

        PlacementSpawn = transform.Find("PlacementSpawn");
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
            //_swinging = true;
            //_anim.SetBool("Swing", true);
        }
        
    }

    public void Init()
    {
        //Initialize and check everything on start of player


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
