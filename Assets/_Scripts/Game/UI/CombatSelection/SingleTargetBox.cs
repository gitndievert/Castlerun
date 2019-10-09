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

using SBK.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleTargetBox : MonoBehaviour
{
    public TextMeshProUGUI TargetText;
    public Image TargetIcon;
    public ProgressBar HealthBar;
    
    public bool HasSelection { get; private set; }

    public static Color DefaultColor;
    public static Color EnemyColor = Color.red;

    private ISelectable _target;
    private GameObject _targetObj;
    private Image _backgroundImage;

        
    private void Awake()
    {
        //        
        HasSelection = true; //temp for now        
        _backgroundImage = GetComponent<Image>();
        DefaultColor = _backgroundImage.color;
        _backgroundImage.gameObject.SetActive(false);
        HealthBar.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (HasSelection && HealthBar.isActiveAndEnabled)
        {
            int health = _target.GetCurrentHealth();
            if (health <= 0 || _target.IsDying)
            {
                ClearTarget();
            }
            else
            {
                Debug.Log($"Health {health}");
                Debug.Log($"Max Health {_target.GetMaxHealth()}");
                Debug.Log(Mathf.RoundToInt(((float)health / _target.GetMaxHealth()) * 100));
                HealthBar.BarValue = Mathf.RoundToInt(((float)health / _target.GetMaxHealth()) * 100);
            }            
        }
    }

    public void SetTarget(ISelectable target)
    {        
        _target = target;
        _targetObj = target.GameObject;
        if (_targetObj && !target.IsDying)
        {
            Process();
        }        
    }

    public void SetTarget(GameObject target)
    {
        SetTarget(target.GetComponent<ISelectable>());
    }

    public void ClearTarget()
    {
        HasSelection = false;
        _target = null;
        _targetObj = null;
        TargetText.text = string.Empty;
        _backgroundImage.gameObject.SetActive(false);
        HealthBar.gameObject.SetActive(false);
    }

    private void Process()
    {
        TargetText.text = _target.DisplayName;
        TargetIcon.sprite = _target.GetIcon();
        HasSelection = true;       
        
        switch (_targetObj.tag)
        {
            case Global.ARMY_TAG:
            case Global.BUILD_TAG:
                SwapBackgroundColor(DefaultColor);
                break;
            case Global.ENEMY_TAG:
                SwapBackgroundColor(EnemyColor);
                break;
        }
        _backgroundImage.gameObject.SetActive(true);
        HealthBar.gameObject.SetActive(true);
    }

    private void SwapBackgroundColor(Color color)
    {
        _backgroundImage.color = color;
    }

    
}
