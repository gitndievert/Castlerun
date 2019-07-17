using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBuild : Build
{   
    
    //Basic Builds are Instant
    protected override float BuildTime { get { return 0f; } }

    protected override ResourceType ResourceType {  get { return _pickType; } }

    private ResourceType _pickType;

    protected override void Awake()
    {
        //Do not load audiosource  
    }
      
    
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

}
