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

    private void Awake()
    {
        Inventory = GetComponent<Inventory>();
        StatsModifier = GetComponent<StatModifier>();
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

        if (Input.GetMouseButtonDown(0))
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

                        Debug.Log(hit.transform.gameObject.name);
                        Debug.Log(durability + " " + rt.ToString());
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        
    }



}
