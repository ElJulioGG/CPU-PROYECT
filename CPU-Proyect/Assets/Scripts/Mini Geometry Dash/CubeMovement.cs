using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float MagnitudDeSalto = 15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetpos;
    [SerializeField] private float DistanciaSuelo = 0.009f;
    [SerializeField] private float jumptime = 0.3f; 
    [SerializeField] public bool isGrounded = false;
    private bool isJumping = false;
    private float jumptimer;
    private float speed = 7f;
    private void Update()
    {

        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);

        isGrounded = Physics2D.OverlapCircle(feetpos.position, DistanciaSuelo, groundLayer);
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            rb.velocity = Vector2.up * MagnitudDeSalto;
        }

        if(isJumping && Input.GetButton("Jump"))
        {
            if (jumptimer < jumptime)
            {
                rb.velocity = Vector2.up * MagnitudDeSalto;

                jumptimer += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            jumptimer = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Spike")
        {
            Destroy(gameObject);
        }
    }
}

