using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject player;
    public GameObject gameOver;
    public GameObject gameWin;
    public GameManager gameManager;
    [SerializeField] private Vector3 startPos; //TEST


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Change scene 
            player.transform.position = startPos;
            Debug.Log("Aloooooooooooooo");
            gameWin.SetActive(true);

        }
    }
}
