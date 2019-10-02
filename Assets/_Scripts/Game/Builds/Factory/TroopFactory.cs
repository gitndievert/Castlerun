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

using Photon.Pun;
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
public class TroopFactory : Build, IPunObservable
{    
    public float PlacementDistance = 2f;

    public Troop[] Troops;    

    /// <summary>
    /// Time to train each troop
    /// </summary>
    public float TrainingTime;

    /// <summary>
    /// The number of troops trained on each training pass
    /// </summary>
    public int NumberToTrain = 1;

    [SerializeField]
    private int _trainedCounter = 0;

    private bool _isQueued;    
    private BuildArea _buildArea;
    private Queue<Troop> _troopQueue = new Queue<Troop>();

    /// <summary>
    /// The number of troops trained on each training pass
    /// </summary>
    public int MaxTrained = 5;

    [Header("Spawn and Waypoint Positions")]
    public Transform SpawnPointPosition;
    public Transform WayPointPosition;  


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
        CamShake.Instance.Shake(1f, .5f);
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
            if (_trainedCounter <= MaxTrained)
            {
                foreach (var troop in _troopQueue)
                {
                    StartCoroutine(QueueTroop(troop));
                    Player.Inventory.Set(troop.Costs);                    
                }
            }

            _troopQueue.Dequeue();
            _isQueued = false;
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

    public void UnListTroop()
    {
        if(_trainedCounter > 1)
            _trainedCounter--;
    }
    
    public void Train(Troop selectedTroop)
    {
        if(_trainedCounter >= MaxTrained)
        {
            Global.Message("Cannot Train anymore troops");
            return;
        }
        
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
            _trainedCounter++;
            _isQueued = true;
            _troopQueue.Enqueue(selectedTroop);            
        }
        else
        {
            Global.Message("Not enough resources to make troop");
        }

        RefreshTroopPanel();
    }   

    private IEnumerator QueueTroop(Troop selectedTroop)
    {        
        yield return new WaitForSeconds(TrainingTime);

        GameObject makeTroop = null;

        if (Global.DeveloperMode)
        {
            makeTroop = Instantiate(selectedTroop.gameObject, SpawnPointPosition.position, Quaternion.identity);
        }
        else
        {
            makeTroop = PhotonNetwork.Instantiate(selectedTroop.gameObject.name, SpawnPointPosition.position, Quaternion.identity);
        }

        if (Player != null)
        {
            //Parent to Player Builds
            makeTroop.transform.parent = Player.PlayerWorldItems.transform;

            //Access Troop Component
            var troop = makeTroop.GetComponent<Troop>();
            //Add Player to Troop
            troop.SetPlayer(Player);
            troop.SetFactory(this);

            //Move up the troop (GOING TO TABLE FOR NOW)
            WayPointPosition = SpawnPointPosition; //temp stuff
            troop.Move(WayPointPosition.position);

            //Play Spawning Sound FX
            if (troop.FreshTroop != null /*&& i == NumberToTrain - 1*/)
            {
                SoundManager.PlaySound(troop.FreshTroop);
            }

            //If Troop is a Gatherer
            Gatherer gatherer = makeTroop.GetComponent<Gatherer>();            
            if (gatherer != null)
            {                                                     
                int i = 0;
                foreach(Transform points in GameManager.Player1ResourcePoints)
                {
                    gatherer.points.Add(i, points);
                    i++;
                }
            }            
        }        

        yield return null;
    }

    public override void UnSelect()
    {
        if(EventSystem.current.IsPointerOverGameObject()) return;
        base.UnSelect();
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data

            //stream.SendNext(this.IsFiring);
            //stream.SendNext(this.Health);
        }
        else
        {
            // Network player, receive data

            //this.IsFiring = (bool)stream.ReceiveNext();
            //this.Health = (float)stream.ReceiveNext();
        }
    }
}
