using UnityEngine;
using TMPro;

public class Bust : BasePrefab
{
    /// <summary>
    /// Two minute life time!
    /// </summary>
    const float LIFE_TIME = 120f;
       
    public bool GravityPull = false;

    //Maybe for future
    //public TextMeshPro ResouceText;
    //public TextMeshPro ResouceAmt;

    private int _amount;
    private ResourceType _rt;
    private GameObject _bustObj;
    private AudioClip _bustSound;
    private Transform _backplane;


    protected override void Start()
    {
        base.Start();              
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

    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag != "Player") return;
        var player = col.transform.GetComponent<Player>();
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

