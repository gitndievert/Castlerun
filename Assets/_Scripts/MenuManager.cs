using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string Scenename
    {
        get { return SceneManager.GetActiveScene().name; }
    }

    public void OnClick_Main_FindGame()
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

    // Start is called before the first frame update
    void Start()
    {
        //Current Menu Scene
        if (Scenename == "Menu2")
        {
            Music.Instance.PlayMusicTrack(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
