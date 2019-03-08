using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems : BaseResource
{
    protected override void Awake()
    {
        ResourceType = ResourceType.Gems;
        Value = 1;
    }
      

    // Update is called once per frame
    void Update()
    {
        
    }
}
