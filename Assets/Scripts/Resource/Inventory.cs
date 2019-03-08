using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int WoodCount;

    public int RockCount;

    public int MetalCount;

    public int GemCount;

    public static int MaxWood = 500;

    public static int MaxRock = 500;

    public static int MaxMetal = 500;

    public static int MaxGems = 200;

    public bool IsWoodFull {  get { return WoodCount >= MaxWood; } }

    public bool IsRockFull { get { return RockCount >= MaxRock; } }

    public bool IsMetalFull { get { return MetalCount >= MaxMetal; } }

    public bool IsGemsFull { get { return GemCount >= MaxGems; } }

    // Start is called before the first frame update
    void Start()
    {
        ResetAll();        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);            

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider != null && hit.transform.tag == "Resource")
                {
                    var resource = hit.transform.GetComponent<IResource>();                    
                    int durability = resource.GetDurability();
                    ResourceType rt = resource.GetResourceType();                    
                    switch(rt)
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
                    //hit.collider.enabled = false;
                    Debug.Log(hit.transform.gameObject.name);
                    Debug.Log(durability + " " + rt.ToString());
                }
            }
        }
    }

    public void ResetAll()
    {
        WoodCount = 0;
        RockCount = 0;
        MetalCount = 0;
        GemCount = 0;
    }
       
        
    public void Set(ResourceType type, int amount = 0)
    {
        switch(type)
        {
            case ResourceType.Wood:
                if((amount + WoodCount) <= MaxWood)
                    WoodCount = amount;
                //else
                    //Trigger that message event back to user
                break;
            case ResourceType.Rock:
                if ((amount + RockCount) <= MaxRock)
                    RockCount = amount;
                //else
                    //Trigger that message event back to user
                break;
            case ResourceType.Metal:
                if ((amount + MetalCount) <= MaxMetal)
                    MetalCount = amount;
                //else
                    //Trigger that message event back to user
                break;
            case ResourceType.Gems:
                if ((amount + GemCount) <= MaxGems)
                    GemCount = amount;
                //else
                    //Trigger that message event back to user
                break;
        }   
    }
}
