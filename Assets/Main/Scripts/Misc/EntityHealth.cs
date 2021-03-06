﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public GameObject deathPrefab;
    public bool inheritParentScale;
    public float scaleMultiplier;
    public bool isDead;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void DealDamage(float damageToDeal)
    {
        currentHealth -= damageToDeal;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        if (deathPrefab != null)
        {
            // Create a death effect particle effect.
            GameObject deathEffect = Instantiate(deathPrefab);
            deathEffect.transform.position = this.transform.position;
            if (inheritParentScale)
            {
                deathEffect.transform.localScale = transform.localScale * scaleMultiplier;
            }
        }

        Game.AddScore(1);
        CharacterMaster.instance.Health.currentHealth += 5;
        isDead = true;
        Destroy(this.gameObject,0.2f);
    }
}
