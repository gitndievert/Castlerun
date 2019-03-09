using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bust : BasePrefab
{    
    private int _amount;
    private ResourceType _rt;
    private GameObject _bustObj;
    private AudioClip _bustSound;

    protected override void Start()
    {
        base.Start();
    }

    public void SetValues(ResourceType type, int amount, AudioClip bustsound)
    {
        _rt = type;
        if (amount < 0) _amount = 0;
        _amount = amount;
        _bustObj = transform.Find(type.ToString()).gameObject;
        _bustSound = bustsound;
    }


    //NATE NOTE
    //Maybe setup and animation corroutine here
    public void Spawn()        
    {
        if(_bustObj != null)
        {
            _bustObj.SetActive(true);
        }
    }


    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag != "Player") return;
        var player = col.transform.GetComponent<Player>();
        if (player)
        {
            player.Inventory.Set(_rt, _amount);
            Destroy(gameObject);
            SoundManager.PlaySound(_bustSound);
        }
    }
}

