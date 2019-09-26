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

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
public abstract class Troop : BasePrefab, ICharacter, ISelectable
{
    const float TROOP_DESTROY_TIMER = 4f;
    
    #region Selectable properties
    [Header("All the waypoints that this Troop will follow")]
    public Dictionary<int, Transform> points = new Dictionary<int, Transform>();

    public Light SelectionTarget;
    public bool IsSelected { get; set; }
    public Player TroopPlayer { get; set; }
    public GameObject GameObject => gameObject;
    #endregion

    #region Visual Troop Control        
    public abstract string DisplayName { get; }
   
    protected Animator anim;
    protected NavMeshAgent nav;        

    private const float SmoothingCoefficient = .15f;
    private float _velocityDenominatorMultiplier = .5f;
    private float _minVelx = -2.240229f;
    private float _maxVelx = 2.205063f;
    private float _minVely = -2.33254f;
    private float _maxVely = 3.70712f;
    private Vector2 _smoothDeltaPosition;
    private bool _moving;
    private int _destPoint;
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

    [HideInInspector]
    public bool CanAttack = false;
    public bool IsAttacking;
    public bool UnderAttack;

    protected ISelectable EnemyTarget { get; set; }
    protected Transform EnemyTargetTransform { get; set; }

    private int _hitCounter = 1;
    #endregion    

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        //Seconds until object is destroyes and cleaned up        
        nav = GetComponent<NavMeshAgent>();        
    }

    protected virtual void Start()
    {
        SelectionTargetStatus(false);
        MaxHealth = Health;    

        //We don't want people exploding lol
        CanExplode = false;        
        DestroyTimer = TROOP_DESTROY_TIMER;
        gameObject.layer = GetTag == Global.ARMY_TAG ? Global.ARMY_LAYER : 0;        

        if (Costs.CostFactors.Length == 0)
            throw new System.Exception("Please add a cost");
        
        
        _smoothDeltaPosition = default;
        _moving = true;              
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (GetTag == Global.ARMY_TAG)
        {
            //For Selectable Troops
            if (UIManager.Instance.SelectableComponent.IsWithinSelectionBounds(gameObject) && !IsSelected)
                SelectMany();

            if (CanAttack && !IsAttacking)
            {
                IsAttacking = true;
                Attack();
            }
            else if(!CanAttack)
            {
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

                    if (nav.remainingDistance >= nav.stoppingDistance)
                    {

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
        }
    }
        
    public void OnAnimatorMove()
    {
        if (!_moving) return;
        var position = anim.rootPosition;
        position.y = nav.nextPosition.y;
        transform.position = position;
    }    

    public void OnMouseExit()
    {
        //SelectionUI.ClearSingleTarget();
    }

    protected void GoToNextPoint()
    {
        if (points.Count == 0) return;               
        nav.destination = points[_destPoint].position;
        _destPoint = (_destPoint + 1) % points.Count;
    }

    public void OnMouseDown()
    {
        //if (EventSystem.current.IsPointerOverGameObject()) return;
        if (!IsSelected)
            Select();
    }
    
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

    /// <summary>
    /// Called Method on Target Selection
    /// </summary>
    public void Select()
    {
        if (GetTag == Global.NPC_TAG) return;
        if (!IsSelected)
        {
            IsSelected = true;
            Selection selection = UIManager.Instance.SelectableComponent;
            
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

    private void OnCollisionEnter(Collision collision)
    {
        //Come Back here!
        if (collision.transform.tag == "Player") return;
        if (collision.transform.tag == Global.ARMY_TAG)
        {            
            MoveStop();            
        }
    }
    
    /// <summary>
    /// Attack methods needed for Troops
    /// </summary>
    public virtual void Attack()
    {
        MoveStop();
        InvokeRepeating("Fire", 0, AttackDelaySec);
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
        ClearEnemyTargets();
        CancelInvoke(); //Stop Combat        
        //anim.Play("Grounded");
    }

    /// <summary>
    /// This Troops target for attack
    /// </summary>
    /// <param name="target"></param>
    public virtual void Target(ISelectable target)
    {        
        EnemyTarget = target;
        EnemyTargetTransform = target.GameObject.transform;        
    }

    public void ClearEnemyTargets()
    {
        EnemyTarget = null;
        EnemyTargetTransform = null;        
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
    }

    public void MoveStop()
    {
        _moving = false;
        //nav.SetDestination(transform.position);
        //nav.destination = transform.position;
        anim.SetBool("move", false);
        nav.velocity = Vector3.zero;
        nav.isStopped = true;        
    }

    public override void SetHit(int amount)
    {
        //Taking this out to test
        //if (!IsSelected) return;
        if (Health - amount > 0)
        {
            Health -= amount;

            if (_hitCounter >= 3)
            {
                if (HitSounds.Length > 0)
                    SoundManager.PlaySound(HitSounds);

                anim.Play("Hit");
                _hitCounter = 1;
            }

            _hitCounter++;
        }
        else
        {            
            if (DestroySound != null)
                SoundManager.PlaySound(DestroySound);
            if (CanExplode) Explode();
            Die();
        }
    }

    public override void Die()
    {
        anim.Play("Death1");
        base.Die();
    }

}
