public interface IBuild
{
    void ConfirmPlacement();
    bool SetResourceType(ResourceType type);
    void SetHit(int amount);
}