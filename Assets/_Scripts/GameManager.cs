﻿using SBK.Unity;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
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
            //Need to figure out spawn positions (with PUN)
            int spawnIndex = PhotonNetwork.IsMasterClient ? 0 : 1;
            var character = PhotonNetwork.Instantiate(PlayerInstance.name, PlayerPads[spawnIndex].PlayerSpawnPosition, Quaternion.identity);

            var player = character.GetComponent<Player>();
            player.PlayerNumber = spawnIndex + 1; //total hack for player number right now

            //if (photonView.IsMine)
            //{
            //Transform pt = character.transform;
            CameraRig.GetComponent<CameraRotate>().target = character.transform;
            //}
        }
        else
        {
            Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
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

    #region Photon Callbacks


    /// <summary>
    /// Called when the local player left the room. We need to load the launcher scene.
    /// </summary>
    public override void OnLeftRoom()
    {
        //For now, just load the first scene index
        SceneManager.LoadScene(0);
    }
       

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


            LoadArena();
        }
    }


    public override void OnPlayerLeftRoom(Photon.Realtime.Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects
               
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
            LoadArena();
        }
    }



    #endregion


    #region Public Methods


    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }


    #endregion

    private void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
    }



}
