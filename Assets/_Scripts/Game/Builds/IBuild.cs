using UnityEngine;

public interface IBuild
{
    void ConfirmPlacement();
    bool SetResourceType(ResourceType type);
    void SetHit(int amount);    
    bool Locked { get; }
    void Lock(bool lockit);
    void SetPlayer(Player player);
    int PlacementCost { get; set; }
}