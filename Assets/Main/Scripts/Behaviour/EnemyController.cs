﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1;
    private CharacterMaster player;
    private bool hasAttacked;

    private void Awake()
    {
        player = CharacterMaster.instance;
    }

    private void Update()
    {
        if (player != null)
        {
            TrackPlayer();
            AttackPlayer();
        }
    }

    public void TrackPlayer()
    {
        transform.position = Vector3.Slerp(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void AttackPlayer()
    {
        if (!hasAttacked && Vector3.Distance(transform.position, player.transform.position) < 3)
        {
            player.GetComponent<EntityHealth>().DealDamage(10);
            GetComponent<EntityHealth>().Death();
            hasAttacked = true;
        }
    }
}
