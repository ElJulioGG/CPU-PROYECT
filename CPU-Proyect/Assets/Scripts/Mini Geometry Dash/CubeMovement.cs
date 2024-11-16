using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float MagnitudDeSaltoMax = 15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetpos;
    [SerializeField] private float DistanciaSuelo = 0.009f;
    [SerializeField] private float jumptimeMax = 0.3f;
    [SerializeField] public bool isGrounded = false;
    [SerializeField] VictoryScreen victoryScreen;
    private bool isChargingJump = false;
    private float jumptimer;
    private float speed = 7f;
    public bool GameOver = false;


    private void Start()
    {
    }
    private void Update()
    {
        if (GameOver == false)
        {
            // Movimiento horizontal del cubo
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);

            // Comprobar si el personaje está en el suelo
            isGrounded = Physics2D.OverlapCircle(feetpos.position, DistanciaSuelo, groundLayer);

            // Comienza a cargar el salto cuando el botón de salto se presiona estando en el suelo
            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                isChargingJump = true;
                //Regresa valor inicial
                jumptimer = 0f; // Reinicia el temporizador del salto
            }

            //Si en el aire se preciona otra vez cae rapidamente
            if (!isGrounded && Input.GetButtonDown("Jump"))
            {

                isChargingJump = true;
                rb.gravityScale = 50;
                jumptimer = 0f;
            }

            // Incrementa el tiempo de carga del salto mientras se mantenga presionado el botón
            if (isChargingJump && Input.GetButton("Jump"))
            {
                if (jumptimer < jumptimeMax)
                {
                    jumptimer += Time.deltaTime; // Acumula el tiempo de carga hasta el máximo permitido
                }
            }

            // Cuando se suelta el botón de salto, aplicar la fuerza del salto en función del tiempo cargado
            if (isChargingJump && Input.GetButtonUp("Jump"))
            {
                float jumpForce = Mathf.Lerp(0, MagnitudDeSaltoMax, jumptimer / jumptimeMax); // Calcular fuerza de salto
                rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Aplicar la fuerza de salto
                isChargingJump = false; // Detener la carga de salto
            }

            if (isGrounded)
            {
                rb.gravityScale = 5;
            }
        }
    }

    // Detectar colisión con objetos
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Spike" || other.transform.tag == "Obstacle" || other.transform.tag == "Deadzone")
        {
            transform.position = new Vector2(-9.90f, -3.4f);  // Destruir el objeto si colisiona con un "Spike"
        }

        if(other.transform.tag == "Meta")
        {
            victoryScreen.Screen();
            GameOver = true;
        }
    }


}
