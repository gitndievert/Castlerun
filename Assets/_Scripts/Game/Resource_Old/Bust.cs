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

using UnityEngine;

public class Bust : BasePrefab
{
    /// <summary>
    /// Two minute life time!
    /// </summary>
    const float LIFE_TIME = 120f;
       
    public bool GravityPull = false;


    private int _amount;
    private ResourceType _rt;
    private GameObject _bustObj;
    private AudioClip _bustSound;
    private Transform _backplane;
    private Collider _col;
    private bool _locked = false;
    
    protected void Start()
    {        
        _col = GetComponent<Collider>();
        _col.isTrigger = false;
    }
    
    private void FixedUpdate()
    {
        if (GravityPull)
        {
            float overlapRadius = 3f;
            foreach (var col in Physics.OverlapSphere(transform.position, overlapRadius))
            {
                if (col.transform.tag == "Player")
                {
                    float force = -30f;
                    transform.GetComponent<Rigidbody>().AddExplosionForce(force, col.transform.position, 5f, 0, ForceMode.Force);
                }
            }
        }
    }

    public void SetValues(ResourceType type, int amount, AudioClip bustsound)
    {
        _rt = type;
        if (amount < 0) _amount = 0;
        _amount = amount;
        _bustObj = transform.Find(type.ToString()).gameObject;
        _bustSound = bustsound;
        //ResouceText.text = type.ToString();
        //ResouceAmt.text = amount.ToString();
    }
    
    //NATE NOTE
    //Maybe setup and animation corroutine here
    public void Spawn()        
    {
        if(_bustObj != null)
        {
            _bustObj.SetActive(true);            
            Destroy(gameObject, LIFE_TIME);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (_locked) return;
        if (col.transform.tag == "Player")
        {
            PickUp(col.transform.GetComponent<Player>());
        }
        else if (col.gameObject.layer == Global.GROUND_LAYER)
        {
            _col.isTrigger = true;
            transform.GetComponent<Rigidbody>().isKinematic = true;
            _locked = true;
            transform.GetComponent<Bobber>().StartBob(true);
            return;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag != "Player") return;        
        PickUp(col.transform.GetComponent<Player>());        
    }   

    public void PickUp(Player player)
    {
        if (player)
        {
            player.Inventory.Set(_rt, _amount);
            if (!player.Inventory.IsFull(_rt))
            {
                Destroy(gameObject);
                SoundManager.PlaySound(_bustSound);
            }
        }
    }
}

    