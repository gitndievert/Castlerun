using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Gatherer : Troop
{
    public Transform[] points;

    protected float Speed;

    private NavMeshAgent _nav;
    private int _destPoint;
    private Animator _anim;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected void Start()
    {
        _nav = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _anim.Play("HumanoidRun");
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        if (!_nav.pathPending && _nav.remainingDistance < 0.5f)
        {            
            GoToNextPoint();
        }
    }

    protected void GoToNextPoint()
    {
        if (points.Length == 0)
        {
            return;
        }
        _nav.destination = points[_destPoint].position;
        _destPoint = (_destPoint + 1) % points.Length;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
    }

    //NOTE: Come back here to add the strafe on X
    private void InputMagnitude()
    {
        

        _anim.SetFloat("InputZ", 1f, 0.0f, Time.deltaTime * 2f);
        //_anim.SetFloat("InputX", 1f, 0.0f, Time.deltaTime * 2f);

        //Calc the Input Magnitude
        Speed = new Vector2(0f, 1f).sqrMagnitude * 5;

        _anim.SetFloat("Speed", Speed, 0.0f, Time.deltaTime);
    }
}
