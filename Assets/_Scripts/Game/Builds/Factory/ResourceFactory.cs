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

public class ResourceFactory : TroopFactory
{
    private int _goldIncrementAmount = 500;
    private float _goldIncrementSec = 90f;

    protected override void Start()
    {
        base.Start();
        InvokeRepeating("AccumulateGold", 30f, _goldIncrementSec);
    }

    protected void OnDestroy()
    {
        CancelInvoke("AccumulateGold");
    }

    private void AccumulateGold()
    {
        Player.Inventory.Set(ResourceType.Gold, _goldIncrementAmount);
    }
}
