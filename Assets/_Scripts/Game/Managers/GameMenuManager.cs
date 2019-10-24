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
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviourPunCallbacks
{
    public static bool IsMenuUp = false;

    public GameObject BackPanel;
    public List<GameObject> UIPanels;
            
    private GameObject _activePanel = null;    
        

    public string Scenename
    {
        get { return SceneManager.GetActiveScene().name; }
    }

    private void Awake()
    {
        OnClick_UnloadAllPanels();        
    }

    // Start is called before the first frame update
    private void Start()
    {        
        //This activates panels
        //Music.Instance.PlayMusicTrack(0);
        //_activePanel = UIPanels.Where(p => p.name == "MainMenuPanel").First();
        //_activePanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        IsMenuUp = BackPanel.activeSelf;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsMenuUp)
            {
                OnClick_UnloadAllPanels();
            }
            else
            {
                if(Selection.Instance.SingleTargetSelected != null)
                {
                    Selection.Instance.PreMenuClear();
                }
                else
                {
                    OnClick_LoadPanelsByName("GameMainMenuPanel");
                }                
            }
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
        BackPanel.SetActive(true);
        var panel = UIPanels.Where(p => p.name == panelname).First();
        if (panel != null)
        {            
            if (_activePanel != null)            
                _activePanel.SetActive(false);                         
            panel.SetActive(true);
            _activePanel = panel;
        }
    }

    public void OnClick_LoadPanelsByObject(GameObject panel)
    {
        BackPanel.SetActive(true);        
        _activePanel.SetActive(false);
        panel.SetActive(true);
        _activePanel = panel;
    }

    public void OnClick_UnloadAllPanels()
    {
        BackPanel.SetActive(false);        
        if (UIPanels.Count > 0)
        {
            foreach (var panel in UIPanels)
            {
                panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
                panel.SetActive(false);
            }
        }
    }
    
}
