using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 
 * Player networking will be account based, storing castles, wins etc. 
 * 
 * */
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

    public int PlayerNumber;

    [Tooltip("For Testing Changes on Castles")]
    public CastleType CastleType = CastleType.None;

    #region Player Components
    public Companion Companion { get; private set; }
    public Inventory Inventory { get; private set; }
    public int ActorNumber { get; internal set; }    
    public Castle Castle { get; private set; }
    #endregion

    private GameObject _mainHand;
    private GameObject _offHand;    
    private Animator _anim;
    private bool _swinging = false;  

    private void Awake()
    {
        Inventory = GetComponent<Inventory>();
        
        _anim = GetComponent<Animator>();
        Castle = null;
    }

    // Start is called before the first frame update
    private void Start()
    {        
        var castlemanger = CastleManager.Instance;

        //Multiplayer will change this later on
        PlayerNumber = 1;

        //Set the castle Properties
        if (Castle == null)
        {
            //var castleType = CastleType == CastleType.None ? CastleType.Castle3 : CastleType;
            
            Castle = castlemanger.GetCastleByType(CastleType);
            Castle.Level = 2;
            Castle.Experience = 4050f;
        }
        
        CastleManager.Instance.SpawnCastle(Castle, this);


    }

    // Update is called once per frame
    private void Update()
    {        
        //Health = _stat.Health;
        //MoveSpeed = _stat.MoveSpeed;
        //BuildSpeed = _stat.BuildSpeed;        
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

    private void Init()
    {
        //Initialize and check everything on start of player


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
