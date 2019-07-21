using SBK.Unity;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : DSingle<GameManager>
{    
    public GameObject PlayerInstance;
    public PlayerPad[] PlayerPads;

    public Plans Plans { get; set; }
    
    public Dictionary<int, Player> PlayerList = new Dictionary<int, Player>();

    [SerializeField]
    private readonly int _numOfPlayer = 2;

    protected override void PAwake()
    {
        //FOR TESTING        
        PlayerPads = gameObject.GetComponentsInChildren<PlayerPad>();
        Plans = GetComponent<Plans>();
    }

    protected override void PDestroy()
    {
     
    }   

    void Start()
    {
        PlayerList.Clear();
        StartPlayersTest();
        Music.Instance.PlayMusicTrack(1);
    }    

    private void StartPlayersTest()
    {
        //PUN Networking pieces
        int spawnIndex = 0;

        var character = Instantiate(PlayerInstance, PlayerPads[spawnIndex].PlayerSpawnPosition, Quaternion.identity);
        var player = character.GetComponent<Player>();
        int playerNum = spawnIndex + 1;
        player.Init("Krunchy", playerNum); //Player number is a hack until things are setup            
        PlayerList.Add(playerNum, player);

        /*for (int i = 1; i < _numOfPlayer; i++)
        {
            //Photon Instantiation
            //var character = Instantiate(PlayerInstance, PlayerPads[i - 1].PlayerSpawnPosition, Quaternion.identity);
            
           
            //var player = character.GetComponent<Player>();
            //player.PlayerNumber = i;            

            //Will need to be totally redone with the multiplayer code
            //if (player.PlayerNumber == 1)
            if(photonView.IsMine)
            {
                Transform pt = transform;
                //Having issues with autocam, so removing it
                //CameraRig.GetComponent<AutoCam>().SetTarget(pt);
                CameraRig.GetComponent<CameraRotate>().target = pt;
                //CameraRig.GetComponent<BasicCameraBehaviour>().SetPlayerTarget = pt;
            }
        }*/
    }
}
