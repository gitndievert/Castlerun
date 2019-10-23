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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/**
 * For future additions for general extensions
 */
public static partial class Extensions
{
    public static string SetupForClass(this string setup, string another)
    {
        return another;
    }

    /// <summary>
    /// Returns the layer for the hit transform
    /// </summary>
    /// <param name="hit"></param>
    /// <returns></returns>
    public static int GetLayer(this RaycastHit hit)
    {
        return hit.transform.gameObject.layer;
    }

    /// <summary>
    /// Returns the tag from the hit transform
    /// </summary>
    /// <param name="hit"></param>
    /// <returns></returns>
    public static string GetTag(this RaycastHit hit)
    {
        return hit.transform.tag;
    }

    public static string TypeToString(this Enum types)
    {
        return Regex.Replace(types.ToString(), "(\\B[A-Z])", " $1");
    }

}

