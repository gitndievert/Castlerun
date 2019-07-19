using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput : MonoBehaviour
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

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _camera = Camera.main;
        CharacterController = GetComponent<CharacterController>();     
    }

    // Start is called before the first frame update
    void Start()
    {        
        Lock = false;      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Lock) return;
        InputMagnitude();
        _moveVector = Vector3.zero;
        CharacterController.Move(_moveVector);       

        if (Input.GetKeyDown(KeyBindings.Jump)) Jump();
        if (Input.GetKeyDown(KeyBindings.Dance1)) Dance();
    }

    private void Update()
    {
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

    public void Swing()
    {
        Debug.Log("Swinging for attack");
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
