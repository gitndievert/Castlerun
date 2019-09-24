// ********************************************************************
// CONFIDENTIAL - DO NOT DISTRIBUTE
// COPYRIGHT 2019-2020 Wacky Potato Games, LLC. All Rights Reserved.
// 
// If you send, receive, or use this file for any purpose other than
// internal use by Wacky Potato Games, it is without permission and an act of theft.
// Report any misuse of this file immediately to contact@wackypotato.com
// Misuse or failure to report misuse will subject you to legal action.
// 
// The intellectual and technical concepts contained herein are
// proprietary and are protected by trade secret and/or copyright law.
// Dissemination or reproduction of this material is forbidden.
// ********************************************************************

using SBK.Unity;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : DSingle<GameManager>
{    
    public GameObject PlayerInstance;
    public List<PlayerPad> PlayerPads;    
    
    public Dictionary<int, Player> PlayerList = new Dictionary<int, Player>();

    [SerializeField]
    private readonly int _numOfPlayer = 2;    


    
    protected override void PAwake()
    {
        
    }

    protected override void PDestroy()
    {
     
    }   

    void Start()
    {
        PlayerList.Clear();
        //Commented out for networking test
        StartPlayersTest();
        //StartMusic();
    }    

    public void OnClickQuit()
    {
        Application.Quit();
    }

    private void StartMusic()
    {
        Music.Instance.PlayMusicTrack(1);
    }

    private void StartPlayersTest()
    {
        //PUN Networking pieces
        int spawnIndex = 0;
        int playerNum = spawnIndex + 1;
        var pad = PlayerPads[spawnIndex];

        if (!pad.gameObject.activeSelf)
            throw new System.Exception($"Player Pad is not active for player {playerNum}");

        var character = Instantiate(PlayerInstance, pad.PlayerSpawnPosition, Quaternion.identity);
        var player = character.GetComponent<Player>();
        player.PlayerName = "Krunchy";
        player.PlayerNumber = playerNum;        
        player.PlayerPad = pad;
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
