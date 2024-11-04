using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveDistance = 5f;  // Distancia que se mover� el jugador
    public float stayDuration = 5f;  // Tiempo de permanencia en la nueva posici�n
    public float originalXPosition = 1f;  // Posici�n original del jugador
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
            SetIdleAnimation();  // Muestra animaci�n Idle antes de que comience el juego
            return;
        }

        if (!isMoving)
        {
            SetIdleWalkingAnimation();  // Cambia a Idle_Walking despu�s de que comience el juego
            transform.position = new Vector3(originalXPosition, transform.position.y, transform.position.z);
        }
    }

    public void MovePlayerLeft()
    {
        if (!isMoving)
        {
            StartCoroutine(MovePlayer(-1f));  // Pasamos direcci�n negativa para la izquierda
        }
    }

    public void MovePlayerRight()
    {
        if (!isMoving)
        {
            StartCoroutine(MovePlayer(1f));  // Pasamos direcci�n positiva para la derecha
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

        SetMoveAnimation();  // Cambia a la animaci�n de movimiento
        // Cambiar la direcci�n del sprite en funci�n de la direcci�n
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

        // Verificar si el enemigo est� girado y si el jugador se movi� a tiempo
        if (enemyController.isTurned)
        {
            Debug.Log("El enemigo ha girado. Verificando si el jugador se movi�...");
            if (!IsMoving())  // Si el jugador no se movi� a tiempo
            {
                Debug.Log("El jugador no se movi� a tiempo. Quitando vida.");
                LoseLife();
                enemyController.isTurned = false;  // Resetea el estado de giro del enemigo
            }
        }

        yield return new WaitForSeconds(stayDuration);  // Tiempo de permanencia en la nueva posici�n

        // Regresa a la posici�n original
        transform.position = new Vector3(originalXPosition, transform.position.y, transform.position.z);
        SetPreMoveAnimation();  // Vuelve a la animaci�n pre-movimiento

        yield return new WaitForSeconds(0.3f);  // Espera un poco antes de terminar el movimiento
        SetIdleWalkingAnimation();  // Vuelve a Idle_Walking
        isMoving = false;
    }

    private void LoseLife()
    {
        // Implementa la l�gica para reducir la vida del jugador
        Debug.Log("Vida reducida.");
    }

    // M�todos para controlar las animaciones basadas en par�metros
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
