using UnityEngine;
using TMPro;

public abstract class BasePrefab : MonoBehaviour
{
    public int Health;
    public AudioClip DestroySound;
    public TextMeshPro HealthText;

    protected int MaxHealth;

    protected PlayerUI PlayerUI
    {
        get
        {
            return UIManager.Instance.PlayerUIPanel;
        }
    }

    protected TargetUI TargetUI
    {
        get
        {
            return UIManager.Instance.TargetUI;
        }
    }

    protected virtual void Awake()
    {
      
    }
    
    protected void TagPrefab(string tag)
    {
        transform.tag = tag;
    }
    
    protected void OnMouseOver()
    {
        if(transform.tag == "Player") return;
        TargetPanel(true);
        TargetUI.Target.text = $"{Health}/{MaxHealth}";
    }

    protected void OnMouseExit()
    {
        ClearTarget();
    }

    private void ClearTarget()
    {
        TargetUI.Target.text = "";
        TargetPanel(false);
    }

    protected void TargetPanel(bool show)
    {
        UIManager.Instance.TargetPanel.gameObject.SetActive(show);
    }

    public virtual void SetHit(int amount)
    {
        if (Health - amount > 0)
        {
            Health -= amount;            
        }
        else
        {
            if (DestroySound != null)
                SoundManager.PlaySoundOnGameObject(gameObject, DestroySound);
            Destroy(gameObject);
            ClearTarget();
        }
    }       

    public override string ToString()
    {
        return Health.ToString();
    }
}
