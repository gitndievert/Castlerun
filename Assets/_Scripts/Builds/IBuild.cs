using UnityEngine;

public interface IBuild
{
    void ConfirmPlacement();
    bool SetResourceType(ResourceType type);
    void SetHit(int amount);
    Rigidbody RigidBody { get; }
    bool Locked { get; }
    void Lock(bool lockit);
}