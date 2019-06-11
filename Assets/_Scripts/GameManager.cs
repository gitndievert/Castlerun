using SBK.Unity;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject CameraRig = null;
    public GameObject PlayerInstance;
    public PlayerPad[] PlayerPads;    

    private int _numOfPlayer = 2;     
  

    void Awake()
    {
        //FOR TESTING        
        PlayerPads = gameObject.GetComponentsInChildren<PlayerPad>();                              
    }

    void Start()
    {
        if (CameraRig == null)
            throw new System.Exception("Must have a Camera Rig Hooked Up on the Game Manager");
        StartPlayersTest();
        Music.Instance.PlayMusicTrack(1);
    }    

    private void StartPlayersTest()
    {
        //PUN Networking pieces
        if (Player.LocalPlayerInstance == null)
        {
            int spawnIndex = 0;

            var character = Instantiate(PlayerInstance, PlayerPads[spawnIndex].PlayerSpawnPosition, Quaternion.identity);
            var player = character.GetComponent<Player>();
            
            player.PlayerNumber = spawnIndex + 1; //total hack for player number right now

            CameraRig.GetComponent<CameraRotate>().target = character.transform;            

        }       
        
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
