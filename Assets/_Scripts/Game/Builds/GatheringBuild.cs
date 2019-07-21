using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringBuild : Build
{
    protected override float BuildTime => ConstructionTime;

    protected override ResourceType ResourceType => ResourceType.Wood;

    /// <summary>
    /// The time it takes to build the structure
    /// </summary>
    public float ConstructionTime;

    public GameObject[] Gatherers;

    [Tooltip("Turn on the Builder")]
    [Header("Turn On Builder")]
    public bool EnableBuilder = false;

    /// <summary>
    /// This is the start timer for the initial Troops. Hi Jessia
    /// </summary>
    public float StartTime;

    /// <summary>
    /// Time to train each troop
    /// </summary>
    public float TrainingTime;

    /// <summary>
    /// The number of troops trained on each training pass
    /// </summary>
    public int NumberToTrain = 1;

    private int _trainedCounter = 0;


    /// <summary>
    /// The number of troops trained on each training pass
    /// </summary>
    public int MaxTrained = 5;


    private ResourceType _pickType;
    private bool _IsBuilding = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        if (Health == 0) Health = 300;
        MaxHealth = Health;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnableBuilder && !_IsBuilding)
        {
            BuildTroops();
            _IsBuilding = true;
            Debug.Log("Building Turned on for Harvesting Peoples");
        }

        if ((!EnableBuilder && _IsBuilding) || _trainedCounter == MaxTrained)
        {
            StopBuild();
            Debug.Log("Max Number of Harvesters Made");
        }
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

    public void BuildTroops()
    {
        if (Gatherers == null) return;
        InvokeRepeating("Build", StartTime, TrainingTime);
    }

    public void StopBuild()
    {
        CancelInvoke();
        _IsBuilding = false;
    }


    private void Build()
    {
        for (int i = 0; i < NumberToTrain; i++)
        {
            var randTroop = (GameObject)Gatherers[Random.Range(0, Gatherers.Length - 1)];            
            Instantiate(randTroop, transform.position + (Vector3.forward * 2), Quaternion.identity);
            _trainedCounter++;
        }
    }
}
