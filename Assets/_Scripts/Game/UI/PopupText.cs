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

using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{
    public Animator PopUpTextAnimator;

    public Color DamageColor = Color.red;
    public Color HealColor = Color.green;


    private TextMeshProUGUI _damageText;

    private void Awake()
    {
        _damageText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        var info = PopUpTextAnimator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, info[0].clip.length);        
    }

    public void SetText(string text)
    {        
        _damageText.text = text;
    }

    public void SetCombatText(TextType type, string text, bool critical = false)
    {
        switch(type)
        {
            case TextType.Damage:
                _damageText.color = DamageColor;
                break;
            case TextType.Heal:
                _damageText.color = HealColor;
                break;
        }
        
        if (critical) _damageText.fontSize *= 1.5f;
        SetText(text);
    }

    

}
