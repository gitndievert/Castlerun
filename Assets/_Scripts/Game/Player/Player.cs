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

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Player : BasePrefab, IPlayer, IPunObservable
{
    public static GameObject LocalPlayerInstance;

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
    //Camera Rig    
    public Inventory Inventory { get; private set; }    
    public int ActorNumber { get; internal set; }
    public GameObject PlayerWorldItems { get; internal set; }    
    #endregion

    #region Private Members
    private GameObject _mainHand;
    private GameObject _offHand;                 
    private string _playerName;
    private BattleCursor _battleCursor;
    private bool _swinging = false;


    private MovementInput _movement;



    #endregion

    [Tooltip("The Player's UI GameObject Prefab")]
    [SerializeField]
    private GameObject playerUiPrefab;

    protected override void Awake()
    {
        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instanciation when levels are synchronized
        if (photonView.IsMine || Global.DeveloperMode)
        {
            LocalPlayerInstance = gameObject;
        }

        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(gameObject);

        PlayerWorldItems = new GameObject("PlayerWorldItems");
        base.Awake();
    }

    // Start is called before the first frame update
    protected void Start()
    {
        //Set Cameras
        if (photonView.IsMine || Global.DeveloperMode)
        {

            Inventory = GetComponent<Inventory>();
            _movement = GetComponent<MovementInput>();
            _battleCursor = GetComponent<BattleCursor>();
            if (photonView.Owner != null)
            {
                PlayerName = photonView.Owner.NickName;
            }

            CameraRotate.target = transform;
            MiniMapControls.target = transform;

            PlayerUI.PlayerName.text = _playerName;

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

            BuildManager.Instance.Placements.Player = this;
        }

        // Create the UI
        if (this.playerUiPrefab != null)
        {
            GameObject _uiGo = Instantiate(playerUiPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
        {
            Debug.LogWarning("<Color=Red><b>Missing</b></Color> PlayerUiPrefab reference on player Prefab.", this);
        }
    }

    public override void OnDisable()
    {
        // Always call the base to remove callbacks
        base.OnDisable();        
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;        
    }
        
    protected void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
    {
        CalledOnLevelWasLoaded(scene.buildIndex);
    }    

    /// <summary>
    /// MonoBehaviour method called after a new level of index 'level' was loaded.
    /// We recreate the Player UI because it was destroy when we switched level.
    /// Also reposition the player if outside the current arena.
    /// </summary>
    /// <param name="level">Level index loaded</param>
    protected void CalledOnLevelWasLoaded(int level)
    {
        // check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
        if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
        {
            transform.position = new Vector3(0f, 5f, 0f);
        }

        GameObject _uiGo = Instantiate(this.playerUiPrefab);
        _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
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
        if (!Global.BuildMode && Input.GetMouseButton(KeyBindings.RIGHT_MOUSE_BUTTON))
        {
            if (SwingEnemyTargetSelected() != null)
            {
                //COME BACK!!!! Attack Methods go here                
                if (!_swinging)
                {
                    Swing();
                }                
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
                BuildManager.Instance.Placements.SetGrid = true;
                BuildManager.Instance.LoadBasicWall();
            }
            else
            {
                //Announce build mode off                
                CameraRotate.BuildCamMode = false;
                BuildManager.Instance.Placements.SetGrid = false;                
                BuildManager.Instance.Placements.ClearObject();                
            }

        }

        /////// BUILD MODE OPTIONS
        if (Global.BuildMode)
        {                       
            if (Input.GetKeyDown(KeyBindings.BuildKey1))
            {
                BuildManager.Instance.LoadBasicWall();
            }
            else if (Input.GetKeyDown(KeyBindings.BuildKey2))
            {
                BuildManager.Instance.LoadBasicFloor();
            }
            else if (Input.GetKeyDown(KeyBindings.BuildKey3))
            {
                BuildManager.Instance.LoadBasicRamp();
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
            CamShake.Instance.Shake();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            CamShake.Instance.Shake(1f, .5f);
        }    
               
        if(Input.GetKeyDown(KeyCode.I))
        {
            _movement.Dance();
        }
        
    }

    //Main method for serialization on Player actions
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data

            //stream.SendNext(this.IsFiring);
            //stream.SendNext(this.Health);
        }
        else
        {
            // Network player, receive data

            //this.IsFiring = (bool)stream.ReceiveNext();
            //this.Health = (float)stream.ReceiveNext();
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
        _swinging = true;
        if (SwingEnemyTargetSelected() != null)
        {
            _movement.SwingPlayer();
            var target = SwingEnemyTargetSelected();

            switch (target.GameObject.tag)
            {
                case Global.ENEMY_TAG:
                    Damage.ApplyDamage(target, 5, 25, true);
                    break;
            }                      
        }

        _swinging = false;
        _movement.SwingStop();
    }
    
    private ISelectable SwingEnemyTargetSelected()
    {
        var enemytarget = UIManager.Instance.SelectableComponent.EnemyTargetSelected;
        if (enemytarget == null) return null;
        GameObject target = enemytarget.GameObject;
        //Removed for testing
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
