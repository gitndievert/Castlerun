using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public const float HARVEST_DISTANCE = 17f;

    private const int MAX_WOOD = 500;
    private const int MAX_ROCK = 500;
    private const int MAX_METAL = 500;
    private const int MAX_GEMS = 200;

    [Range(0,MAX_WOOD)]
    public int WoodCount = 0;
    [Range(0, MAX_ROCK)]
    public int RockCount = 0;
    [Range(0, MAX_METAL)]
    public int MetalCount = 0;
    [Range(0, MAX_GEMS)]
    public int GemsCount = 0;

    public static int MaxWood = 500;
    public static int MaxRock = 500;
    public static int MaxMetal = 500;
    public static int MaxGems = 200;

    public bool ResetOnStart = false;

    public bool IsWoodFull { get { return WoodCount >= MaxWood; } }
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
        if(ResetOnStart) ResetAll();
        //To Trigger Events
        //EventManager.StartListening("Resource", ResourceMessage);
        //EventManager.TriggerEvent("Resource");
        //public void ResourceMessage(){}
    }

    private void OnDestroy()
    {
        //EventManager.StopListening("Resource", ResourceMessage);
    }

    // Update is called once per frame
    void Update()
    {
        _ui.WoodText.text = WoodCount.ToString();
        _ui.RockText.text = RockCount.ToString();
        _ui.MetalText.text = MetalCount.ToString();
        _ui.GemsText.text = GemsCount.ToString();        
    }
       
    public void ResetAll()
    {
        WoodCount = 0;
        RockCount = 0;
        MetalCount = 0;
        GemsCount = 0;
    }
       
    public int GetCount(ResourceType type)
    {
        switch(type)
        {
            case ResourceType.Wood:
                return WoodCount;
            case ResourceType.Rock:
                return RockCount;
            case ResourceType.Metal:
                return MetalCount;
            case ResourceType.Gems:
                return GemsCount;
            default:
                return 0;
        }
    }

    public bool IsFull(ResourceType type)
    {
        switch(type)
        {
             case ResourceType.Wood:
                return IsWoodFull;
            case ResourceType.Rock:
                return IsRockFull;
            case ResourceType.Metal:
                return IsMetalFull;
            case ResourceType.Gems:
                return IsGemsFull;
            default:
                return false;
        }
    }
        
    public void Set(ResourceType type, int amount = 0)
    {
        switch (type)
        {
            case ResourceType.Wood:
                if ((amount + WoodCount) <= MaxWood)
                {
                    WoodCount += amount;                    
                }
                else
                {
                    _ui.Messages.text = "You cannot store anymore wood";
                    return;
                }                
                break;
            case ResourceType.Rock:
                if ((amount + RockCount) <= MaxRock)
                {
                    RockCount += amount;                    
                }
                else
                {
                    _ui.Messages.text = "You cannot store anymore rock";
                    return;
                }                
                break;
            case ResourceType.Metal:
                if ((amount + MetalCount) <= MaxMetal)
                {
                    MetalCount += amount;                    
                }
                else
                {
                    _ui.Messages.text = "You cannot store anymore metal";
                    return;
                }                
                break;
            case ResourceType.Gems:
                if ((amount + GemsCount) <= MaxGems)
                {
                    GemsCount += amount;                    
                }
                else
                {
                    _ui.Messages.text = "You cannot store anymore gems";
                    return;

                }                
                break;
        }

        if (amount > 0)
        {
            _ui.Messages.text = $"You gathered {amount} {type.ToString()}";
        }
        else
        {
            _ui.Messages.text = $"You used {Mathf.Abs(amount)} {type.ToString()}";
        }



    }   

   
   
}
