using System;
using UnityEngine;

[Serializable]
public class UpgradeBuildSlots
{
    public UpgradeBuildSlot[] UpgradeSlots;
}

[Serializable]
public class UpgradeBuildSlot
{
    public string Bonus;
    public int Amount;
    public Sprite Icon;

    public void Upgrade(Player player)
    {

    }

    public void Upgrade(TroopFactory factory)
    {

    }

    public void Upgrade(Troop troop)
    {

    }
}