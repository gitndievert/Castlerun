using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput : MonoBehaviour
{

    public float InputX;
    public float InputZ;
    public Vector3 DesiredMoveDirection;
    public bool BlockRotationPlayer;
    public float DesiredRotationSpeed;
    public float Speed;    
    public float AllowPlayerRotation;
    public bool IsGrounded;    
    
    public CharacterController CharacterController;

    private Camera _camera;
    private Animator _anim;
    private float _verticalVelocity;
    private Vector3 _moveVector;
             
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _camera = Camera.main;
        CharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputMagnitude();
        IsGrounded = CharacterController.isGrounded;
        if(IsGrounded)
        {
            _verticalVelocity -= 0;
        }        
        else
        {
            _verticalVelocity -= 2;
        }
        
        _moveVector = new Vector3(0, _verticalVelocity, 0);
        CharacterController.Move(_moveVector);

        if(Input.GetKeyDown(KeyCode.Space)) Jump();        
                
        //Legacy
        //if there is no root motion available
        /*Vector3 newPosition = transform.position;
        newPosition.z += _anim.GetFloat("InputMagnitude") * Time.deltaTime;
        transform.position = newPosition;*/

    }  

    private void PlayerMoveAndRotation()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");
                
        var forward = _camera.transform.forward;
        var right = _camera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        DesiredMoveDirection = forward * InputZ + right * InputX;

        if(!BlockRotationPlayer)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(DesiredMoveDirection), DesiredRotationSpeed);
        }
    }

    private void InputMagnitude()
    {
        //Calc input vectors
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        var vecout = new Vector2(InputZ, InputX);        

        _anim.SetFloat("InputZ", InputZ, 0.0f, Time.deltaTime * 2f);
        _anim.SetFloat("InputX", InputX, 0.0f, Time.deltaTime * 2f);

        //Calc the Input Magnitude
        Speed = new Vector2(InputX, InputZ).sqrMagnitude * 5;

        _anim.SetFloat("Speed", Speed, 0.0f, Time.deltaTime);
    }

    public void Jump()
    {
        _anim.Play("Jump");
    }


}
