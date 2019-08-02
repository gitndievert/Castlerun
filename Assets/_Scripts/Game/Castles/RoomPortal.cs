using UnityEngine;
using SBK.Unity;

public enum WarpDirection
{
    North,
    South,
    East,
    West
}

public enum WarpType
{
    Entrance,
    Exit
}

[RequireComponent(typeof(Collider))]
public class RoomPortal : MonoBehaviour
{    
    public RoomPortal DestinationPortal;
    [Space(10)]
    public WarpDirection WarpOutDirection = WarpDirection.North;
    [Space(10)]
    public WarpType WarpType = WarpType.Entrance;

    private Collider _col;
    private Castle _attachedCastle;      
    
    private void Awake()
    {
        _col = GetComponent<Collider>();
        _col.isTrigger = true;
        _attachedCastle = GetComponentInParent<Castle>();
        _attachedCastle.InnerCastle.SetActive(false);
    }
   
    /// <summary>
    /// Teleport the player to selected destination
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerEnter(Collider col)
    {        
        if(col.transform.tag == "Player" && DestinationPortal != null 
            && WarpType == WarpType.Entrance)
        {
            //CC On Player will jack with position, need to disable and re-enable
            col.transform.GetComponent<CharacterController>().enabled = false;
            col.transform.position = DestinationPortal.transform.position;    
            //Rotate(col.transform);
            col.transform.GetComponent<CharacterController>().enabled = true;                     
        }
    }
    
    private void Rotate(Transform target)
    {
        float yDir = 0f;
        switch (WarpOutDirection)
        {            
            case WarpDirection.East:
                yDir = 90f;
                break;
            case WarpDirection.South:
                yDir = 180f;
                break;
            case WarpDirection.West:
                yDir = 270f;
                break;
        }

        target.Rotate(0, yDir, 0);
    }
}