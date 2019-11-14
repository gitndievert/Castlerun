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
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static GameManager Instance;
    public static Player MyLocalPlayer;
    
    /// <summary>
    /// Player Prefab
    /// </summary>
    [Tooltip("The prefab to use for representing the player")]
    public GameObject PlayerInstance;
    public GameObject TestPlayerInstance;

    #region SpawnPoints
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

    #endregion
    [Space(10)]
    public TextMeshProUGUI Messages;
    public TextMeshProUGUI PlayersConnected;

    public GameObject GameFlag;
    
    #region Sounds
    public AudioClip[] PlayerJoining;
    #endregion


    private void Awake()
    {
        //Flip to Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if(Instance != this)
            {
                Destroy(Instance);
                Instance = this;
            }
        }

        DontDestroyOnLoad(gameObject);        
    }

    private void Start()
    {           
        if(Global.DeveloperMode)
        {
            StartPlayersTest();
            //Music.Instance.PlayMusicTrack(1);
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

                int playernum = PhotonNetwork.LocalPlayer.ActorNumber;              
                
                var player = character.GetComponent<Player>();
                player.ActorNumber = playernum;
                MyLocalPlayer = player;

                Debug.Log($"Current actor number: {playernum}");
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
                        player.PlayerCastle = castleSpawn.GetComponent<Castle>();
                        player.PlayerCastle.PlayerNumber = playernum;
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

    #region Photon Callbacks

    /// <summary>
    /// Called when a Photon Player got connected. We need to then load a bigger scene.
    /// </summary>
    /// <param name="other">Other.</param>
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player other)
    {
        Debug.Log("OnPlayerEnteredRoom() " + other.NickName); // not seen if you're the player connecting
        Messages.text = $"{other.NickName} entered the game";

        UpdatePlayersList();

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom         
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

    #region Developer/Test Calls
    //Hacked up
    private void StartPlayersTest()
    {
        var character = Instantiate(PlayerInstance, Player1SpawnPoint.localPosition, Player1SpawnPoint.localRotation);
        var player = character.GetComponent<Player>();        
        var castleSpawn = Instantiate(CastleManager.Instance.GetCastle("classic"), Player1CastlePoint.localPosition, Player1CastlePoint.localRotation);
        player.PlayerName = "Krunchy";
        player.PlayerCastle = castleSpawn.GetComponent<Castle>();
        player.PlayerCastle.PlayerNumber = 1;


        var charactertwo = Instantiate(TestPlayerInstance, Player2SpawnPoint.localPosition, Player2SpawnPoint.localRotation);
        var playertwo = charactertwo.GetComponent<Player>();
        var castleSpawnTwo = Instantiate(CastleManager.Instance.GetCastle("fod"), Player2CastlePoint.localPosition, Player2CastlePoint.localRotation);
        playertwo.PlayerName = "MrTest";
        playertwo.PlayerCastle = castleSpawnTwo.GetComponent<Castle>();
        player.PlayerCastle.PlayerNumber = 2;
        charactertwo.tag = Global.ENEMY_TAG;
        charactertwo.layer = Global.ENEMY_LAYER;

        castleSpawnTwo.tag = Global.ENEMY_TAG;
        castleSpawnTwo.layer = Global.ENEMY_LAYER;        
    }

    #endregion
   
}
