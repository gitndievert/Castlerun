using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BuildArea : MonoBehaviour
{
    public bool CanBuild { get; private set; }
        
    private const string BUILDAREA_TAG = "BuildArea";

    [Header("Collider Colors")]
    public Color ValidSpot = Color.green;
    public Color InvalidSpot = Color.red;

    private Collider _col;
    private Renderer _rend;
    
    // Start is called before the first frame update
    void Start()
    {
        _col = GetComponent<Collider>();
        _col.isTrigger = true;
        _rend = GetComponent<Renderer>();
        transform.tag = BUILDAREA_TAG;
        //Initialize Builder
        CanBuild = true;
        SetPlaneColor(ValidSpot);
    }

    public void ShowPlane(bool show)
    {        
        _rend.enabled = show;
    }

    public void TogglePlane()
    {
        _rend.enabled = !_rend.enabled;
    }

    public void SetPlaneColor(Color color)
    {
        if(_rend.enabled)
            _rend.material.color = color;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != BUILDAREA_TAG) return;
        CanBuild = false;
        SetPlaneColor(InvalidSpot);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.transform.tag != BUILDAREA_TAG) return;
        CanBuild = true;
        SetPlaneColor(ValidSpot);
    }

    
   
}
