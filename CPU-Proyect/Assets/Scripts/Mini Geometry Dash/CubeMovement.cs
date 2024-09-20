using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float MagnitudDeSalto = 15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetpos;
    [SerializeField] private float DistanciaSuelo = 0.009f;

    [SerializeField] public bool isGrounded = false;
    private bool isJumping = false;

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetpos.position, DistanciaSuelo, groundLayer);
        if (isGrounded && Input.GetButton("Jump"))
        {
            isJumping = true;
            rb.velocity = Vector2.up * MagnitudDeSalto;
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }
}

