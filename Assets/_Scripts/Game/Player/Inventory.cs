// ********************************************************************
// CONFIDENTIAL - DO NOT DISTRIBUTE
// COPYRIGHT 2019-2020 Wacky Potato Games, LLC. All Rights Reserved.
// 
// If you send, receive, or use this file for any purpose other than
// internal use by Wacky Potato Games, it is without permission and an act of theft.
// Report any misuse of this file immediately to contact@wackypotato.com
// Misuse or failure to report misuse will subject you to legal action.
// 
// The intellectual and technical concepts contained herein are
// proprietary and are protected by trade secret and/or copyright law.
// Dissemination or reproduction of this material is forbidden.
// ********************************************************************

using Photon.Pun;
using UnityEngine;

public class Inventory : MonoBehaviourPun
{   
    const int MAX_WOOD = 500;
    const int MAX_ROCK = 500;
    const int MAX_METAL = 500;
    const int MAX_GOLD = 100000;

    [Range(0,MAX_WOOD)]
    public int WoodCount = 0;
    [Range(0, MAX_ROCK)]
    public int RockCount = 0;
    [Range(0, MAX_METAL)]
    public int MetalCount = 0;
    [Range(0, MAX_GOLD)]
    public int GoldCount = 0;

    [Header("Main Hand Weapons")]
    [SerializeField]
    private GameObject PrimaryHand;
    [SerializeField]
    private GameObject SecondaryHand;

    [Header("Weapons")]
    public GameObject Weapon;
    public GameObject Shield;

    public bool ResetOnStart = false;

    public bool IsWoodFull { get { return WoodCount >= MAX_WOOD; } }
    public bool IsRockFull { get { return RockCount >= MAX_ROCK; } }
    public bool IsMetalFull { get { return MetalCount >= MAX_METAL; } }
    public bool IsGoldFull { get { return GoldCount >= MAX_GOLD; } }

    private InventoryUI _ui;    
    private PhotonView _pv;
    

    // Start is called before the first frame update
    void Start()
    {
        _ui = UIManager.Instance.InventoryUIPanel;
        if(ResetOnStart) ResetAll();
        //Set up hands
        ResetHands();
        _pv = GetComponent<PhotonView>();

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
        _ui.GoldText.text = GoldCount.ToString();        
    }
       
    public void ResetAll()
    {
        WoodCount = 0;
        RockCount = 0;
        MetalCount = 0;
        GoldCount = 0;
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
            case ResourceType.Gold:
                return GoldCount;
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
            case ResourceType.Gold:
                return IsGoldFull;
            default:
                return false;
        }
    }

    /// <summary>
    /// Pass in the cost summary of a building or troop
    /// </summary>
    /// <param name="costs">Costs attached to building or troop</param>
    public void Set(Costs costs)
    {
        foreach(Cost cost in costs.CostFactors)
        {
            Set(cost.Resource, -cost.Amount);
        }
    }
        
    /// <summary>
    /// Add or subtract amount in Inventory
    /// </summary>
    /// <param name="type">Resouce Type</param>
    /// <param name="amount">Amount of Resource</param>
    public void Set(ResourceType type, int amount = 0)
    {
        if ((_pv != null && !_pv.IsMine) || Global.DEVELOPER_MODE)
        {
            switch (type)
            {
                case ResourceType.Wood:
                    if ((amount + WoodCount) <= MAX_WOOD)
                    {
                        WoodCount += amount;
                    }
                    else
                    {
                        Global.Message("You cannot store anymore wood");
                        return;
                    }
                    break;
                case ResourceType.Rock:
                    if ((amount + RockCount) <= MAX_ROCK)
                    {
                        RockCount += amount;
                    }
                    else
                    {
                        Global.Message("You cannot store anymore rock");
                        return;
                    }
                    break;
                case ResourceType.Metal:
                    if ((amount + MetalCount) <= MAX_METAL)
                    {
                        MetalCount += amount;
                    }
                    else
                    {
                        Global.Message("You cannot store anymore metal");
                        return;
                    }
                    break;
                case ResourceType.Gold:
                    if ((amount + GoldCount) <= MAX_GOLD)
                    {
                        GoldCount += amount;
                    }
                    else
                    {
                        Global.Message("You cannot store anymore gems");
                        return;

                    }
                    break;
            }

            if (amount > 0)
            {
                Global.Message($"You gathered {amount} {type.ToString()}");
            }
            else
            {
                Global.Message($"You used {Mathf.Abs(amount)} {type.ToString()}");
            }
        }
    }   

    public void ResetHands()
    {
        EmptyHand("primary");
        EmptyHand("secondary");
        PrimaryHand = Weapon;
        PrimaryHand.SetActive(true);
        SecondaryHand = Shield;
        SecondaryHand.SetActive(true);
    }

    public void SwitchHand(GameObject newHand, string hand)
    {
        if (hand == "primary")
        {
            PrimaryHand.SetActive(false);
            PrimaryHand = newHand;
            PrimaryHand.SetActive(true);
        }
        else if (hand == "secondary")
        {
            SecondaryHand.SetActive(false);
            SecondaryHand = newHand;
            SecondaryHand.SetActive(true);
        }
    }

    public void EmptyHand(string hand)
    {
        if (hand == "primary" && PrimaryHand != null)
        {
            PrimaryHand.SetActive(false);
        }
        else if (hand == "secondary" && SecondaryHand != null)
        {
            SecondaryHand.SetActive(false);
        }
    }


}
