using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Troop : BasePrefab, ICharacter
{
    [Header("All the points that this Troop will Follow")]
    public Transform[] points;

}
