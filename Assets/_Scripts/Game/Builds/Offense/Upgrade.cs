using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Upgrade : Build
{
    public UpgradeBuildSlots Slots;
    

    public bool Slot1Upgraded = false;
    public bool Slot2Upgraded = false;
    public bool Slot3Upgraded = false;

    private BuildArea _buildArea;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (_buildArea == null)
        {
            _buildArea = GetComponentInChildren<BuildArea>();
        }
    }

    protected override void Update()
    {

    }

    public override void OnMouseDown()
    {
        base.OnMouseDown();
        if (isFinished)
        {
            RefreshUpgradePanel();
        }
    }

    public void RefreshUpgradePanel()
    {
        int i = 0;
        int slotcount = Slots.UpgradeSlots.Length;

        BuildManager.Instance.RefreshSelections();

        foreach (Transform trans in BuildManager.Instance.BuildUI.SelectionsPanel.transform)
        {
            if (i >= slotcount) break;
            var slot = Slots.UpgradeSlots[i];

            var image = trans.GetComponent<Image>();
            image.sprite = slot.Icon;
            //Shift Alpha
            var color = image.color;
            color.a = 0.85f;
            image.color = color;
            
            trans.GetComponent<Button>().onClick.AddListener(() => slot.Upgrade(Player));

            i++;
        }
    }

    public override void UnSelect()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        base.UnSelect();
    }

    public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
