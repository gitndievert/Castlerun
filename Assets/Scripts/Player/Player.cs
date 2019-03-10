using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public StatModifier StatsModifier;
    
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

    #region Player Components
    public Companion Companion { get; private set; }
    public Inventory Inventory { get; private set; }
    public int ActorNumber { get; internal set; }
    #endregion

    private GameObject _mainHand;
    private GameObject _offHand;
    private Stats _stat;

    private void Awake()
    {
        Inventory = GetComponent<Inventory>();
        StatsModifier = GetComponent<StatModifier>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _stat = StatModifier.CurrentStats;
    }

    // Update is called once per frame
    private void Update()
    {        
        Health = _stat.Health;
        MoveSpeed = _stat.MoveSpeed;
        BuildSpeed = _stat.BuildSpeed;
    }

    private void OnDestroy()
    {
        
    }



}
