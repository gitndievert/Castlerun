using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class MenuManager : MonoBehaviour
{
    public List<GameObject> UIPanels;

    private GameObject _activePanel = null;

    public string Scenename
    {
        get { return SceneManager.GetActiveScene().name; }
    }

    public void OnClick_Main_LoadGame()
    {
        SceneManager.LoadSceneAsync("Demo_1");        
    }


    public void OnClick_Main_Quit()
    {
        Application.Quit();
    }

    public void OnClick_Main_Store()
    {

    }

    public void OnClick_LoadPanelsByName(string panelname)
    {   
        var panel = UIPanels.Where(p => p.name == panelname).First();
        if(panel != null)
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

    private void Awake()
    {
        if(UIPanels.Count > 0)
        {
            foreach(var panel in UIPanels)
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
}
