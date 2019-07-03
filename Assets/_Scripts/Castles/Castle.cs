using UnityEngine;

public class Castle : MonoBehaviour
{
    [Range(50f,1000f)]
    public float DoorBustHealth = 100f;
    public CastleType CastleType = CastleType.Default;    
    public Player CastleOwner { get; set; }    
       


}
