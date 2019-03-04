using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INotifications
{
    //void ScheduleNotification(string title, string notification, int seconds);
    //void ScheduleRepeatingNotification(string title, string notification, int seconds, float repeatDays);
    void InGameNotification(InGameNotificationTypes panel);
}
