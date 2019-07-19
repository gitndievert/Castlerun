using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BasicBuild : Build
{
    #region Basic Build Resource Cost
    public const int WOOD_COST = 10;
    public const int ROCK_COST = 20;
    public const int METAL_COST = 30;
    #endregion

    public List<SnapPoints> SnapPoints = new List<SnapPoints>();

    public Material[] Materials;

    //Basic Builds are Instant
    protected override float BuildTime { get { return 0f; } }

    protected override ResourceType ResourceType {  get { return _pickType; } }

    private ResourceType _pickType;
    private Renderer _rend;

    protected override void Awake()
    {
        
    }

    protected override void Start()
    {
        base.Start();
        SnapPoints = GetComponents<SnapPoints>().ToList();
        _rend = GetComponent<Renderer>();        
    }

    public override bool SetResourceType(ResourceType type)
    {
        _pickType = type;
        switch (type)
        {
            case ResourceType.Wood:
                PlacementCost = WOOD_COST;                
                break;
            case ResourceType.Rock:
                PlacementCost = ROCK_COST;
                break;
            case ResourceType.Metal:
                PlacementCost = METAL_COST;
                break;
            default:
                PlacementCost = 0;
                return false;
        }

        UIManager.Instance.Messages.text = $" Resource {type.ToString()}";

        return true;
    }

    public Vector3[] SnapPointPos
    {
        get { return SnapPoints.Select(a => a.transform.position).ToArray(); }
    }

    protected override void OnCollisionEnter(Collision col)
    {
        var colObj = col.gameObject;
        switch (colObj.tag)
        {
            default:
                return;            
            case "Projectile":
            case "Smasher":
                //Random change on damage??
                int damage = col.gameObject.GetComponent<IDamager>().GetDamage();
                SetHit(damage);
                break;
            case "Player":
                if (!_isPlaced) return;
                break;
        }
    }

    /*public Transform GetCloseSnapByBuild(BasicBuild collidingbuild)
    {
        foreach (SnapPoints point in SnapPoints)
        {
            if (point.Snapped) continue;            
            foreach(var colpoint in collidingbuild.SnapPoints)
            {
                float dist = Vector3.Distance(point.Position, colpoint.Position);
                if (dist < 1f) return colpoint.PointTransform;              
            }          
        }       

        return null;
    }*/
}
