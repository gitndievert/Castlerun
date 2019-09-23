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

using Mirror;
using UnityEngine;

public class MovementInput : NetworkBehaviour
{
    public static bool Lock;

    [SyncVar]
    public float InputX;
    [SyncVar]
    public float InputZ;
    public Vector3 DesiredMoveDirection;
    public bool BlockRotationPlayer;
    public float DesiredRotationSpeed;
    public float Speed;    
    public float AllowPlayerRotation;
    public bool IsGrounded;
    public float RotateOverload = 2f;

    private float _verticalVelocity;


    public CharacterController CharacterController;

    public Companion SetPlayerCompanion
    {
        set
        {
            if (value == null)
            {
                _companionAnim = null;
            }
            else
            {
                _companionAnim = value.gameObject.GetComponent<Animator>();
            }
        }
    }

    private Camera _camera;
    private Animator _anim;    
    private Vector3 _moveVector;
    private Animator _companionAnim;    

    [SerializeField]
    private float _groundCheckDistance = 0.1f;

    private float _origGroundCheckDistance;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _camera = Camera.main;
        CharacterController = GetComponent<CharacterController>();
        Global.MouseLook = false;        
    }

    // Start is called before the first frame update
    void Start()
    {        
        Lock = false;
        _origGroundCheckDistance = _groundCheckDistance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Lock) return;
        InputMagnitude();
        _moveVector = Vector3.zero;
        CharacterController.Move(_moveVector);

        //Dont Allow Right Mouse if Running
        Global.MouseLook = Speed > 0;

        //Add Grounding

        if (Input.GetKeyDown(KeyBindings.Jump)) Jump();
        if (Input.GetKeyDown(KeyBindings.Dance1)) Dance();
    }

    private void Update()
    {

        //Make sure character is grounded
        IsGrounded = CharacterController.isGrounded;
         _verticalVelocity -= IsGrounded ? 0 : 1;
        Vector3 moveVector = new Vector3(0, _verticalVelocity, 0);
        CharacterController.Move(moveVector);

        //Allowed the Mouse Look on Right Mouse Button
        if (Input.GetMouseButton(KeyBindings.RIGHT_MOUSE_BUTTON))
        {
            float mouseInput = Input.GetAxis("Mouse X");
            Vector3 lookhere = new Vector3(0, mouseInput, 0);
            transform.Rotate(lookhere * RotateOverload);
            Global.MouseLook = true;
        }
        else
        {
            Global.MouseLook = false;
        }
    }

    //NOTE: Come back here to add the strafe on X
    [ClientCallback]
    private void InputMagnitude()
    {
        if (!isLocalPlayer) return;
        //Calc input vectors
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");            

        _anim.SetFloat("InputZ", InputZ, 0.0f, Time.deltaTime * 2f);
        _anim.SetFloat("InputX", InputX, 0.0f, Time.deltaTime * 2f);

        if(_companionAnim != null)
        {
            _companionAnim.SetFloat("InputZ", InputZ, 0.0f, Time.deltaTime * 2f);
        }

        //Calc the Input Magnitude
        Speed = new Vector2(InputX, InputZ).sqrMagnitude * 5;

        _anim.SetFloat("Speed", Speed, 0.0f, Time.deltaTime);
    }

    public void Jump()
    {
        _anim.Play("Jump");
    }

    public void Dance()
    {
        _anim.Play("Dance");
    }

    public void StopDancing()
    {
        //_anim.
    }

    public void SwingPlayer()
    {        
        _anim.SetBool("Swing", true);
    }

    public void SwingStop()
    {
        _anim.SetBool("Swing", false);
    }

    public void Hit()
    {
        _anim.Play("Hit");
    }

    public void Die()
    {
        _anim.Play("Death1");
    }
    
}
