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
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
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
    protected Rigidbody rb;

    private Vector3 _lockPoint;
    private bool _isMoving = false;
    private ThirdPersonCharacter _char;
    #endregion

    #region AudioClips For Troops
    [Space(5)]
    [Header("Troop Audio Clips")]
    public AudioClip FreshTroop;
    public AudioClip[] SelectionCall;
    public AudioClip[] Acknowledgement;
    public AudioClip[] AttackBattleCryClips;
    #endregion

    private int _destPoint;

    #region Targeting Systems
    [Header("Combat")]
    public float AttackDelaySec = 3f;
    [HideInInspector]
    public bool CanAttack = false;
    protected ISelectable EnemyTarget { get; set; }
    protected Transform EnemyTargetTransform { get; set; }  
    public bool IsAttacking { get; set; }
    #endregion

    private int _hitCounter = 1;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        //Seconds until object is destroyes and cleaned up        
        nav = GetComponent<NavMeshAgent>();
        _char = GetComponent<ThirdPersonCharacter>();
        if (GetComponent<Rigidbody>() == null)
            gameObject.AddComponent<Rigidbody>();

        rb = GetComponent<Rigidbody>();        
    }

    protected virtual void Start()
    {
        SelectionTargetStatus(false);
        MaxHealth = Health;
        nav.updateRotation = true;
        nav.updatePosition = true;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        //We don't want people exploding lol
        CanExplode = false;        
        DestroyTimer = TROOP_DESTROY_TIMER;
        gameObject.layer = GetTag == Global.ARMY_TAG ? Global.ARMY_LAYER : 0;
        anim.applyRootMotion = false;

        if (Costs.CostFactors.Length == 0)
            throw new System.Exception("Please add a cost");
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if (GetTag == Global.ARMY_TAG)
        {  
            if (_isMoving)
            {
                nav.SetDestination(_lockPoint);              

                if (nav.remainingDistance >= nav.stoppingDistance)
                {
                    _char.Move(nav.desiredVelocity, false, false);
                }
                else
                {
                    _isMoving = false;
                    MoveStop();
                }              
            }

            if (CanAttack && !IsAttacking)
            {                
                IsAttacking = true;
                _lockPoint = transform.position; //Stop at this position             
                Attack();
            }

        }
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
        if(!IsSelected)
            Select();
    }

    //No Idea what to do, maybe create another BasePrefab class for this???
    protected virtual void Update()
    {
        //For Selectable Troops
        if (UIManager.Instance.SelectableComponent.IsWithinSelectionBounds(gameObject) && !IsSelected)
            SelectMany();
        //Keep Grounded
        //Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit);
        //transform.up -= (transform.up - hit.normal) * 0.1f;
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
        if (collision.transform.tag == Global.ARMY_TAG ||
            collision.transform.tag == Global.ENEMY_TAG)
        {
            MoveStop();            
        }
    }
    
    /// <summary>
    /// Attack methods needed for Troops
    /// </summary>
    public virtual void Attack()
    {
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
        _lockPoint = point;
        _isMoving = true;
        if(AttackBattleCryClips.Length > 0)
            SoundManager.PlaySound(AttackBattleCryClips);
    }

    public void MoveStop()
    {
        _isMoving = false;
        _char.Move(Vector3.zero, false, false);
        nav.velocity = Vector3.zero;         
    }

    public override void SetHit(int amount)
    {
        //Taking this out to test
        //if (!IsSelected) return;
        if (Health - amount > 0)
        {
            Health -= amount;

            if (_hitCounter >= 5)
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
