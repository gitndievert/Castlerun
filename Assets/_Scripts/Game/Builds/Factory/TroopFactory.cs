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
using UnityEngine.EventSystems;
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
    private bool _isQueued;    
    private BuildArea _buildArea;
    private Queue<Troop> _troopQueue = new Queue<Troop>();

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
        if (_buildArea == null)
        {
            _buildArea = GetComponentInChildren<BuildArea>();
        }        
    }    

    public override bool ConfirmPlacement()
    {
        if (!_buildArea.CanBuild) return false;
        base.ConfirmPlacement();
        _buildArea.ShowPlane(false);        
        /*if (BuildTime > 0)
        {
            StartCoroutine(RunBuild());            
        }*/        

        return true;
    }
       
    // Update is called once per frame
    protected override void Update()
    {
        if (_troopQueue.Count > 0 && _isQueued)
        {
            foreach(var troop in _troopQueue)
            {
                StartCoroutine(QueueTroop(troop));
            }

            _troopQueue.Dequeue();
            _isQueued = false;
        }

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
        }
    }
    
    public void RefreshTroopPanel()
    {
        int i = 0;
        int troopcount = Troops.Length;

        BuildManager.Instance.RefreshSelections();

        foreach (Transform trans in BuildManager.Instance.BuildUI.SelectionsPanel.transform)
        {
            if (i >= troopcount) break;
            var troop = Troops[i];

            var image = trans.GetComponent<Image>();
            image.sprite = troop.GetIcon();
            //Shift Alpha
            var color = image.color;
            color.a = 0.85f;
            image.color = color;

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
        //Keep Factory selected
        //TODO: Here
        //BUGGY SHIT CODE NEED TO FIX
        IsSelected = true;
        SelectionUI.UpdateSingleTarget(this);
        //END SHIT CODE

        bool metCosts = false;
        var costs = selectedTroop.GetCosts();
        foreach (var cost in costs.CostFactors)
        {
            int invcount = Player.Inventory.GetCount(cost.Resource);
            metCosts = invcount > 0 && (invcount - cost.Amount >= 0);
        }

        if (metCosts)
        {
            if (_trainedCounter == MaxTrained)
            {
                //Cannot train anymore troops message                
                return;
            }

            Player.Inventory.Set(costs);
            //_trainedCounter++;
            _isQueued = true;
            _troopQueue.Enqueue(selectedTroop);            
        }

        RefreshTroopPanel();
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
            troop.SetPlayer(Player);
            //Move up the troop
            troop.Move(makeTroop.transform.position + (Vector3.forward * 3 * PlacementDistance));

            //Play Spawning Sound FX
            if (troop.FreshTroop != null /*&& i == NumberToTrain - 1*/)
            {
                SoundManager.PlaySound(troop.FreshTroop);
            }

            Gatherer gatherer = makeTroop.GetComponent<Gatherer>();
            //If Troop is a Gatherer
            if (gatherer != null)
            {
                gatherer.SetFactory(this);
                gatherer.HarvestingSelection = ResourceType.Wood;
                //TODO: Come Back for Pathing
                //Using one spawnpoint for now                
                int i = 0;
                foreach(Transform points in GameManager.Player1ResourcePoints)
                {
                    gatherer.points.Add(i, points);
                    i++;
                }
            }

            //_trainedCounter--;
        }        

        yield return null;
    }

    public override void UnSelect()
    {
        if(EventSystem.current.IsPointerOverGameObject()) return;
        base.UnSelect();
    }
}
