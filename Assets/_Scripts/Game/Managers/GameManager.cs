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
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static GameManager LocalGameManagerInstance;
    
    /// <summary>
    /// Player Prefab
    /// </summary>
    [Tooltip("The prefab to use for representing the player")]
    public GameObject PlayerInstance;            

    [Header("Spawn Points")]
    //Player Spawns
    public Transform Player1SpawnPoint;
    public Transform Player2SpawnPoint;
    public Transform Player3SpawnPoint;
    public Transform Player4SpawnPoint;
    //Castle Spawns
    [Space(5)]
    public Transform Player1CastlePoint;
    public Transform Player2CastlePoint;
    public Transform Player3CastlePoint;
    public Transform Player4CastlePoint;

    [Header("Resource Points")]
    public Transform Player1ResourcePoints;
    public Transform Player2ResourcePoints;
    //public Transform Player3ResourcePoints;
    //public Transform Player4ResourcePoints;
    public TextMeshProUGUI Messages;
    public TextMeshProUGUI PlayersConnected;

    [Header("PUN Network Variables")]
    /// <summary>
    /// Total Players in Room
    /// </summary>
    public static int PlayersInRoom;

    /// <summary>
    /// My Players Number in Room
    /// </summary>
    public static int MyPlayerNumber;

    public float StartingTime;

    #region Sounds
    public AudioClip[] PlayerJoining;


    #endregion


    private void Awake()
    {
        //Flip to Singleton
        if (LocalGameManagerInstance == null)
        {
            LocalGameManagerInstance = this;
        }
        else
        {
            if(LocalGameManagerInstance != this)
            {
                Destroy(LocalGameManagerInstance);
                LocalGameManagerInstance = this;
            }
        }

        DontDestroyOnLoad(gameObject);        
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

                Transform spawnTransform = Player1SpawnPoint;
                Transform castleTransform = Player1CastlePoint;

                switch (PhotonNetwork.CurrentRoom.PlayerCount)
                {
                    case 2:
                        spawnTransform = Player2SpawnPoint;
                        castleTransform = Player2CastlePoint;
                        break;
                    case 3:
                        spawnTransform = Player3SpawnPoint;
                        castleTransform = Player3CastlePoint;
                        break;
                    case 4:
                        spawnTransform = Player4SpawnPoint;
                        castleTransform = Player4CastlePoint;
                        break;
                }                

                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                var character = PhotonNetwork.Instantiate(PlayerInstance.name, spawnTransform.localPosition, spawnTransform.localRotation, 0);
                character.layer = Global.IGNORE_LAYER;

                MyPlayerNumber = PhotonNetwork.LocalPlayer.ActorNumber;              
                
                var player = character.GetComponent<Player>();
                player.ActorNumber = MyPlayerNumber;                

                Debug.Log($"Current actor number: {MyPlayerNumber}");
                //Kill music for now
                MusicManager.stop(3f);

                //Castle test data                
                var custom = PhotonNetwork.LocalPlayer.CustomProperties;
                foreach(var c in custom)
                {                    
                    if((string)c.Key == "castle")
                    {
                        var castleObj = CastleManager.Instance.GetCastle((string)c.Value);                        
                        var castleSpawn = PhotonNetwork.Instantiate(castleObj.name, castleTransform.localPosition, castleTransform.localRotation, 0);                        
                    }
                }

                SoundManager.PlaySound(PlayerJoining);
            }
            else
            {
                Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
            }
        }

        UpdatePlayersList();
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
        other.CustomProperties.TryGetValue("castle", out object castle);

        if (castle != null)
        {
            Debug.Log($"Player {other.NickName} has castle {(string)castle}");
        }

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
            //LoadArena();            
        }

        SoundManager.PlaySound(PlayerJoining);
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

        var character = Instantiate(PlayerInstance, Player1SpawnPoint.localPosition, Player1SpawnPoint.localRotation);
        var player = character.GetComponent<Player>();
        player.PlayerName = "Krunchy";
        
        var castleSpawn = Instantiate(CastleManager.Instance.GetCastle("classic"), Player1CastlePoint.localPosition, Player1CastlePoint.localRotation);        

        var castleSpawnTwo = Instantiate(CastleManager.Instance.GetCastle("fod"), Player2CastlePoint.localPosition, Player2CastlePoint.localRotation);
        var charactertwo = Instantiate(PlayerInstance, Player2SpawnPoint.localPosition, Player2SpawnPoint.localRotation);
        var playertwo = charactertwo.GetComponent<Player>();
        playertwo.PlayerName = "MrTest";

        castleSpawnTwo.tag = Global.ENEMY_TAG;
        castleSpawnTwo.layer = Global.ENEMY_LAYER;
        
    }    
}
