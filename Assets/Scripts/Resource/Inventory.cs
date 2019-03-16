using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public const float HARVEST_DISTANCE = 2.0f;    

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
       
        
    public void Set(ResourceType type, int amount = 0)
    {        
        switch (type)
        {
            case ResourceType.Wood:
                if ((amount + WoodCount) <= MaxWood)
                {
                    WoodCount += amount;
                    _ui.Messages.text = $"You gathered {amount} wood";                    
                }
                else
                {
                    _ui.Messages.text = "You cannot store anymore wood";
                }                
                break;
            case ResourceType.Rock:
                if ((amount + RockCount) <= MaxRock)
                {
                    RockCount += amount;
                    _ui.Messages.text = $"You gathered {amount} rock";
                }
                else
                {
                    _ui.Messages.text = "You cannot store anymore rock";
                }                
                break;
            case ResourceType.Metal:
                if ((amount + MetalCount) <= MaxMetal)
                {
                    MetalCount += amount;
                    _ui.Messages.text = $"You gathered {amount} metal";
                }
                else
                {
                    _ui.Messages.text = "You cannot store anymore metal";
                }                
                break;
            case ResourceType.Gems:
                if ((amount + GemsCount) <= MaxGems)
                {
                    GemsCount += amount;
                    _ui.Messages.text = $"You gathered {amount} gems";
                }
                else
                {
                    _ui.Messages.text = "You cannot store anymore gems";
                }                
                break;
        }
     
    }   

   
   
}
