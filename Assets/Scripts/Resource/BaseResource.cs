using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class BaseResource : BasePrefab, IResource
{
    public ResourceType ResourceType;
    public GameObject BustResourceObj;

    public int GetDurability()
    {
        return Durability;
    }

    public ResourceType GetResourceType()
    {
        return ResourceType;
    }

    public void SetHit(int amount)
    {
        if (Durability <= 0) BustResource();
        Durability -= amount;
    }

    protected int Durability = 100;
    protected int Value = 10;

    protected override void Awake()
    {
        base.Awake();
        tag = "Resource";
    }

    protected override void Start()
    {
        base.Start();
    }

    protected void BustResource()
    {
        if (BustResourceObj != null)
        {
            var bust = Instantiate(BustResourceObj, new Vector3(transform.position.x,
                transform.position.y + 2f, transform.position.z), Quaternion.identity);
            //Sounds
            //Effects
            var bustValues = bust.GetComponent<Bust>();
            bustValues.SetValues(ResourceType, Value);
            Destroy(gameObject);
        }
    }



}
