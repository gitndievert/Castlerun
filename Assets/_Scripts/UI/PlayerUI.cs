using System;
using UnityEngine;
using TMPro;

[Serializable]
public class PlayerUI
{
    #region Main Player Stats
    public TextMeshProUGUI PlayerName;
    public TextMeshProUGUI HealthText;
    //public SimpleHealthBar HealthBar;
    //public SimpleHealthBar StaminaBar;
    public GameObject HealthBar;
    public GameObject StaminaBar;

    public TextMeshProUGUI CastleLevel;
    #endregion

    public ProgressBar HealthBarInstance
    {
        get { return HealthBar.GetComponent<ProgressBar>(); }
    }

    public ProgressBar StaminaBarInstance
    {
        get { return StaminaBar.GetComponent<ProgressBar>(); }
    }
}
