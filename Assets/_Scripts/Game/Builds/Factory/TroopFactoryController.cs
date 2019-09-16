using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopFactoryController : MonoBehaviour
{
    public List<TroopFactory> Factories;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Builds are triggers on controller like this:
    //1: Set the global build mode = Global.BuildMode;
    //2: _placementController.LoadObject(_plans.Barracks, true);
}
