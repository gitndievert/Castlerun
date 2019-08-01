using UnityEngine;

public enum WarpDirection
{
    North,
    South,
    East,
    West
}

[RequireComponent(typeof(Collider))]
public class RoomPortal : MonoBehaviour
{
    public Vector3 Destination;
    public Transform DestinationTransform;
    public WarpDirection WarpOutDirection = WarpDirection.North;
    public bool Teleported;

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
        if (col.transform.tag == "Player" && !Teleported)
        {
            //CC On Player will jack with position, need to disable and re-enable
            col.transform.GetComponent<CharacterController>().enabled = false;

            if (DestinationTransform != null)
            {                
                col.transform.position = DestinationTransform.position;            
            }
            else
            {
                col.transform.position = Destination;
            }

            //Rotate(col.transform);
            col.transform.GetComponent<CharacterController>().enabled = true;
            Teleported = true;
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
