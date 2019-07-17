using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BasicBuild : Build
{
    protected List<Transform> SnapPoints = new List<Transform>();

    //Basic Builds are Instant
    protected override float BuildTime { get { return 0f; } }

    protected override ResourceType ResourceType {  get { return _pickType; } }

    private ResourceType _pickType;

    protected override void Awake()
    {
        //Do not load audiosource  
    }

    protected override void Start()
    {
        base.Start();
        var snap = GetComponentsInChildren<Transform>().Skip(1).ToList();
        if (snap.Count > 0 && snap != null)
        {
            SnapPoints = snap;
        }
    }

    public override bool SetResourceType(ResourceType type)
    {
        _pickType = type;
        switch (type)
        {
            case ResourceType.Wood:
                PlacementCost = 10;
                break;
            case ResourceType.Rock:
                PlacementCost = 10;
                break;
            case ResourceType.Metal:
                PlacementCost = 20;
                break;
            default:
                PlacementCost = 0;
                return false;
        }

        return true;
    }

    protected override void OnCollisionEnter(Collision col)
    {
        var colObj = col.gameObject;
        switch (colObj.tag)
        {
            default:
                return;
            case "Build":
                Debug.Log("Hitting the Target");
                var build = colObj.GetComponent<BasicBuild>();
                if(build)
                {
                    var bah = GetCloseSnapByBuild(build);
                    //Debug.Log(bah.transform.position);
                }                
                break;
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

    public Transform GetCloseSnapByBuild(BasicBuild build)
    {
        var connectingBuildPos = build.SnapPoints;
        foreach (var point in connectingBuildPos)
        {
            Debug.Log(point.position);
        }

        return null;
        /*foreach (var point in SnapPoints)
        {
            var pos = point.position;            
        }*/

    }

}
