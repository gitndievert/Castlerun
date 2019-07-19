using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SnapDirection
{
    None,
    Top,
    Bottom,
    Left,
    Right
}

[RequireComponent(typeof(BoxCollider))]
public class SnapPoints : MonoBehaviour
{
    public bool Snapped;    
    public SnapDirection SnapDirection = SnapDirection.None;

    private Collider _col;
    private IBuild _build;

    protected void Awake()
    {
        Snapped = false;
        _col = GetComponent<Collider>();
        _col.isTrigger = true;
        _build = transform.parent.GetComponent<BasicBuild>();
    }
  
    protected void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == Global.SNAP_POINT_TAG)
        {
            var snap = col.GetComponent<SnapPoints>();

            Debug.Log($"We got a hit on {snap.ToString()}");
            if (snap.SnapDirection == SnapDirection.None) return;
            switch(snap.SnapDirection)
            {
                case SnapDirection.Left:
                case SnapDirection.Right:
                    if(CheckSnapDirections(snap.SnapDirection, SnapDirection))
                    {
                        //Commented out for now
                        //_build.Lock(true);
                    }
                    break;
                case SnapDirection.Top:
                    break;
                case SnapDirection.Bottom:
                    break;
                default:
                    break;

            }
        }
    }
       
    private bool CheckSnapDirections(SnapDirection sideone, SnapDirection sidetwo)
    {        
        return sideone != sidetwo;
    }

    public override string ToString()
    {
        return SnapDirection.ToString();
    }

}
