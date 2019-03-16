using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SBK.Unity;

public partial class UIManager : PSingle<UIManager>
{
    /// <summary>
    /// Inventory Panel
    /// </summary>
    public InventoryUI InventoryUIPanel;

    /// <summary>
    /// Player Panel
    /// </summary>
    public PlayerUI PlayerUIPanel;

    protected override void PAwake()
    {
        
    }

    protected override void PDestroy()
    {
        
    }
}
