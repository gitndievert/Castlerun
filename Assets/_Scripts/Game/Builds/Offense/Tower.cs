using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Build
{
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

    public override bool ConfirmPlacement()
    {
        if (!_buildArea.CanBuild) return false;
        gameObject.SetActive(true);
        base.ConfirmPlacement();
        _buildArea.ShowPlane(false);
        CamShake.Instance.Shake(1f, .5f);

        return true;
    }
}
