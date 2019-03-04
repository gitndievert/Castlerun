using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region BaseStats
    public float Health;
    public float MoveSpeed;
    public float BuildSpeed;    
    #endregion


    public Companion Companion;

    private Inventory _inventory;
    private GameObject _mainHand;
    private GameObject _offHand;
           
    
    // Start is called before the first frame update
    void Start()
    {
        Health = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
