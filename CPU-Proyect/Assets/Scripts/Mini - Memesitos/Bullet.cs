using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife = 1f;  // Defines how long before the bullet is destroyed
    public float speed = 1f;
    public float rotation = 0f;

    private Rigidbody2D rb;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Mathf.Cos(rotation), Mathf.Sin(rotation)).normalized;
        rb.velocity = transform.right * speed;
        AudioManager.instance.PlayUI("EnemyShoot1");
    }
    //ss

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > bulletLife)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Handle collision logic here
        // Example: if (other.gameObject.CompareTag("Player"))
        // {
        //     Destroy(gameObject);
        // }
    }
}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class Bullet : MonoBehaviour
//{
//    public float bulletLife = 1f;  // Defines how long before the bullet is destroyed
//    public float rotation = 0f;
//    public float speed = 1f;


//    private Vector2 spawnPoint;
//    private float timer = 0f;


//    // Start is called before the first frame update
//    void Start()
//    {
//        spawnPoint = new Vector2(transform.position.x, transform.position.y);
//    }


//    // Update is called once per frame
//    void Update()
//    {
//        if (timer > bulletLife) Destroy(this.gameObject);
//        timer += Time.deltaTime;
//        transform.position = Movement(timer);
//    }


//    private Vector2 Movement(float timer)
//    {
//        // Moves right according to the bullet's rotation
//        float x = timer * speed * transform.right.x;
//        float y = timer * speed * transform.right.y;
//        return new Vector2(x + spawnPoint.x, y + spawnPoint.y);
//    }

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        //if (other.gameObject.CompareTag("Player"))
//        //{
//        //    Destroy(gameObject);
//        //}
//    }

//}

