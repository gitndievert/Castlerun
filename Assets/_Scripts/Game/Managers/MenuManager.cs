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

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using ExitGames.Client.Photon;
using UnityEngine.UI;

public class MenuManager : MonoBehaviourPunCallbacks
{
    const byte MAX_PLAYER_PER_ROOM = 4;

    public TMP_InputField PlayerNameInput;
    public TMP_InputField RoomNameInputField;
    public TMP_InputField MaxPlayersInputField;

    public Button StartGameButton;
    public List<GameObject> UIPanels;

    private Dictionary<string, RoomInfo> cachedRoomList;
    private Dictionary<string, GameObject> roomListEntries;
    private Dictionary<int, GameObject> playerListEntries;    
    private GameObject _activePanel = null;

    /// <summary>
    /// Keep track of the current process. Since connection is asynchronous and is based on several callbacks from Photon, 
    /// we need to keep track of this to properly adjust the behavior when we receive call back by Photon.
    /// Typically this is used for the OnConnectedToMaster() callback.
    /// </summary>
    private bool isConnecting;

    /// <summary>
    /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
    /// </summary>
    private string gameVersion = "1";


    public string Scenename
    {
        get { return SceneManager.GetActiveScene().name; }
    }

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        cachedRoomList = new Dictionary<string, RoomInfo>();
        roomListEntries = new Dictionary<string, GameObject>();

        PlayerNameInput.text = "Player " + Random.Range(1000, 10000);

        if (UIPanels.Count > 0)
        {
            foreach (var panel in UIPanels)
            {
                panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
                panel.SetActive(false);
            }
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        //Current Menu Scene
        if (Scenename == "Menu2")
        {
            Music.Instance.PlayMusicTrack(0);
        }

        _activePanel = UIPanels.Where(p => p.name == "MainMenuPanel").First();
        _activePanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Start the connection process. 
    /// - If already connected, we attempt joining a random room
    /// - if not yet connected, Connect this application instance to Photon Cloud Network
    /// </summary>
    public void Connect()
    {
        // keep track of the will to join a room, because when we come back from the game we will get a callback that we are connected, so we need to know what to do then
        isConnecting = true;

        // hide the Play button for visual consistency
        //controlPanel.SetActive(false);       

        string playerName = PlayerNameInput.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;            
        }
        else
        {
            Debug.LogError("Player Name is invalid.");
        }

        // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
        if (PhotonNetwork.IsConnected)
        {            
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {            
            // #Critical, we must first and foremost connect to Photon Online Server.
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
       

    public void OnClick_Main_LoadGame(string scenename)
    {
        SceneManager.LoadSceneAsync(scenename);
    }

    public void OnClick_PUN_Login()
    {
        string playerName = PlayerNameInput.text;
        
        if(!string.IsNullOrEmpty(playerName))
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;            
            PhotonNetwork.ConnectUsingSettings();            
        }
        else
        {
            Debug.LogError("Player Name is invalid.");
        }
    }

    public void OnClick_Main_Quit()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LeaveRoom();
        }
        Application.Quit();
    }

    public void OnClick_Back()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }

        OnClick_LoadPanelsByName("MainMenuPanel");
    }

    public void OnClick_LeaveGameButton()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void OnClick_LoadPanelsByName(string panelname)
    {
        var panel = UIPanels.Where(p => p.name == panelname).First();
        if (panel != null)
        {
            _activePanel.SetActive(false);
            panel.SetActive(true);
            _activePanel = panel;
        }
    }

    public void OnClick_LoadPanelsByObject(GameObject panel)
    {
        _activePanel.SetActive(false);
        panel.SetActive(true);
        _activePanel = panel;
    }

    #region PUN CallBacks

    /// <summary>
    /// Called after the connection to the master is established and authenticated
    /// </summary>
    public override void OnConnectedToMaster()
    {
        // we don't want to do anything if we are not attempting to join a room. 
        // this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
        // we don't want to do anything.
        if (isConnecting)
        {            
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room.\n Calling: PhotonNetwork.JoinRandomRoom(); Operation will fail if no room found");

            // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public void CreateRoom()
    {        
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MAX_PLAYER_PER_ROOM });
    }

    /// <summary>
    /// Called when a JoinRandom() call failed. The parameter provides ErrorCode and message.
    /// </summary>
    /// <remarks>
    /// Most likely all rooms are full or no rooms are available. <br/>
    /// </remarks>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {        
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        CreateRoom();
    }

    /*public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CreateRoom();
    }*/


    /// <summary>
    /// Called after disconnecting from the Photon server.
    /// </summary>
    public override void OnDisconnected(DisconnectCause cause)
    {        
        Debug.LogError("PUN Basics Tutorial/Launcher:Disconnected");
        isConnecting = false;
        OnClick_LoadPanelsByName("MainMenuPanel");
    }

    /// <summary>
    /// Called when entering a room (by creating or joining it). Called on all clients (including the Master Client).
    /// </summary>
    /// <remarks>
    /// This method is commonly used to instantiate player characters.
    /// If a match has to be started "actively", you can call an [PunRPC](@ref PhotonView.RPC) triggered by a user's button-press or a timer.
    ///
    /// When this is called, you can usually already access the existing players in the room via PhotonNetwork.PlayerList.
    /// Also, all custom properties should be already available as Room.customProperties. Check Room..PlayerCount to find out if
    /// enough players are in the room to start playing.
    /// </remarks>
    public override void OnJoinedRoom()
    {     
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.\nFrom here on, your game would be running.");

        MusicManager.stop();
        // #Critical: We only load if we are the first player, else we rely on  PhotonNetwork.AutomaticallySyncScene to sync our instance scene.
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            // #Critical
            // Load the Room Level.             
            PhotonNetwork.LoadLevel("Demo_2");
        }
    }

    #endregion   
}
