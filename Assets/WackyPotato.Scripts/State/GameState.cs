using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;


public static class GameState
{    
    /*public static bool Victory;                        
    public static Settings SettingsData = new Settings();
    public static string SavePath = Application.persistentDataPath + "SettingsInfo.dat";
    public static bool DragEnabled = true;

    public static void SaveData()
    {
        var bf = new BinaryFormatter();
        using (var file = File.Open(SavePath, FileMode.OpenOrCreate))
        {
            Settings settings = null;
            if (!SettingsData.Written)
            {
                settings = new Settings
                {
                    Written = true,
                    Reviewed = false,
                    WinCount = 0,
                    MusicVolume = 1f,
                    SoundVolume = 1f
                };
                SettingsData = settings;
            }
            else
            {
                settings = SettingsData;
            }
            bf.Serialize(file, settings);
            file.Close();
        }
    }

    public static void LoadData()
    {
        if(File.Exists(SavePath))
        {
            var bf = new BinaryFormatter();
            using (var file = File.Open(SavePath, FileMode.Open))
            {
                SettingsData = (Settings)bf.Deserialize(file);
                PushSettings();
                file.Close();
            }
        }

    }

    private static void PushSettings()
    {            
        Music.Instance.Volume(SettingsData.MusicVolume);
        SoundManager.Instance.Volume(SettingsData.SoundVolume);
        if(SettingsData.WinCount == 10)
        {
            //WILL NEED TO SETUP WITH OTHER GAMES
            //Notifications.Instance.InGameNotification(InGameNotificationTypes.TryOtherApp);
                
            //Will just increment for now. Wins doesn't matter its more for tracking popups
            SettingsData.WinCount++; 
        }
    }*/


}

