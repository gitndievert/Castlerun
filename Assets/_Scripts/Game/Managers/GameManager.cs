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

using Photon.Pun;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager LocalGameManagerInstance;
    
    /// <summary>
    /// Player Prefab
    /// </summary>
    [Tooltip("The prefab to use for representing the player")]
    public GameObject PlayerInstance;            

    [Header("Spawn Points")]
    public Transform Player1SpawnPoint;
    public Transform Player2SpawnPoint;
    public Transform Player3SpawnPoint;
    public Transform Player4SpawnPoint;

    [Header("Resource Points")]
    public Transform Player1ResourcePoints;
    public Transform Player2ResourcePoints;
    //public Transform Player3ResourcePoints;
    //public Transform Player4ResourcePoints;
    public TextMeshProUGUI Messages;
    public TextMeshProUGUI PlayersConnected;

    private void Awake()
    {
        //Flip to Singleton
        LocalGameManagerInstance = this;
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {   
        
        if(Global.DeveloperMode)
        {
            StartPlayersTest();
            //StartMusic();
            return;
        }
        
        // in case we started this demo with the wrong scene being active, simply load the menu scene
        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("Menu2");
            return;
        }

        if (PlayerInstance != null)
        {
            if (Player.LocalPlayerInstance == null)
            {
                Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);

                Vector3 spawnPos = Player1SpawnPoint.position;                

                switch(PhotonNetwork.CurrentRoom.PlayerCount)
                {
                    case 2:
                        spawnPos = Player2SpawnPoint.position;
                        break;
                    case 3:
                        spawnPos = Player3SpawnPoint.position;
                        break;
                    case 4:
                        spawnPos = Player4SpawnPoint.position;
                        break;
                }                

                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                var character = PhotonNetwork.Instantiate(PlayerInstance.name, spawnPos, Quaternion.identity, 0);              
                
                //Kill music for now
                MusicManager.stop(3f);
            }
            else
            {
                Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
            }
        }

        UpdatePlayersList();
    }

    private void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClick_Quit();
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void OnClick_Quit()
    {
        Application.Quit();
    }

    private void StartMusic()
    {
        Music.Instance.PlayMusicTrack(1);
    }

    void LoadArena()
    {
        PhotonNetwork.LoadLevel("Demo_2");
    }

    #region Photon Callbacks

    /// <summary>
    /// Called when a Photon Player got connected. We need to then load a bigger scene.
    /// </summary>
    /// <param name="other">Other.</param>
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player other)
    {
        Debug.Log("OnPlayerEnteredRoom() " + other.NickName); // not seen if you're the player connecting
        Messages.text = $"{other.NickName} entered the game";

        PlayersConnected.text += other.NickName;

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
            //LoadArena();
        }
    }

    /// <summary>
    /// Called when a Photon Player got disconnected. We need to load a smaller scene.
    /// </summary>
    /// <param name="other">Other.</param>
    public override void OnPlayerLeftRoom(Photon.Realtime.Player other)
    {
        Debug.Log("OnPlayerLeftRoom() " + other.NickName); // seen when other disconnects
        Messages.text = $"{other.NickName} left the game";

        UpdatePlayersList();

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
            //LoadArena();
        }
    }

    public void UpdatePlayersList()
    {
        PlayersConnected.text = "";

        foreach (var player in PhotonNetwork.PlayerList)
        {
            PlayersConnected.text += player.NickName+"\n";
        }
    }

    /// <summary>
    /// Called when the local player left the room. We need to load the launcher scene.
    /// </summary>
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Menu2");
    }

    #endregion

    //Hacked up
    private void StartPlayersTest()
    {
        //PUN Networking pieces
        int spawnIndex = 0;
        int playerNum = spawnIndex + 1;        

        var character = Instantiate(PlayerInstance, Player1SpawnPoint.position, Quaternion.identity);
        var player = character.GetComponent<Player>();
        player.PlayerName = "Krunchy";
        player.PlayerNumber = playerNum;                        

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
