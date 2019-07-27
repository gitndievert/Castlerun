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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopFactory : Build, ISelectable
{
    #region Resource Cost
    public const int WOOD_COST = 10;
    #endregion

    protected override float BuildTime => ConstructionTime;

    protected override ResourceType ResourceType => ResourceType.Wood;

    /// <summary>
    /// The time it takes to build the structure
    /// </summary>
    public float ConstructionTime;

    public float PlacementDistance = 2f;

    public Troop[] Troops;
    public BuildArea BuildArea;

    /// <summary>
    /// This is the start timer for the initial Troops. Hi Jessia
    /// </summary>
    public float StartTime;

    /// <summary>
    /// Time to train each troop
    /// </summary>
    public float TrainingTime;

    /// <summary>
    /// The number of troops trained on each training pass
    /// </summary>
    public int NumberToTrain = 1;

    private int _trainedCounter = 0;


    /// <summary>
    /// The number of troops trained on each training pass
    /// </summary>
    public int MaxTrained = 5;    

    private ResourceType _pickType;
    private bool _IsBuilding = false;        

    // Start is called before the first frame update
    protected override void Start()
    {
        if (Health == 0) Health = 300;
        MaxHealth = Health;        
        if(BuildArea == null)
        {
            BuildArea = GetComponentInChildren<BuildArea>();
        }
    }

    public override bool ConfirmPlacement()
    {
        if (!BuildArea.CanBuild) return false;
        base.ConfirmPlacement();
        BuildArea.ShowPlane(false);
        if (BuildTime > 0)
        {
            StartCoroutine(RunBuild());            
        }

        return true;
    }

    protected IEnumerator RunBuild()
    {
        Debug.Log("Start Build");
        Global.BuildMode = false;
        yield return new WaitForSeconds(BuildTime);
        SoundManager.PlaySound(SoundList.Instance.BuildSound);
        Debug.Log("Finish Build");
        Global.BuildMode = true;
        EnableTroopBuilder = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnableTroopBuilder && !_IsBuilding)
        {
            BuildTroops();
            _IsBuilding = true;
            Debug.Log("Building Turned on for Harvesting Peoples");
        }

        if ((!EnableTroopBuilder && _IsBuilding) || _trainedCounter == MaxTrained)
        {
            StopBuild();
            Debug.Log("Max Number of Harvesters Made");
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked on the target");
    }


    public override bool SetResourceType(ResourceType type)
    {
        _pickType = type;
        switch (type)
        {
            case ResourceType.Wood:
                PlacementCost = WOOD_COST;
                break;
            default:
                return false;
        }

        return true;
    }        

    public void BuildTroops()
    {
        if (Troops == null) return;
        InvokeRepeating("Build", StartTime, TrainingTime);
    }

    public void StopBuild()
    {
        CancelInvoke();
        _IsBuilding = false;
    }


    private void Build()
    {
        for (int i = 0; i < NumberToTrain; i++)
        {
            var randTroop = Troops[Random.Range(0, Troops.Length - 1)];            
            var makeTroop = Instantiate(randTroop.gameObject, transform.position + (Vector3.forward * 2 * PlacementDistance), Quaternion.identity);            
            //Come back
            if (Player != null)
                makeTroop.GetComponent<Troop>().points = Player.PlayerPad.ResourcePoints; //Might change all this later

            _trainedCounter++;
        }
    }
}
