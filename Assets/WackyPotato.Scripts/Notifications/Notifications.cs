﻿// ********************************************************************
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

using System;
using System.Collections.Generic;
using UnityEngine;
using SBK.Unity;
//using SA.Common.Util;
using System.Linq;

public class Notifications : PSingle<Notifications>, INotifications
{
    /*public AndroidNotificationManager AndroidNotificationMgr
    {
        get { return AndroidNotificationManager.Instance; }
    }

    #if UNITY_ANDROID || UNITY_EDITOR
    public List<LocalNotificationTemplate> PendingAndroidNotifications
    {
        get { return AndroidNotificationMgr.LoadPendingNotifications(true); }
    }
    #endif*/

    public GameObject InGameUIPanel;
    public GameObject InGameNotificationPanelRating;
    public GameObject InGameNotificationPanelFreeToPlay;    
    public GameObject[] InGameNotificationPanelsApps;

    protected override void PAwake()
    {
            
    }

    protected override void PDestroy()
    {        
    }
        
    /*public void ScheduleNotification(string title, string notification, int seconds)
    {
#if UNITY_ANDROID
        var builder = new AndroidNotificationBuilder(IdFactory.NextId, title, notification, seconds);
        AndroidNotificationMgr.ScheduleLocalNotification(builder);        
#endif
    }

    public void ScheduleRepeatingNotification(string title, string notification, int seconds, float repeatDays)
    {
#if UNITY_ANDROID
        var builder = new AndroidNotificationBuilder(IdFactory.NextId, title, notification, seconds);
        builder.SetRepeating(true);
        var currentDt = DateTime.Now;
        TimeSpan delay = currentDt.AddDays(repeatDays) - currentDt;
        builder.SetRepeatDelay(Convert.ToInt32(delay.TotalSeconds));
        AndroidNotificationMgr.ScheduleLocalNotification(builder);
#endif
    }

    public void PopToastNotification(string notification)
    {
#if UNITY_ANDROID
        AndroidToast.ShowToastNotification(notification, AndroidToast.LENGTH_LONG);
#endif
    }*/

    public void InGameNotification(string title, string notification)
    {
        //Generic Panel
    }

    public void InGameNotification(InGameNotificationTypes panel)
    {        
        switch (panel)
        {
            case InGameNotificationTypes.FreetoPay:                
                InGameNotificationPanelFreeToPlay.SetActive(true);
                break;
            case InGameNotificationTypes.RateOurApp:
                InGameNotificationPanelRating.SetActive(true);
                break;
            case InGameNotificationTypes.TryOtherApp:
                var r = new System.Random();
                var randPanel = InGameNotificationPanelsApps.OrderBy(x => r.Next()).FirstOrDefault();
                if(randPanel != null)
                    randPanel.SetActive(true);
                break;
        }
    }



}
