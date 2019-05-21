using SBK.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionManager : PSingle<CompanionManager>
{
    /// <summary>
    /// All Castles in Game
    /// </summary>
    [Tooltip("All the companion prefabs in the game")]
    public List<Companion> CompanionList;

    protected override void PAwake()
    {
        if (CompanionList != null && CompanionList.Count == 0)
            CompanionList.AddRange(transform.GetComponentsInChildren<Companion>());
    }

    protected override void PDestroy()
    {

    }

    public Companion GetCompanionByType(CompanionType companion)
    {
        foreach(var c in CompanionList)
        {
            if (c.CompanionType == companion)
                return c;
        }

        return null;
    }

}
