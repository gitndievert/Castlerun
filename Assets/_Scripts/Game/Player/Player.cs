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
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player : BasePrefab, IPlayer
{
    public static GameObject LocalPlayerInstance;

    [Header("Basic Player Properties")]
    public float MoveSpeed;
    public float BuildSpeed;
    public float AttackDistance = 3f;
    public override string DisplayName => PlayerName;

    [Range(15,50)]
    public int HitAmountMin = 30;
    [Range(25, 150)]
    public int HitAmountMax = 75;

    public MeleeWeaponTrail WeaponTrail;

    public TextMeshPro FloatingPlayerText;
    public TextMeshPro FloatingPlayerTitleText;

    public bool CompanionOut = false;        

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

    /// <summary>
    /// Players PUN Actor Number Assigned in network
    /// </summary>
    public int ActorNumber = 0;

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

    public ISelectable MyTarget
    {
        get { return UIManager.Instance.SelectableComponent.EnemyTargetSelected; }
    }

    #region ISelectables    
    public bool IsSelected { get; set; }
    public GameObject GameObject => gameObject;    
    #endregion

    #region Player Components      
    public Inventory Inventory { get; private set; }        
    public GameObject PlayerWorldItems { get; internal set; }    
    public string PlayerTitle { get; private set; }
    #endregion

    #region Private Members
    private GameObject _mainHand;
    private GameObject _offHand;   
    [SerializeField]
    private string _playerName;
    private BattleCursor _battleCursor;    
        
    private float _attackDelay = 1f;
    private float _lastAttacked;
    private int _hitCounter = 1;
    private bool _jumping;


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
    protected override void Start()
    {
        base.Start();

        if (!Global.DeveloperMode)
        {
            if (photonView != null && !photonView.IsMine)
            {                
                gameObject.layer = Global.ENEMY_LAYER;
            }
        }        

        //Set Cameras
        if (photonView.IsMine || Global.DeveloperMode)
        {
            Inventory = GetComponent<Inventory>();            
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
            //CastleManager.Instance.SpawnCastle(CastleType.FortressOfDoom, this);

            if (CompanionOut && CompanionType != CompanionType.None)
            {
                SetCompanion(CompanionType);
            }

            BuildManager.Instance.Placements.Player = this;

            if(FloatingPlayerText != null)            
                FloatingPlayerText.gameObject.SetActive(false);
            
            if (FloatingPlayerTitleText != null)            
                FloatingPlayerTitleText.gameObject.SetActive(false);

            if (PlayerText.PlayerTitles.Length > 0)
                PlayerTitle = PlayerText.PlayerTitles[Random.Range(0, PlayerText.PlayerTitles.Length - 1)];

        }

        WepTrailDisable();
        _movement = GetComponent<MovementInput>();
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
        if (Health <= 0)
            Health = MaxHealth;
        //Figure out later                
        UIManager.Instance.HealthBar.BarValue = Health;
        UIManager.Instance.StaminaBar.BarValue = Health;
        PlayerUI.HealthText.text = $"{Health}/{MaxHealth}";

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
                        if(Global.DeveloperMode)
                        {
                            Swing();
                        }
                        else
                        {
                            photonView.RPC("Swing", RpcTarget.All);
                        }
                    }
                }
            }

            //Temporary, work out the details for build mappings later
            //Disallow movements if player is DEAD
            MovementInput.Lock = IsDead;

            //Jump
            if(Input.GetKeyDown(KeyCode.Space))
            {
                _jumping = true;
                _movement.Jump();                
            }

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
            if (Input.GetKeyDown(KeyCode.K))
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
            if (Input.GetKeyDown(KeyCode.B))
            {
                ReleaseCompanion();
            }

            /////// TEST CAM SHAKER
            if (Input.GetKeyDown(KeyCode.O))
            {
                CamShake.Instance.Shake();
            }           

            /*if (Input.GetKeyDown(KeyCode.I))
            {
                _movement.Dance();
            }*/

            //Hit my face
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!IsDead)
                {
                    Debug.Log("Doing damage to player!");
                    SetHit(HitAmountMin,HitAmountMin);
                }
            }

            //Camera Switch
            if(Input.GetKeyDown(KeyCode.Z))
            {
                CameraRotate.BattleFieldMode = true;
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                CameraRotate.BattleFieldMode = false;
            }
           
        }

        //Face player labels toward camera
        if (FloatingPlayerText != null && FloatingPlayerText.text.Length > 0)
        {
            //var camVector = Camera.main.transform.position;
            FloatingPlayerText.rectTransform.LookAt(Camera.main.transform);
            FloatingPlayerText.rectTransform.Rotate(Vector3.up - new Vector3(0, 180, 0));
        }
        //Face player labels toward camera
        if (FloatingPlayerTitleText != null && FloatingPlayerTitleText.text.Length > 0)
        {
            //var camVector = Camera.main.transform.position;
            FloatingPlayerTitleText.rectTransform.LookAt(Camera.main.transform);
            FloatingPlayerTitleText.rectTransform.Rotate(Vector3.up - new Vector3(0, 180, 0));
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

    public void WepTrailEnable()
    {
        if (WeaponTrail == null) return;
        WeaponTrail.Emit = true;        
    }

    public void WepTrailDisable()
    {
        if (WeaponTrail == null) return;
        WeaponTrail.Emit = false;
    }

    [PunRPC]
    public void Swing()
    {
        if (UIManager.Instance.
            IsMouseOverUI()) return;
        if (MyTarget != null)
        {            
            _movement.AttackPlayer();
            if (photonView.IsMine || Global.DeveloperMode)
            {
                if (MyTarget.IsDead) return;                
                if (!Extensions.DistanceLess(transform, MyTarget.GameObject.transform, AttackDistance)) return;

                //May need to manage PUN tags
                switch (MyTarget.GameObject.tag)
                {
                    case Global.ENEMY_TAG:
                        MyTarget.SetHit(HitAmountMin, HitAmountMax);
                        break;
                }
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
    
    [PunRPC]
    protected override void RPC_TakeHit(int amount, bool takehit)
    {
        Health -= amount;
        if (takehit) _movement.Hit();
        PlayerUI.HealthText.text = $"{Health}/{MaxHealth}";
        UIManager.Instance.HealthBar.BarValue = Mathf.RoundToInt(((float)Health / MaxHealth) * 100);
        if (Health - amount <= 0)
            Die();
    }

    public override void SetHit(int min, int max)
    {
        //You're dead, go back
        if (Health <= 0 || IsDead) return;
        int amount = CalcDamage(min, max, out bool crit);
        if (Health - amount > 0)
        {
            Health -= amount;
            if (photonView.IsMine || Global.DeveloperMode)
            {
                PlayerUI.HealthText.text = $"{Health}/{MaxHealth}";
                UIManager.Instance.HealthBar.BarValue = Mathf.RoundToInt(((float)Health / MaxHealth) * 100);                
            }            

            bool takehit = _hitCounter >= 3;

            if (takehit)
            {
                if (HitSounds.Length > 0)
                    SoundManager.PlaySound(HitSounds);

                _movement.Hit();
                _hitCounter = 1;
            }

            if (!Global.DeveloperMode)            
                photonView.RPC("RPC_TakeHit", RpcTarget.Others, amount, takehit);            

            if (photonView.IsMine || Global.DeveloperMode)
                UIManager.Instance.FloatCombatText(TextType.Damage, amount, crit, transform);

            _hitCounter++;

        }
        else
        {
            if (DestroySound != null)
                SoundManager.PlaySound(DestroySound);

            Die();                    
        }
    }
    
    public override void Die()
    {
        StartCoroutine(DeathSequence());
    }   
   
    private IEnumerator DeathSequence()
    {
        IsDead = true;
        Health = 0;
        PlayerUI.HealthText.text = $"0/100";
        UIManager.Instance.HealthBar.BarValue = 0;        
        _movement.Die();
        if (photonView.IsMine)
        {
            Global.Message("YOU DIED, Respawn in 5 seconds...");
        }
        //Broadcast($"{PlayerName} has DIED!");
        yield return new WaitForSeconds(5f);                
        SetBasicPlayerStats();
        _movement.RestartAnimator();        
        IsDead = false;        
        yield return null;
    }  

    public void UnSelect()
    {
        if (IsSelected)
        {
            IsSelected = false;                              
        }
    }

    public void Select()
    {
        if (GetTag != Global.ENEMY_TAG) return;
        if (!IsSelected)
        {
            IsSelected = true;
            Selection selection = UIManager.Instance.SelectableComponent;
            //Single Target Selection Panel
            SelectionUI.UpdateEnemyTarget(this);            
        }
    }

    public void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        //if(Selection.Instance.SingleTargetSelected != null)
        //    Selection.Instance.BattleCursorOff();
        SelectionUI.ClearEnemyTarget();        
        Select();
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(KeyBindings.RIGHT_MOUSE_BUTTON))
            OnMouseDown();
    }   

    //Main method for serialization on Player actions
    public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //base.OnPhotonSerializeView(stream, info);
        if (stream.IsWriting)
        {
            // We own this player: send the others our data            
            //stream.SendNext(this.Health);
            //stream.SendNext("My name is mr fancy pants");         
            stream.SendNext(PlayerName);
            stream.SendNext(_movement.isAttacking);
            stream.SendNext(ActorNumber);
            //stream.SendNext(_jumping);            
            stream.SendNext(PlayerTitle);
            stream.SendNext(IsDead);
        }
        else
        {
            // Network player, receive data            
            //this.IsFiring = (bool)stream.ReceiveNext();
            //this.Health = (float)stream.ReceiveNext();

            //Debug.Log($"This is from the remote client {(string)stream.ReceiveNext()}");            
            var pname = (string)stream.ReceiveNext();
            FloatingPlayerText.text = pname;
            PlayerName = pname;
            var attacking = (bool)stream.ReceiveNext();
            //var myhealth = (int)stream.ReceiveNext();
            if (attacking)
            {
                _movement.AttackPlayer();
            }
            var actornum = (int)stream.ReceiveNext();
            ActorNumber = actornum;
           
           /* var jumping = (bool)stream.ReceiveNext();
            if(_jumping)
            {
                _movement.Jump();
                _jumping = false;
            }*/
            var title = (string)stream.ReceiveNext();
            FloatingPlayerTitleText.text = $"<{title}>";
            PlayerTitle = title;

            IsDead = (bool)stream.ReceiveNext();
        }
    }


}
