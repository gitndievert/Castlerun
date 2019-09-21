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
using UnityEngine;
using UnityEngine.UI;

/*
 * !! Note About This!!!
 * 
 * Troop Factories are a generic term for any building that can produce
 * troops, either Army or Gathering, etc. 
 * 
 */

public class TroopFactory : Build
{    
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
        base.Start();
        if (BuildArea == null)
        {
            BuildArea = GetComponentInChildren<BuildArea>();
        }        
    }    

    public override bool ConfirmPlacement()
    {
        if (!BuildArea.CanBuild) return false;
        base.ConfirmPlacement();
        BuildArea.ShowPlane(false);        
        /*if (BuildTime > 0)
        {
            StartCoroutine(RunBuild());            
        }*/        

        return true;
    }
       
    // Update is called once per frame
    protected override void Update()
    {
        if (!_IsBuilding && _trainedCounter != MaxTrained)
        {            
            _IsBuilding = true;            
        }
        else if (_IsBuilding && _trainedCounter == MaxTrained)
        {
            StopTraining();
            Debug.Log("Max Number of Harvesters Made");
        }        
    }

    public override void OnMouseDown()
    {
        base.OnMouseDown();
        if (isFinished)
        {
            RefreshTroopPanel();
            BuildManager.Instance.ShowTroopPanel();
        }
    }
    
    public void RefreshTroopPanel()
    {
        int i = 0;
        int troopcount = Troops.Length;

        foreach (Transform trans in BuildManager.Instance.BuildUI.TroopsPanel.transform)
        {
            if (i >= troopcount) break;
            var troop = Troops[i];

            var image = trans.GetComponent<Image>();
            image.sprite = troop.GetIcon();
            trans.GetComponent<Button>().onClick.AddListener(() => Train(troop));           

            i++;
        }
    }
    
    public void StopTraining()
    {
        CancelInvoke();
        _IsBuilding = false;
    }
    
    public void Train(Troop selectedTroop)
    {
        if (_trainedCounter == MaxTrained)
        {
            //Cannot train anymore troops message
            return;
        }
        StartCoroutine(QueueTroop(selectedTroop));
    }   

    private IEnumerator QueueTroop(Troop selectedTroop)
    {
        yield return new WaitForSeconds(TrainingTime);

        var makeTroop = Instantiate(selectedTroop.gameObject, transform.position + (Vector3.forward * 2 * PlacementDistance), Quaternion.identity);        

        if (Player != null)
        {
            //Parent to Player Builds
            makeTroop.transform.parent = Player.PlayerWorldItems.transform;

            //Access Troop Component
            var troop = makeTroop.GetComponent<Troop>();
            //Add Player to Troop
            troop.TroopPlayer = Player;
            //Move up the troop
            troop.Move(makeTroop.transform.position + (Vector3.forward * 3 * PlacementDistance));

            //Play Spawning Sound FX
            if (troop.FreshTroop != null)
            {
                SoundManager.PlaySound(troop.FreshTroop);
            }

            Gatherer gatherer = makeTroop.GetComponent<Gatherer>();
            //If Troop is a Gatherer
            if (gatherer != null)
            {
                gatherer.SetFactory(this);
                gatherer.HarvestingSelection = ResourceType.Wood;
                for (int p = 0; p < Player.PlayerPad.ResourcePoints.Length; p++)
                {
                    gatherer.points.Add(p, Player.PlayerPad.ResourcePoints[p]);
                }
            }
        }

        _trainedCounter++;

        yield return null;
    }
}
