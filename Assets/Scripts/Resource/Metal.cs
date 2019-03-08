using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : BaseResource
{
    protected override void Awake()
    {
        ResourceType = ResourceType.Metal;
        Durability = 150;
        Value = 10;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
