using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Costs
{
    public Cost[] CostFactors;
}

[Serializable]
public class Cost
{
    public ResourceType Resource;
    public int Amount;
}
