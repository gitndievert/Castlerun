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

using UnityEngine;
using Photon.Pun;

public class MovementInput : MonoBehaviourPun, IPunObservable
{
    public static bool Lock;
        
    public float InputX;    
    public float InputZ;
    public Vector3 DesiredMoveDirection;
    public bool BlockRotationPlayer;
    public float DesiredRotationSpeed;
    public float Speed;    
    public float AllowPlayerRotation;
    public bool IsGrounded;
    public float RotateOverload = 2f;

    private float _verticalVelocity;
    private bool _isJumping;

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
        
    private Animator _anim;    
    private Vector3 _moveVector;
    private Animator _companionAnim;    

    [SerializeField]
    private float _groundCheckDistance = 0.1f;

    private float _origGroundCheckDistance;

    private void Awake()
    {
        _anim = GetComponent<Animator>();        
        CharacterController = GetComponent<CharacterController>();
        Global.MouseLook = false;        
    }

    // Start is called before the first frame update
    void Start()
    {        
        Lock = false;
        _origGroundCheckDistance = _groundCheckDistance;
    }   

    private void Update()
    {
        if (Lock && (!photonView.IsMine || !Global.DeveloperMode)) return;
        InputMagnitude();
        _moveVector = Vector3.zero;
        CharacterController.Move(_moveVector);

        //Make sure character is grounded
        IsGrounded = CharacterController.isGrounded;
         _verticalVelocity -= IsGrounded ? 0 : 1;
        Vector3 moveVector = new Vector3(0, _verticalVelocity, 0);
        CharacterController.Move(moveVector);

        //Allowed the Mouse Look on Right Mouse Button
        if (Input.GetMouseButton(KeyBindings.RIGHT_MOUSE_BUTTON) && Speed > 0)
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

        //Experiment with jumps
        if (Input.GetKeyDown(KeyBindings.Jump))
        {
            if (!_isJumping)
                _isJumping = true;
            Jump();
        }
        if(Input.GetKeyUp(KeyBindings.Jump))
        {
            if (_isJumping)
                _isJumping = false;
        }

        if (Input.GetKeyDown(KeyBindings.Dance1)) Dance();
    }

    //NOTE: Come back here to add the strafe on X    
    private void InputMagnitude()
    {        
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

    //Main method for serialization on Player actions
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(_isJumping);
            //stream.SendNext(this.Health);
        }
        else
        {
            // Network player, receive data
            _isJumping = (bool)stream.ReceiveNext();
            //this.Health = (float)stream.ReceiveNext();
        }
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
