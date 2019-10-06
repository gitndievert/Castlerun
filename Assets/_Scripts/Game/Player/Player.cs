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
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : BasePrefab, IPlayer, IPunObservable
{
    public static GameObject LocalPlayerInstance;

    [Header("Basic Player Properties")]
    public float MoveSpeed;
    public float BuildSpeed;

    [Range(15,50)]
    public int HitAmountMin = 30;
    [Range(25, 150)]
    public int HitAmountMax = 75;

    public TextMeshPro FloatingPlayerText;

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

    public Vector3 RespawnPos { get; set; }

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
        
    private float _attackDelay = 1f;
    private float _lastAttacked;    


    private MovementInput _movement;

    #region PUN Triggers    

    #endregion


    #endregion

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

        //Sets the transform respawn point
        RespawnPos = transform.position;

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


            PlayerName = PhotonNetwork.LocalPlayer.NickName;            

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

            if(FloatingPlayerText != null)
            {
                FloatingPlayerText.gameObject.SetActive(false);

            }            

        }        
        
    }

    public override void OnDisable()
    {
        // Always call the base to remove callbacks
        base.OnDisable();        
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
  
    protected void OnSceneLoaded(Scene scene, LoadSceneMode loadingMode)
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
    }

    private void SetBasicPlayerStats()
    {
        Health = 100;

        //Figure out later                
        UIManager.Instance.HealthBar.BarValue = Health;
        UIManager.Instance.StaminaBar.BarValue = Health;
        PlayerUI.HealthText.text = $"{Health}/100";

        MoveSpeed = 10f;
        BuildSpeed = 10f;
    }
           
    private void Update()
    {
        //Attack 
        if (photonView.IsMine || Global.DeveloperMode)
        {
            if (Time.time > _lastAttacked)
            {
                if ((Input.GetMouseButton(KeyBindings.LEFT_MOUSE_BUTTON) ||
                    Input.GetMouseButtonDown(KeyBindings.LEFT_MOUSE_BUTTON))
                    && !Selection.IsSelecting)
                {
                    if (!Global.BuildMode)
                    {                        
                        ResetAttackTimer();
                        Swing();
                    }
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

        //Hit my face
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!IsDead)
            {
                Debug.Log("Doing damage to player!");
                SetHit(HitAmountMin);                
            }
        }


        //Face player labels toward camera
        if (FloatingPlayerText != null && FloatingPlayerText.text.Length > 0)
        {
            //var camVector = Camera.main.transform.position;
            FloatingPlayerText.rectTransform.LookAt(Camera.main.transform);
            FloatingPlayerText.rectTransform.Rotate(Vector3.up - new Vector3(0, 180, 0));
        }           

        
    }

    //Main method for serialization on Player actions
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data            
            //stream.SendNext(this.Health);
            //stream.SendNext("My name is mr fancy pants");            
            if (FloatingPlayerText != null)
            {
                stream.SendNext(PlayerName);                
            }
        }
        else
        {
            // Network player, receive data            
            //this.IsFiring = (bool)stream.ReceiveNext();
            //this.Health = (float)stream.ReceiveNext();
            
            //Debug.Log($"This is from the remote client {(string)stream.ReceiveNext()}");
            FloatingPlayerText.text = (string)stream.ReceiveNext();
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
        if (SwingEnemyTargetSelected() != null)
        {
            _movement.AttackPlayer();
            var target = SwingEnemyTargetSelected();

            //May need to manage PUN tags
            switch (target.GameObject.tag)
            {
                case Global.ENEMY_TAG:
                    Damage.ApplyDamage(target, HitAmountMin, HitAmountMax, true);
                    break;
            }
        }
        else
        {
            if (Physics.Raycast(Selection.SelectionRayHit, out RaycastHit hit))
            {
                if (hit.point != null)
                { 
                    if (hit.transform.tag != Global.ARMY_TAG 
                        && hit.transform.tag != Global.BUILD_TAG 
                        && hit.GetLayer() != Global.UI_LAYER)
                    {
                        _movement.AttackPlayer();
                    }
                }
            }
        }        
    }

    private void ResetAttackTimer()
    {
        _lastAttacked = Time.time + _attackDelay;
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
        StartCoroutine(DeathSequence());
    }

    public void Target(ISelectable target)
    {
        throw new System.NotImplementedException();
    }
   
    private IEnumerator DeathSequence()
    {
        Health = 0;
        PlayerUI.HealthText.text = $"0/100";
        UIManager.Instance.HealthBar.BarValue = 0;
        Debug.Log("I am DEAD!");
        _movement.Die();
        Global.Message("YOU DIED, Respawn in 5 seconds...");
        //Broadcast($"{PlayerName} has DIED!");
        yield return new WaitForSeconds(5f);        
        SetBasicPlayerStats();
        _movement.RestartAnimator();        
        IsDead = false;        
        yield return null;
    }    
   
}
