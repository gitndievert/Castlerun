using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyBuild : Build
{
    public ArmyBuildType ArmyOutputType = ArmyBuildType.Infantry;

    public GameObject Troop;

    /// <summary>
    /// The time it takes to build the structure
    /// </summary>
    public float ConstructionTime;

    /// <summary>
    /// Time to train each troop
    /// </summary>
    public float TrainingTime;

    /// <summary>
    /// The number of troops trained on each training pass
    /// </summary>
    public int NumberToTrain = 1;


    private bool _IsBuilding = false;
    private ResourceType _pickType;
    

    //This seems dumb now
    protected override float BuildTime => ConstructionTime;
    protected override ResourceType ResourceType => ResourceType.Wood;

    public override bool SetResourceType(ResourceType type)
    {
        _pickType = type;
        switch (type)
        {
            case ResourceType.Wood:
                PlacementCost = 10;
                break;
            case ResourceType.Rock:
                PlacementCost = 10;
                break;
            case ResourceType.Metal:
                PlacementCost = 20;
                break;
            default:
                PlacementCost = 0;
                return false;
        }

        return true;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void BuildTroops()
    {

    }

    public void StopBuild()
    {

    }
    


}
