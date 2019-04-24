using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SBK.Unity;
using TMPro;

public partial class UIManager : PSingle<UIManager>
{
    public ProgressBar HealthBar;
    public ProgressBar StaminaBar;

    /// <summary>
    /// Inventory Panel
    /// </summary>
    public InventoryUI InventoryUIPanel;

    /// <summary>
    /// Player Panel
    /// </summary>
    public PlayerUI PlayerUIPanel;

    public TextMeshProUGUI Messages;

    protected override void PAwake()
    {
        
    }

    protected override void PDestroy()
    {
        
    }
}
