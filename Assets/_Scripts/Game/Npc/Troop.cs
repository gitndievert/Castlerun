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


using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Troop : BasePrefab, ICharacter, ISelectable
{
    [Header("All the waypoints that this Troop will follow")]
    public Dictionary<int, Transform> points = new Dictionary<int, Transform>();

    public Light SelectionTarget;

    //Selectable
    public bool IsSelected { get; set; }

    public GameObject GameObject => gameObject;

    protected static readonly Color SelectedColor = Color.green;
    protected static readonly Color DamageColor = Color.red;

    protected Animator anim;
    protected NavMeshAgent nav;

    private Vector3 _lockPoint;
    private bool _isMoving = false;

    #region AudioClips For Troops
    public AudioClip[] SelectionCall;
    public AudioClip[] Acknowledgement;
    #endregion

    private int _destPoint;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        //Seconds until object is destroyes and cleaned up
        DestroyTimer = 1f;
        nav = GetComponent<NavMeshAgent>();
    }

    protected virtual void Start()
    {
        SelectionTargetStatus(false);
        MaxHealth = Health;
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {        
        if (!nav.pathPending && points.Count > 0)
        {
            GoToNextPoint();
        }   
        
        if(_isMoving)
        {
            nav.SetDestination(_lockPoint);
            
            if (nav.remainingDistance < 0.5f)
            {
                _isMoving = false;
                Debug.Log("is Moving");
                //anim.Play("Idle");
                _isMoving = false;                
            }            
        }
    }

    protected void GoToNextPoint()
    {
        if (points.Count == 0) return;               
        nav.destination = points[_destPoint].position;
        _destPoint = (_destPoint + 1) % points.Count;
    }

    private void OnMouseDown()
    {
        if(!IsSelected)
            Select();        
    }

    //No Idea what to do, maybe create another BasePrefab class for this???
    protected virtual void Update()
    {
        //For Selectable Troops
        if (UIManager.Instance.SelectableComponent.IsWithinSelectionBounds(gameObject) && !IsSelected)        
            Select();           
    }

    public void UnSelect()
    {
        if (IsSelected)
        {
            IsSelected = false;
            SelectionTargetStatus(false);
            points.Clear();            
        }
    }

    public void Select()
    {
        if (!IsSelected)
        {
            IsSelected = true;
            Debug.Log("Hit on " + gameObject.name);
            SelectionTargetStatus(true, SelectedColor);
            UIManager.Instance.SelectableComponent.UpdateList(this);
        }
    }
    
    private void SelectionTargetStatus(bool status)
    {
        if (SelectionTarget == null) return;
        SelectionTarget.gameObject.SetActive(status);
    }

    private void SelectionTargetStatus(bool status, Color color)
    {
        if (SelectionTarget == null) return;
        SelectionTargetStatus(status);
        SelectionTarget.color = SelectedColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Come Back here!
        if (collision.transform.tag != "Blah") return;
    }

    public abstract void Target(ISelectable target);    
        
    public void Move(Vector3 point)
    {
        //anim.Play("Walk");
        _lockPoint = point;
        _isMoving = true;
    }


}
