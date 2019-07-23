using UnityEngine;

public interface IBuild
{
    void ConfirmPlacement();
    bool SetResourceType(ResourceType type);
    void SetHit(int amount);        
    void SetPlayer(Player player);
    int PlacementCost { get; set; }
}