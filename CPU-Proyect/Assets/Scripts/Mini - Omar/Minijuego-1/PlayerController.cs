using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveDistance = 5f;  // Distancia que se moverá el jugador
    public float stayDuration = 5f;  // Tiempo de permanencia en la nueva posición
    public float originalXPosition = 1f;  // Posición original del jugador
    public bool isMoving = false;  // Bandera para controlar el movimiento

    private Animator animator;  // Referencia al componente Animator
    private EnemyController enemyController;  // Referencia al controlador del enemigo

    void Start()
    {
        // Asigna la referencia al EnemyController
        enemyController = FindObjectOfType<EnemyController>();
        animator = GetComponent<Animator>();
        SetIdleAnimation();  // Por defecto, comenzamos en estado idle
    }

    void Update()
    {
        if (!GameManagerM1.instance.startPlaying)  // Espera a que empiece el juego
        {
            SetIdleAnimation();  // Muestra animación Idle antes de que comience el juego
            return;
        }

        if (!isMoving)
        {
            SetIdleWalkingAnimation();  // Cambia a Idle_Walking después de que comience el juego
            transform.position = new Vector3(originalXPosition, transform.position.y, transform.position.z);
        }
    }

    public void MovePlayerLeft()
    {
        if (!isMoving)
        {
            StartCoroutine(MovePlayer(-1f));  // Pasamos dirección negativa para la izquierda
        }
    }

    public void MovePlayerRight()
    {
        if (!isMoving)
        {
            StartCoroutine(MovePlayer(1f));  // Pasamos dirección positiva para la derecha
        }
    }

    public bool IsMoving()
    {
        return isMoving;  // Devuelve el estado del movimiento
    }

    IEnumerator MovePlayer(float direction)
    {
        isMoving = true;

     
        SetPreMoveAnimation();
        yield return new WaitForSeconds(0.3f);  // Espera antes de moverse

        SetMoveAnimation();  // Cambia a la animación de movimiento
        // Cambiar la dirección del sprite en función de la dirección
        if (direction < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);  // Gira a la izquierda
        }
        else if (direction > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);  // Gira a la derecha
        }

        Vector3 newPosition = new Vector3(originalXPosition + (direction * moveDistance), transform.position.y, transform.position.z);
        transform.position = newPosition;

        // Verificar si el enemigo está girado y si el jugador se movió a tiempo
        if (enemyController.isTurned)
        {
            Debug.Log("El enemigo ha girado. Verificando si el jugador se movió...");
            if (!IsMoving())  // Si el jugador no se movió a tiempo
            {
                Debug.Log("El jugador no se movió a tiempo. Quitando vida.");
                LoseLife();
                enemyController.isTurned = false;  // Resetea el estado de giro del enemigo
            }
        }

        yield return new WaitForSeconds(stayDuration);  // Tiempo de permanencia en la nueva posición

        // Regresa a la posición original
        transform.position = new Vector3(originalXPosition, transform.position.y, transform.position.z);
        SetPreMoveAnimation();  // Vuelve a la animación pre-movimiento

        yield return new WaitForSeconds(0.3f);  // Espera un poco antes de terminar el movimiento
        SetIdleWalkingAnimation();  // Vuelve a Idle_Walking
        isMoving = false;
    }

    private void LoseLife()
    {
        // Implementa la lógica para reducir la vida del jugador
        Debug.Log("Vida reducida.");
    }

    // Métodos para controlar las animaciones basadas en parámetros
    private void SetIdleAnimation()
    {
        animator.SetBool("isIdle", true);
        animator.SetBool("isIdleWalking", false);
        animator.SetBool("isPreMove", false);
        animator.SetBool("isMoving", false);
    }

    private void SetIdleWalkingAnimation()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isIdleWalking", true);
        animator.SetBool("isPreMove", false);
        animator.SetBool("isMoving", false);
    }

    private void SetPreMoveAnimation()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isIdleWalking", false);
        animator.SetBool("isPreMove", true);
        animator.SetBool("isMoving", false);
    }

    private void SetMoveAnimation()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isIdleWalking", false);
        animator.SetBool("isPreMove", false);
        animator.SetBool("isMoving", true);
    }
}
