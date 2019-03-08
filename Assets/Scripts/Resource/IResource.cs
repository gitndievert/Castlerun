public interface IResource
{
    int GetDurability();
    ResourceType GetResourceType();
    void SetHit(int amount);
}
