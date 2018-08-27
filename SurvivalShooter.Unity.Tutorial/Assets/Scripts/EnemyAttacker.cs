using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour {

    [HideInInspector] public float timer;
    public bool playerInRange { get; private set; }

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find(GameString.player);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.Equals(player))
        {
            playerInRange = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.Equals(player))
        {
            playerInRange = false;
        }
    }
}
