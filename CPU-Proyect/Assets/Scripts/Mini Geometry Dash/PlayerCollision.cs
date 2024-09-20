using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameObject Player;
    private void Start()
    {
          Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.tag == "Obstacle")
        {
            Destroy(Player);
        }
    }
}
