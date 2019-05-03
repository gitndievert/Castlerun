using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{   
       
    public float DoorBustHealth = 100f;
    public CastleType CastleType = CastleType.Default;
    public AudioClip AmbientMusic;
    public Color CastleColorHue;    

    [Header("Stats and Owner")]
    public CastleStats CastleStats;
    public Player CastleOwner { get; set; }    

    //List of NPC the castle can offer
    //public Npc[] Npcs;   

    private CastleManager _manager;    


    //Castles will have many passive properties
    //Need something to map materials
    //Need something for icons
    //Need something for ambient noise
    //Need Health

    private void Awake()
    {        
        _manager = CastleManager.Instance;
    }

    private void Start()
    {
        
    }    

    /// <summary>
    /// !!!TODO!!! - For Jessia
    /// Come back later, This will change the color on the rend for the castles
    /// </summary>
    /// <param name="color"></param>
    private void SetColor(Color color)
    {

    }


}
