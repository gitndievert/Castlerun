public interface IResource
{
    int GetHealth();
    ResourceType GetResourceType();
    void SetHit(int amount);
    void PlayHitSounds();
}
