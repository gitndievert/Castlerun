using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyBuild : Build
{    
    protected override float BuildTime => ConstructionTime;

    protected override ResourceType ResourceType => ResourceType.Wood;

    public ArmyBuildType ArmyOutputType = ArmyBuildType.Infantry;
    
    public GameObject Troop;

    [Tooltip("Turn on the Builder")]
    [Header("Turn On Builder")]
    public bool EnableBuilder = false;


    /// <summary>
    /// The time it takes to build the structure
    /// </summary>
    public float ConstructionTime;

    /// <summary>
    /// Time to train each troop
    /// </summary>
    public float TrainingTime;

    /// <summary>
    /// This is the start timer for the initial Troops. Hi Jessia
    /// </summary>
    public float StartTime;

    /// <summary>
    /// The number of troops trained on each training pass
    /// </summary>
    public int NumberToTrain = 1;


    private bool _IsBuilding = false;
    private ResourceType _pickType;


    private void OnMouseDown()
    {
        Debug.Log("Clicked on the barracks");
    }


    public override bool SetResourceType(ResourceType type)
    {
        _pickType = type;
        switch (type)
        {
            case ResourceType.Wood:
                PlacementCost = 10;
                break;            
            default:                
                return false;
        }

        return true;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        if (Health == 0) Health = 40;
        MaxHealth = Health;
    }

    // Update is called once per frame
    void Update()
    {
        if(EnableBuilder && !_IsBuilding)
        {
            BuildTroops();
            _IsBuilding = true;
            Debug.Log("Building Turned on for Barracks");
        }

        if(!EnableBuilder && _IsBuilding)
        {
            StopBuild();
            Debug.Log("Building Turned OFF");
        }
    }


    public void BuildTroops()
    {
        if (Troop == null) return;
        InvokeRepeating("Build", StartTime, TrainingTime);
    }

    public void StopBuild()
    {
        CancelInvoke();
        _IsBuilding = false;
    }
    

    private void Build()
    {
        for(int i = 0; i < NumberToTrain; i++)
        {
            Instantiate(Troop, transform.position + (Vector3.forward * 2), Quaternion.identity);
        }
    }


}
