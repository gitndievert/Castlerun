﻿// ********************************************************************
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
    public float AttackDistance = 3f;
    public float AttackDelay = 1f;
    public override string DisplayName => PlayerName;

    [Range(15,50)]
    public int HitAmountMin = 30;
    [Range(25, 150)]
    public int HitAmountMax = 75;

    public MeleeWeaponTrail WeaponTrail;

    public TextMeshPro FloatingPlayerText;
    public TextMeshPro FloatingPlayerTitleText;
    
    /// <summary>
    /// Players PUN Actor Number Assigned in network
    /// </summary>
    [Tooltip("The PUN assigned player actor number")]
    public int ActorNumber = 0;


    #region Player Holdings

    [Header("Player Attachements")]
    /// <summary>
    /// Mainhand Object
    /// </summary>
    public GameObject MainHand = null;

    /// <summary>
    /// (Optional) Offhand Object
    /// </summary>
    public GameObject Offhand = null;

  
    /// <summary>
    /// Returns the current companion of the player
    /// </summary>
    public Companion PlayerCompanion;

    /// <summary>
    /// Returns the Players Castle
    /// </summary>
    public Castle PlayerCastle;

    [Header("Flag Actions")]
    /// <summary>
    /// Returns if the Players Flag if Carrying
    /// </summary>
    public Flag PlayerFlag;

    public Transform HandMountPoint;

    public Transform BackMountPoint;

    #endregion

    public Vector3 RespawnPos { get; set; }

    /// <summary>
    /// Returns the status of player holding the flag
    /// </summary>
    public bool HasFlag
    {
        get
        {
            return PlayerFlag != null;
        }
    }

    public bool PickedUpFlag = false;

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
    public string PlayerTitle { get; private set; }
    #endregion

    #region Private Members    
    [SerializeField]
    private bool _isTestPlayer = false;
    private string _playerName;
    private BattleCursor _battleCursor;    
    private float _lastAttacked;
    private int _hitCounter = 1;
    private bool _jumping;
    private MovementInput _movement;    

    #endregion

    protected override void Awake()
    {
        if (!_isTestPlayer)
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
        }
                
        base.Awake();

        //Sets the transform respawn point
        RespawnPos = transform.position;

    }
       

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        if (_isTestPlayer) return;

        if (!Global.DeveloperMode)
        {
            if (photonView != null && !photonView.IsMine)
            {                
                gameObject.layer = Global.ENEMY_LAYER;
            }
        }

        //Hide Weapons on Start
        WepTrailDisable();

        if (MainHand != null)
            MainHand.SetActive(false);

        if (Offhand != null)
            Offhand.SetActive(false);

        CreateFlag();

        //Set Cameras
        if (photonView.IsMine || Global.DeveloperMode)
        {            
            _battleCursor = GetComponent<BattleCursor>();
            
            PlayerName = PhotonNetwork.LocalPlayer.NickName;            

            CameraRotate.target = transform;
            MiniMapControls.target = transform;            

            //Player stats
            SetBasicPlayerStats();       

            if(FloatingPlayerText != null)            
                FloatingPlayerText.gameObject.SetActive(false);
            
            if (FloatingPlayerTitleText != null)            
                FloatingPlayerTitleText.gameObject.SetActive(false);

            if (PlayerText.PlayerTitles.Length > 0)
                PlayerTitle = PlayerText.PlayerTitles[Random.Range(0, PlayerText.PlayerTitles.Length - 1)];           

        }
        
        _movement = GetComponent<MovementInput>();        

        if (_playerName == "")
            PlayerName = "Mr Player";

        PlayerUI.PlayerName.text = PlayerName;

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
        if ((photonView.IsMine || Global.DeveloperMode) && !_isTestPlayer)
        {
            UIManager.Instance.HealthBar.BarValue = Health;
            UIManager.Instance.StaminaBar.BarValue = Health;
            PlayerUI.HealthText.text = $"{Health}/{MaxHealth}";
        }        
    }
           
    private void Update()
    {
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

        AttachCastle();        

        //If test player don't proceed through update()
        if (_isTestPlayer) return;

        //All "Active" player settings in the loop
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

            if (!_isTestPlayer)
            {               
                 PlayerUI.HealthText.text = $"{Health}/{MaxHealth}";
                 UIManager.Instance.HealthBar.BarValue = Mathf.RoundToInt(((float)Health / MaxHealth) * 100);               
            }

            //Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _jumping = true;
                _movement.Jump();                
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
           
        }              

    }      

    //These three methods probably need their own class
    public void SetCompanion(CompanionType companion)
    {        
        ReleaseCompanion();
        var mycompanion = Instantiate(CompanionManager.Instance.GetCompanion(companion),transform.position,transform.rotation);        
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
        //Don't swing if weapon not visible
        if (MainHand == null || !MainHand.activeSelf) return;

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
                    if (hit.GetLayer() != Global.UI_LAYER)
                    {
                        _movement.AttackPlayer();
                    }
                }
            }
        }        
    }

    private void ResetAttackTimer()
    {
        _lastAttacked = Time.time + AttackDelay;
    }
    
    [PunRPC]
    protected void RPC_TakeHit(int amount, bool takehit, int target)
    {
        Debug.Log($"Target number is {target} and my number is {ActorNumber}");
        if (target == ActorNumber)
        {
            Health -= amount;
            if (Health <= 0) Die();            
            if (takehit) _movement.Hit();
            PlayerUI.HealthText.text = $"{Health}/{MaxHealth}";
            UIManager.Instance.HealthBar.BarValue = Mathf.RoundToInt(((float)Health / MaxHealth) * 100);            
        }
    }

    public override void SetHit(int min, int max)
    {
        //You're dead, go back
        if (Health <= 0 || IsDead) return;
        int amount = CalcDamage(min, max, out bool crit);

        Health -= amount;        

        bool takehit = _hitCounter >= 3;

        if (takehit)
        {
            if (HitSounds.Length > 0)
                SoundManager.PlaySound(HitSounds);

            if (!_isTestPlayer)
                _movement.Hit();

            _hitCounter = 1;
        }

        if (!Global.DeveloperMode)
            photonView.RPC("RPC_TakeHit", RpcTarget.Others, amount, takehit, ActorNumber);

        UIManager.Instance.FloatCombatText(TextType.Damage, amount, crit, transform);

        _hitCounter++;

        if (Health <= 0) Die();        

    }
    
    public override void Die()
    {        
        StartCoroutine(DeathSequence());
    }   
   
    private IEnumerator DeathSequence()
    {
        if (DestroySound != null)
            SoundManager.PlaySound(DestroySound);
        IsDead = true;
        Health = 0;        
        _movement.Die();
        UnSelect();
        if (photonView.IsMine)
        {
            Global.Message("YOU DIED, Respawn in 5 seconds...");
            PlayerUI.HealthText.text = $"0/100";
            UIManager.Instance.HealthBar.BarValue = 0;
        }
        //Broadcast($"{PlayerName} has DIED!");
        yield return new WaitForSeconds(5f);                
        SetBasicPlayerStats();
        _movement.RestartAnimator();
        IsDead = false;
        transform.position = RespawnPos;
        yield return null;
    }  

    public void UnSelect()
    {
        if (IsSelected)
        {
            IsSelected = false;
            if (FloatingSelectable != null)
                FloatingSelectable.SetActive(false);
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
            if (FloatingSelectable != null)
                FloatingSelectable.SetActive(true);
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

    
    public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //base.OnPhotonSerializeView(stream, info);
        if (stream.IsWriting)
        {
            // We own this player: send the others our data                        
            stream.SendNext(PlayerName);
            stream.SendNext(_movement.isAttacking);
            stream.SendNext(ActorNumber);            
            stream.SendNext(PlayerTitle);            
        }
        else
        {
            // Network player, receive data            
            
            var pname = (string)stream.ReceiveNext();
            FloatingPlayerText.text = pname;
            PlayerName = pname;
            var attacking = (bool)stream.ReceiveNext();
            
            if (attacking)
            {
                _movement.AttackPlayer();
            }
            var actornum = (int)stream.ReceiveNext();
            ActorNumber = actornum;           
           
            var title = (string)stream.ReceiveNext();
            FloatingPlayerTitleText.text = $"<{title}>";
            PlayerTitle = title;            
        }
    }


    //Attachments 

    private void AttachCastle()
    {
        //Attach scene castle to player
        if (PlayerCastle == null)
        {
            var castles = FindObjectsOfType<Castle>();
            foreach (var castle in castles)
            {
                if (ActorNumber == castle.PlayerNumber)
                {
                    PlayerCastle = castle;
                    break;
                }
            }
        }
    }

    private void CreateFlag()
    {
        if (PlayerFlag == null)
        {
            var makeFlag = PhotonNetwork.Instantiate(GameManager.Instance.GameFlag.name, HandMountPoint.position, Quaternion.identity);
            PlayerFlag = makeFlag.GetComponent<Flag>();
            PlayerFlag.PlayerNumber = ActorNumber;
            PickUpFlag(PlayerFlag);
        }

    }   

    /*
    private void Attachment<T>(GameObject obj) where T: IAttachable
    {
        if (obj == null)
        {
            var type = FindObjectsOfType<T>();
            foreach (var flag in type)
            {
                if (ActorNumber == flag.PlayerNumber)
                {
                    obj = flag;
                    break;
                }
            }
        }
    }*/

    private void AttachCompanion()
    {
        //Future code to attach companions go here
    }

    public void PickUpFlag(Flag flag)
    {        
        PickedUpFlag = true;        
        //Move to RPC
        //PlayerFlag.transform.SetParent(HandMountPoint);        
    }

    public void DropFlag()
    {
        if(HasFlag)
        {
            PickedUpFlag = false;
            //Move to RPC
            //PlayerFlag.transform.parent = null;            
        }
    }



}
