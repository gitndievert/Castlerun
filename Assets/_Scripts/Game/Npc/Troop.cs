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
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Collider))]
public abstract class Troop : BasePrefab, ISelectable
{
    const float TROOP_DESTROY_TIMER = 4f;
    
    
    [Header("All the waypoints that this Troop will follow")]
    public Dictionary<int, Transform> points = new Dictionary<int, Transform>();

    public TroopFactory AssociatedFactory { get; private set; }

    #region Selection Properties
    public Light SelectionTarget;
    public bool IsSelected { get; set; }    
    public GameObject GameObject => gameObject;
    #endregion    
    

    #region Visual Troop Control            

    private const float SmoothingCoefficient = .15f;
    private readonly float _velocityDenominatorMultiplier = .5f;
    private readonly float _minVelx = -2.240229f;
    private readonly float _maxVelx = 2.205063f;
    private readonly float _minVely = -2.33254f;
    private readonly float _maxVely = 3.70712f;
    private Vector2 _smoothDeltaPosition;
    private bool _moving;    
    private Vector2 _velocity = Vector2.zero;

    #endregion

    #region AudioClips For Troops
    [Space(5)]
    [Header("Troop Audio Clips")]
    public AudioClip FreshTroop;
    public AudioClip[] SelectionCall;
    public AudioClip[] Acknowledgement;
    public AudioClip[] AttackBattleCryClips;
    #endregion

    #region Combat Systems
    [Header("Combat")]
    public float AttackDelaySec = 3f;
    public bool IsAttacking = false;
    public float AttackDistance = 3f;
    public float AgroDistance = 5f;

    protected ISelectable AttackTarget;
    
    private float _lastAttacked;
    private int _hitCounter = 1;
    private bool _moveTriggerPoint;
    
    #endregion

    //Components
    protected Animator anim;
    protected NavMeshAgent nav;


    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        //Seconds until object is destroyes and cleaned up        
        nav = GetComponent<NavMeshAgent>();        
    }

    protected override void Start()
    {

        int mask = 1 << Global.GROUND_LAYER;

        if (Physics.Raycast(transform.position,Vector3.down,out RaycastHit hit,5f,mask))
        {
            nav.Warp(hit.point);
        }        

        base.Start();
        SelectionTargetStatus(false);

        //We don't want people exploding lol
        CanExplode = false;        
        DestroyTimer = TROOP_DESTROY_TIMER;        

        if (Costs.CostFactors.Length == 0)
            throw new System.Exception("Please add a cost");
        
        
        _smoothDeltaPosition = default;        
       
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //This troop is flagged as an army
        if (GetTag == Global.ARMY_TAG)
        {
            //For Selectable Troops
            if (UIManager.Instance.SelectableComponent.IsWithinSelectionBounds(gameObject) && !IsSelected)
                SelectMany();

            //So first get within range then do a MoveStop()
            if (AttackTarget != null && IsAttacking)
            {
                if (Extensions.DistanceLess(AttackTarget.GameObject.transform, transform, AttackDistance))
                {
                    if(_moving) MoveStop();
                    if (AttackTarget.IsDead)
                    {
                        StopAttack();
                    }
                    else if (Time.time > _lastAttacked && !AttackTarget.IsDead)
                    {
                        ResetAttackTimer();
                        Fire();
                    }
                }                
            }

            if (_moving)
            {
                var worldDeltaPosition = nav.nextPosition - transform.position;
                var dx = Vector3.Dot(transform.right, worldDeltaPosition);
                var dy = Vector3.Dot(transform.forward, worldDeltaPosition);
                var deltaPosition = new Vector2(dx, dy);
                var smooth = Time.fixedDeltaTime / SmoothingCoefficient;

                _smoothDeltaPosition = Vector2.Lerp(_smoothDeltaPosition, deltaPosition, smooth);

                var velocity = _smoothDeltaPosition / (Time.fixedDeltaTime * _velocityDenominatorMultiplier);

                var x = Mathf.Clamp(Mathf.Round(velocity.x * 1000) / 1000, _minVelx, _maxVelx);
                var y = Mathf.Clamp(Mathf.Round(velocity.y * 1000) / 1000, _minVely, _maxVely);

                if (nav.remainingDistance >= nav.stoppingDistance || _moveTriggerPoint)
                {
                    _moveTriggerPoint = false;

                    anim.SetBool("move", true);
                    anim.SetFloat("velx", x);
                    anim.SetFloat("vely", y);

                    if (worldDeltaPosition.magnitude > nav.radius / 16)
                    {
                        nav.nextPosition = transform.position + 0.1f * worldDeltaPosition;
                    }
                }
                else
                {
                    MoveStop();
                }
            }

        }
        //This troop is flagged as an enemy
        else if (GetTag == Global.ENEMY_TAG)
        {
            //Return attack
            /*if (TargetByPlayer != null && _myAttacker == null)
            {
                TargetingMe.Clear();
                //Add agro towards the player
                //Do within 8 meters for now
                if (Extensions.DistanceLess(TargetByPlayer.transform, transform, AgroDistance))
                {                    
                    _myAttacker = TargetByPlayer;
                    Attack();
                }
            }
            else if (TargetingMe.Count > 0 && _myAttacker == null)
            {
                foreach(var attacker in TargetingMe)
                {
                    //Do within 8 meters for now
                    if (Extensions.DistanceLess(attacker.transform,transform, AgroDistance))
                    {
                        _myAttacker = attacker;
                        Target(attacker);                        
                        Attack();
                        break;
                    }

                }
            }*/
        }
    }

    #region PUN Callbacks
    //Main method for serialization on Player actions   
    public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        base.OnPhotonSerializeView(stream, info);
    }  

    #endregion

    protected virtual void OnAnimatorMove()
    {
        if (!_moving) return;
        var position = anim.rootPosition;
        position.y = nav.nextPosition.y;
        transform.position = position;
    }

    #region Mouseover Callbacks
   
    public void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        //if(Selection.Instance.SingleTargetSelected != null)
        //    Selection.Instance.BattleCursorOff();
        SelectionUI.ClearEnemyTarget();
        SelectionTargetStatus(false);
        Select();
    }

    /// <summary>
    /// Simulate a right click on a mouse down event
    /// </summary>
    public void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(KeyBindings.RIGHT_MOUSE_BUTTON))
            OnMouseDown();        
        if (GetTag == Global.ENEMY_TAG && Selection.Instance.SingleTargetSelected == null)
        {
            float ySpot = Selection.Instance.SelectionTargetObj.transform.position.y;
            var pos = new Vector3(transform.position.x, ySpot, transform.position.z);
            Selection.Instance.BattleCursorOn(transform.position);
            //SelectionUI.UpdateEnemyTarget(this);
            //SelectionTargetStatus(true, DamageColor);
        }       
            
    }

    public void OnMouseExit()
    {
        if (GetTag == Global.ENEMY_TAG)
        {
            Selection.Instance.BattleCursorOff();            
        }
    }

    #endregion

    public void SelectMany()
    {
        if (GetTag != Global.ARMY_TAG) return;
        if (!IsSelected)
        {
            IsSelected = true;
            if (GetTag == Global.ARMY_TAG)
            {
                SelectionTargetStatus(true, SelectedColor);
                UIManager.Instance.SelectableComponent.UpdateMassList(this);                
            }
        }
    }

    public void SetFactory(TroopFactory factory)
    {
        AssociatedFactory = factory;
    }

    /// <summary>
    /// Called Method on Target Selection
    /// </summary>
    public void Select()
    {
        if (GetTag == Global.NPC_TAG) return;
        if (!IsSelected)
        {
            IsSelected = true;            
            
            if (GetTag == Global.ARMY_TAG)
            {
                //Single Target Selection Panel
                if(SelectionCall.Length > 0)
                    SoundManager.PlaySound(SelectionCall);
                SelectionUI.UpdateSingleTarget(this);
                SelectionTargetStatus(true, SelectedColor);                
                //glow green
            }
            else if(GetTag == Global.ENEMY_TAG)
            {
                //Single Target Selection Panel
                SelectionUI.UpdateEnemyTarget(this);
                SelectionTargetStatus(true, DamageColor);
                //glow red
            }
        }
    }

    /// <summary>
    /// Called Method on Target UnSelection
    /// </summary>
    public void UnSelect()
    {
        if (IsSelected)
        {
            IsSelected = false;
            SelectionTargetStatus(false);
            points.Clear();            
        }
    }

    protected void SelectionTargetStatus(bool status)
    {
        if (SelectionTarget == null) return;
        SelectionTarget.gameObject.SetActive(status);
    }

    protected void SelectionTargetStatus(bool status, Color color)
    {
        if (SelectionTarget == null) return;
        SelectionTargetStatus(status);
        SelectionTarget.color = color;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        //Come Back here!
        if (!_moving) return;
        if (collision.transform.tag == "Player") return;
        if (collision.transform.tag == Global.ARMY_TAG)
        {            
            MoveStop();            
        }
    }
    
    /// <summary>
    /// Attack methods needed for Troops
    /// </summary>
    public virtual void Attack(ISelectable target)
    {
        //InvokeRepeating("Fire", 0, AttackDelaySec);        
        if(target.IsDead)
        {
            StopAttack();
            return;
        }
        AttackTarget = target;
        IsAttacking = true;

    }

    /// <summary>
    /// What happens during the attack UNIQUE per type of troop
    /// </summary>
    public abstract void Fire();

    /// <summary>
    /// Stop Attack for Troops
    /// </summary>
    public virtual void StopAttack()
    {
        IsAttacking = false;
        AttackTarget = null;
        //anim.Play("Idle");
    }

   
    public void Move(Vector3 point)
    {
        //nav.ResetPath();
        nav.isStopped = false;        
        nav.SetDestination(point);
        transform.LookAt(point);        
        if(AttackBattleCryClips.Length > 0)
            SoundManager.PlaySound(AttackBattleCryClips);
        _moving = true;
        _moveTriggerPoint = true;
    }

    public void MoveStop()
    {
        _moving = false;        
        anim.SetBool("move", false);
        nav.velocity = Vector3.zero;
        nav.isStopped = true;
        _moveTriggerPoint = false;        
    }

    [PunRPC]
    protected override void RPC_TakeHit(int amount, bool takehit)
    {
        Health -= amount;
        if(takehit) anim.Play("Hit");
    }
    
    public override void SetHit(int min, int max)
    {
        //Taking this out to test
        //if (!IsSelected) return;
        if (IsDead) return;
        int amount = CalcDamage(min, max, out bool crit);
        if (Health - amount > 0)
        {
            Health -= amount;

            bool takehit = _hitCounter >= 3;

            if (takehit)
            {
                if (HitSounds.Length > 0)
                    SoundManager.PlaySound(HitSounds);

                anim.Play("Hit");
                _hitCounter = 1;
            }

            if (!Global.DeveloperMode)            
                photonView.RPC("RPC_TakeHit",RpcTarget.Others, amount, takehit);

            if ((photonView != null && photonView.IsMine) || Global.DeveloperMode)
                UIManager.Instance.FloatCombatText(TextType.Damage, amount, crit, transform);

            _hitCounter++;
        }
        else
        {            
            if (DestroySound != null)
                SoundManager.PlaySound(DestroySound);
            if (CanExplode) Explode();

            if (!Global.DeveloperMode)
            {
                photonView.RPC("Die", RpcTarget.Others);
            }
            else
            {
                Die();
            }
        }
    }

    [PunRPC]
    public override void Die()
    {        
        anim.Play("Death1");
        if (GetTag == Global.ARMY_TAG)
        {
            AssociatedFactory.UnListTroop();
        }
        base.Die();
    }

    private void ResetAttackTimer()
    {
        _lastAttacked = Time.time + AttackDelaySec;
    }



}
