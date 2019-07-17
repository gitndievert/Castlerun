using UnityEngine;
using TMPro;

public abstract class BasePrefab : MonoBehaviour
{
    public int Health;
    public AudioClip DestroySound;
    public TextMeshPro HealthText;

    protected int MaxHealth;

    protected virtual void Awake()
    {
      
    }
    
    protected void TagPrefab(string tag)
    {
        transform.tag = tag;
    }

    protected void SetHealthText(int max)
    {
        SetHealthText(max, max);
    }   

    protected void SetHealthText(int current, int max)
    {
        if (HealthText != null)
            HealthText.text = $"{current}/{max}";
    }

    public virtual void SetHit(int amount)
    {
        if (Health - amount > 0)
        {
            Health -= amount;
            SetHealthText(Health, MaxHealth);
        }
        else
        {
            if (DestroySound != null)
                SoundManager.PlaySoundOnGameObject(gameObject, DestroySound);
            Destroy(gameObject);
        }
    }       

    public override string ToString()
    {
        return Health.ToString();
    }
}
