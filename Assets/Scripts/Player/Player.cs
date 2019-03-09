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

    private string _playerName;

    public string PlayerName
    {
        get { return _playerName; }
        set
        {
            if (value.Length > 20)
                _playerName = value.Substring(0, 20);
            else
                _playerName = value;
        }
    }
    

    public Companion Companion { get; private set; }
    public Inventory Inventory { get; private set; }
    public int ActorNumber { get; internal set; }

    private GameObject _mainHand;
    private GameObject _offHand;


    private void Awake()
    {
        Inventory = GetComponent<Inventory>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        Health = 100f;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnDestroy()
    {
        
    }



}
