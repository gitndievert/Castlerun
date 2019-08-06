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

public class RoomPortal : MonoBehaviour
{    
    public RoomPortal DestinationPortal;
    [Space(10)]
    public WarpDirection WarpOutDirection = WarpDirection.North;
    [Space(10)]
    public WarpType WarpType = WarpType.Entrance;

    /// <summary>
    /// Gets/Sets the Player Position going into the entrance
    /// </summary>
    public Vector3 PlayerPositionInbound { get; set; }

    private Collider _col;    
    
    private void Awake()
    {
        _col = GetComponent<Collider>();
        _col.isTrigger = true;        
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
            //Set Player Position
            DestinationPortal.PlayerPositionInbound = col.transform.position;
            //Rotate(col.transform);
            col.transform.GetComponent<CharacterController>().enabled = true;                     
        }
        else if(col.transform.tag == "Player" && WarpType == WarpType.Exit)
        {
            col.transform.position = new Vector3(transform.position.x, PlayerPositionInbound.y, transform.position.z);
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