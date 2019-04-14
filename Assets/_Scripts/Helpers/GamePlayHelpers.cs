using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamePlayHelpers
{
    public static int GetChance(this int amount, int min, int max)
    {
        if (amount > max) return max;
        int pick = Random.Range(min, max);
        return pick < amount ? pick : amount;
    }
}
