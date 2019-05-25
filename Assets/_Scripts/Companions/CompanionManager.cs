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
    public List<GameObject> CompanionList;

    protected override void PAwake()
    {
       
    }

    protected override void PDestroy()
    {

    }

    public GameObject GetCompanionByType(CompanionType companion)
    {
        foreach(var c in CompanionList)
        {
            var comp = c.GetComponent<Companion>();
            if (comp.CompanionType == companion)
                return c;
        }

        return null;
    }

}
