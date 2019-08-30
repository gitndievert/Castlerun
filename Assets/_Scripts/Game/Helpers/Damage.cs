using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Damage
{

    public static int GetDamage(int min, int max, bool hascritical = false)
    {
        var dmg = Random.Range(min, max);
        if (Random.Range(0, 20) > 17) dmg *= 2;
        return dmg;
    }

    public static void ApplyDamage(Collision col, int mindmg, int maxdmg, bool hascritical = false)
    {
        var contact = col.transform.GetComponent<BasePrefab>();

        //If contact is dying but not destroyed then don't do damage
        if (contact.IsDying) return;

        int damage = GetDamage(mindmg, maxdmg, hascritical);

        if (damage > maxdmg)
        {
            Debug.Log($"CRITICAL HIT! {damage}");
        }
        else
        {
            Debug.Log($"Damage for {damage}");
        }

        contact.SetHit(damage);        
    }

    public static void ApplyDamage(ISelectable enemy, int mindmg, int maxdmg, bool hascritical = false)
    {
        //If contact is dying but not destroyed then don't do damage
        if (enemy.IsDying) return;

        int damage = GetDamage(mindmg, maxdmg, hascritical);

        if (damage > maxdmg)
        {
            Debug.Log($"CRITICAL HIT! {damage}");
        }
        else
        {
            Debug.Log($"Damage for {damage}");
        }

        enemy.SetHit(damage);
    }







}
