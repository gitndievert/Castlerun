using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BasicBuild : Build
{
    public bool Locked;

    public List<SnapPoints> SnapPoints = new List<SnapPoints>();

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
            foreach(var s in snap)
            {
                SnapPoints.Add(new SnapPoints(s.transform));
            }
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

    public Vector3[] SnapPointPos
    {
        get { return SnapPoints.Select(a => a.Position).ToArray(); }
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
                    
                    
                    /*var snapTransform = GetCloseSnapByBuild(build);
                    if(snapTransform)
                    {
                        Debug.Log(snapTransform.gameObject.name);
                        snapTransform.position = transform.position;                       
                    }*/
                    
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

    public Transform GetCloseSnapByBuild(BasicBuild collidingbuild)
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
    }
}

public class SnapPoints
{
    public bool Snapped { get; set; }
    public Transform PointTransform { get; set; }

    public SnapPoints(Transform point)
    {
        PointTransform = point;
        Snapped = false;
    }

    public Vector3 Position
    {
        get { return PointTransform.position;  }
    }
    public Vector3 LocalPosition
    {
        get { return PointTransform.localPosition; }
    }

    public GameObject GameObject
    {
        get { return PointTransform.gameObject; }
    }
}

