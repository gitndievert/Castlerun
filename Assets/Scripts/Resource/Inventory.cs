using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    const float HARVEST_DISTANCE = 2.0f;

    public int WoodCount = 0;
    public int RockCount = 0;
    public int MetalCount = 0;
    public int GemsCount = 0;

    public static int MaxWood = 500;
    public static int MaxRock = 500;
    public static int MaxMetal = 500;
    public static int MaxGems = 200;

    public bool IsWoodFull {  get { return WoodCount >= MaxWood; } }
    public bool IsRockFull { get { return RockCount >= MaxRock; } }
    public bool IsMetalFull { get { return MetalCount >= MaxMetal; } }
    public bool IsGemsFull { get { return GemsCount >= MaxGems; } }

    private InventoryUI _ui;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        _ui = UIManager.Instance.InventoryUIPanel;
        ResetAll();        
    }    

    // Update is called once per frame
    void Update()
    {
        _ui.WoodText.text = WoodCount.ToString();
        _ui.RockText.text = RockCount.ToString();
        _ui.MetalText.text = MetalCount.ToString();
        _ui.GemsText.text = GemsCount.ToString();

        if (Input.GetMouseButtonDown(0))
        {            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);            

            if (Physics.Raycast(ray, out RaycastHit hit))
            {                
                if (TransformHelper.DistanceLess(hit.transform, transform, HARVEST_DISTANCE))
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
       
    public void ResetAll()
    {
        WoodCount = 0;
        RockCount = 0;
        MetalCount = 0;
        GemsCount = 0;
    }
       
        
    public void Set(ResourceType type, int amount = 0)
    {
        switch(type)
        {
            case ResourceType.Wood:
                if((amount + WoodCount) <= MaxWood)
                    WoodCount += amount;
                //else
                    //Trigger that message event back to user
                break;
            case ResourceType.Rock:
                if ((amount + RockCount) <= MaxRock)
                    RockCount += amount;
                //else
                    //Trigger that message event back to user
                break;
            case ResourceType.Metal:
                if ((amount + MetalCount) <= MaxMetal)
                    MetalCount += amount;
                //else
                    //Trigger that message event back to user
                break;
            case ResourceType.Gems:
                if ((amount + GemsCount) <= MaxGems)
                    GemsCount += amount;
                //else
                    //Trigger that message event back to user
                break;
        }   
    }

   
}
