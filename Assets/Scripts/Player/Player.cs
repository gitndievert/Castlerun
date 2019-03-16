using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{ 
    
    #region BaseStats
    public float Health;
    public float MoveSpeed;
    public float BuildSpeed;
    #endregion

    private string _playerName;

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

    #region Player Components
    public Companion Companion { get; private set; }
    public Inventory Inventory { get; private set; }
    public int ActorNumber { get; internal set; }
    public StatModifier StatsModifier;
    #endregion

    private GameObject _mainHand;
    private GameObject _offHand;
    private Stats _stat;
    private Animator _anim;
    private bool _swinging = false;

    private void Awake()
    {
        Inventory = GetComponent<Inventory>();
        StatsModifier = GetComponent<StatModifier>();
        _anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _stat = StatModifier.CurrentStats;        
    }

    // Update is called once per frame
    private void Update()
    {        
        Health = _stat.Health;
        MoveSpeed = _stat.MoveSpeed;
        BuildSpeed = _stat.BuildSpeed;        
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {            
            if (!_swinging)
            {             
                _swinging = true;
                _anim.SetBool("Swing", true);
            }

        }
    }

    public void Swing()
    {        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (TransformHelper.DistanceLess(hit.transform, transform, Inventory.HARVEST_DISTANCE))
            {
                if (hit.collider != null && hit.transform.tag == "Resource")
                {
                    var resource = hit.transform.GetComponent<IResource>();
                    int durability = resource.GetDurability();
                    ResourceType rt = resource.GetResourceType();
                    switch (rt)
                    {
                        case ResourceType.Wood:
                            resource.SetHit(50);
                            break;
                        case ResourceType.Rock:
                            resource.SetHit(25);
                            break;
                        case ResourceType.Metal:
                            resource.SetHit(10);
                            break;
                        case ResourceType.Gems:
                            resource.SetHit(5);
                            break;
                    }

                    //NATE NOTE: Come back
                    //resource.PlayHitSounds();
                                       
                    Debug.Log(durability + " " + rt.ToString());
                }
            }
        }
    }
    
    public void SwingStop()
    {
        _swinging = false;
        _anim.SetBool("Swing", false);
    }

    private void OnDestroy()
    {
        
    }



}
