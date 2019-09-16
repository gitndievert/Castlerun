// ********************************************************************
// CONFIDENTIAL - DO NOT DISTRIBUTE
// COPYRIGHT 2019-2020 Wacky Potato Games, LLC. All Rights Reserved.
// 
// If you send, receive, or use this file for any purpose other than
// internal use by Wacky Potato Games, it is without permission and an act of theft.
// Report any misuse of this file immediately to contact@wackypotato.com
// Misuse or failure to report misuse will subject you to legal action.
// 
// The intellectual and technical concepts contained herein are
// proprietary and are protected by trade secret and/or copyright law.
// Dissemination or reproduction of this material is forbidden.
// ********************************************************************

using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Player : BasePrefab, IPlayer
{
    [Header("Basic Player Properties")]
    public float MoveSpeed;
    public float BuildSpeed;
    public int HitAmount = 50;
    public bool CompanionOut = false;
    public bool IsDead = false;

    [Header("Custom Additions to Player")]
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
    /// Placement Pad for this player
    /// </summary>
    public PlayerPad PlayerPad { get; set; }

    [Range(1, Global.PLAYER_MAX_SLOTS)]
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
    private Plans _plans;
    
    
    private PlacementController _placementController;
    private MovementInput _movement;
       

    //Camera Rig
    private GameObject _camRig;
    #endregion
    

    protected override void Awake()
    {   
        base.Awake();
        Inventory = GetComponent<Inventory>();                    
                
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
        //CastleType = PlayerNumber == 1 ? CastleType.Default : CastleType.FortressOfDoom;
        CastleManager.Instance.SpawnCastle(CastleType.FortressOfDoom, this);             

        if (CompanionOut && CompanionType != CompanionType.None)
        {
            SetCompanion(CompanionType);
        }

        _plans = GameManager.Instance.Plans;

    }
    
    public void Init(string name, int playernum)
    {
        PlayerName = name;
        PlayerNumber = playernum;
        //Global.BattleMode = false;
        MiniMapControls.PlayerTransform = transform;
    }
 
    private void SetBasicPlayerStats()
    {
        Health = 100;
        
        //Figure out later        
        UIManager.Instance.HealthBar.BarValue = 100f;
        UIManager.Instance.StaminaBar.BarValue = 100f;

        MoveSpeed = 10f;
        BuildSpeed = 10f;        
    }
        
    private void Update()
    {

        /////// ATTACK
        if (!Global.BuildMode && Input.GetMouseButton(KeyBindings.LEFT_MOUSE_BUTTON))
        {
            if (SwingEnemyTargetSelected() != null)
            {
                //COME BACK!!!! Attack Methods go here
                _movement.SwingPlayer();
            }
        }

        //Temporary, work out the details for build mappings later
        //Disallow movements if player is DEAD
        MovementInput.Lock = IsDead;

        /////// BUILD MODE ACTIVATION
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Global.BuildMode = !Global.BuildMode;
            _battleCursor.Off();

            if (Global.BuildMode)
            {
                //Announce build mode on                
                _placementController.SetGrid = true;
                _placementController.LoadObject(_plans.GetPlans(ResourceType.Wood, "wall"));
            }
            else
            {
                //Announce build mode off                
                CameraRotate.BuildCamMode = false;
                _placementController.SetGrid = false;
                _placementController.ClearObject();
            }

        }

        /////// BUILD MODE OPTIONS
        if (Global.BuildMode)
        {                       

            if (Input.GetKeyDown(KeyBindings.BuildKey1))
            {
                _placementController.LoadObject(_plans.GetPlans(ResourceType.Wood, "wall"));
            }
            else if (Input.GetKeyDown(KeyBindings.BuildKey2))
            {
                _placementController.LoadObject(_plans.GetPlans(ResourceType.Wood, "floor"));
            }
            else if (Input.GetKeyDown(KeyBindings.BuildKey3))
            {
                _placementController.LoadObject(_plans.GetPlans(ResourceType.Wood, "ramp"));
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                _placementController.LoadObject(_plans.Barracks, true);
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                //_placementController.LoadObject(_plans.ResourceDepot, true);
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                //_placementController.LoadObject(_plans.WizardSpire, true);
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                //_placementController.LoadObject(_plans.Cannon, true);
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                //_placementController.LoadObject(_plans.Catapult, true);
            }
        }


        /////// TEST DAMAGE
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!IsDead)
            {
                Debug.Log("Doing damage to player!");
                SetHit(HitAmount);
            }
        }

        /////// TEST TROOP EXPLODER
        if(Input.GetKeyDown(KeyCode.K))
        {
            Selection.Instance.SingleTargetSelected.GameObject.GetComponent<Troop>().AddExplosionForce(10f);
        }

        /////// TEST COMPANIONS
        if (Input.GetKeyDown(KeyCode.C))
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

        /////// TEST CAM SHAKER
        if (Input.GetKeyDown(KeyCode.O))
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
        if(_movement != null)
            _movement.SetPlayerCompanion = PlayerCompanion;
        PlayerCompanion.transform.parent = transform;
        PlayerCompanion.transform.position = transform.position + (Vector3.right * 1.5f);
    }

    public void ReleaseCompanion()
    {
        if (PlayerCompanion == null) return;
        Destroy(PlayerCompanion.gameObject);
        if(_movement != null)
            _movement.SetPlayerCompanion = null;
        PlayerCompanion = null;
        CompanionOut = false;
    }   
          
    public void Swing()
    {
        if (SwingEnemyTargetSelected() == null) return;
        var target = SwingEnemyTargetSelected();

        switch (target.GameObject.tag)
        {
            case Global.BUILD_TAG:
            case Global.ENEMY_TAG:                
                target.SetHit(HitAmount);
                break;
            case "Player":
            case Global.ARMY_TAG:
            default:
                return;
        }        
    }
    
    private ISelectable SwingEnemyTargetSelected()
    {
        var enemytarget = UIManager.Instance.SelectableComponent.EnemyTargetSelected;
        if (enemytarget == null) return null;
        GameObject target = enemytarget.GameObject;
        if (!Extensions.DistanceLess(transform, target.transform, Global.STRIKE_DIST)) return null;
        return enemytarget;
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

    public override void Die()
    {
        Health = 0;
        PlayerUI.HealthText.text = $"0/100";
        UIManager.Instance.HealthBar.BarValue = 0;
        Debug.Log("I am DEAD!");
        _movement.Die();
    }

    public void Target(ISelectable target)
    {
        throw new System.NotImplementedException();
    }
       
    public void Move(Vector3 point)
    {
        
    }
}
