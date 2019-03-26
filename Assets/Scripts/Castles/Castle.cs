using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    const float MaxExp = 124360f;

    [Range(1,3)]
    public int Level = 1;
    public float DoorBustHealth = 100f;
    public CastleType CastleType = CastleType.Default;
    public AudioClip AmbientMusic;
    public Color CastleColorHue;
    public float Experience;
    public CastleStats CastleStats;
    public Player CastleOwner { get; set; }

    public float[] ExpLevels = { 25000f, 80000f, MaxExp };

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
