using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public float speed = 5f;                  // Velocidad del enemigo
    public float firstWalkDuration = 15.4f;   // Tiempo que camina en el primer ciclo
    public float subsequentWalkDuration = 10f; // Tiempo que camina en ciclos posteriores
    public float turnDuration = 1.5f;         // Tiempo que tarda en girar
    public float turnedDuration = 2f;         // Tiempo que permanece girado
    public Color defaultColor = Color.white;  // Color por defecto del enemigo
    public Color turnColor = Color.yellow;    // Color cuando va a girar
    public Color turnedColor = Color.red;     // Color cuando ya ha girado
    public bool isGameWon = false;

    private Renderer enemyRenderer;           // Referencia al componente Renderer
    private Animator animator;                // Referencia al componente Animator
    private bool isMoving = true;             // Bandera para controlar el movimiento
    public bool isTurned = false;
    private bool hasStartedBehavior = false;  // Nuevo: bandera para iniciar el comportamiento

    void Start()
    {
        enemyRenderer = GetComponent<Renderer>(); // Obtiene el componente Renderer
        enemyRenderer.material.color = defaultColor; // Establece el color inicial
        animator = GetComponent<Animator>();  // Obtiene el componente Animator

        SetIdleAnimation();  // Inicia con la animación Idle por defecto
    }

    void Update()
    {
        if (isGameWon)
        {
            SetIdleAnimation(); // Asegúrate de que la animación Idle esté activada
            StopMovement();     // Detiene el movimiento cuando se gana el juego
            return;             // Termina la ejecución del Update si el juego ha terminado
        }

        if (!GameManagerM1.instance.startPlaying)
        {
            SetIdleAnimation();
            return;
        }

        if (!hasStartedBehavior)
        {
            StartCoroutine(EnemyBehavior());
            hasStartedBehavior = true;
        }

        if (isMoving)
        {
            SetIdleWalkingAnimation();
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    // Coroutine que controla el comportamiento del enemigo
    private IEnumerator EnemyBehavior()
    {
        yield return new WaitForSeconds(firstWalkDuration);  // Tiempo de caminata inicial

        while (true) // Loop infinito para el comportamiento
        {
            if (isGameWon) yield break;  // Si el juego ha sido ganado, detener el comportamiento

            enemyRenderer.material.color = turnColor;
            SetPreMoveAnimation();
            yield return new WaitForSeconds(turnDuration); // Tiempo de giro
            isTurned = true;

            enemyRenderer.material.color = turnedColor;
            TurnEnemy();
            SetMoveAnimation();  // Cambia a la animación de movimiento
            yield return new WaitForSeconds(turnedDuration); // Tiempo girado

            // Cambia de nuevo a color amarillo antes de girar de nuevo
            isTurned = false;
            enemyRenderer.material.color = turnColor;
            SetPreMoveAnimation();  // Cambia a la animación pre-movimiento antes de girar
            yield return new WaitForSeconds(turnDuration); // Tiempo de giro

            // Cambia de nuevo al color por defecto
            enemyRenderer.material.color = defaultColor;
            SetIdleWalkingAnimation();  // Cambia a Idle_Walking cuando no se esté moviendo
            yield return new WaitForSeconds(subsequentWalkDuration); // Tiempo de caminata en ciclos posteriores
        }
    }

    public void TurnEnemy()
    {
        SetMoveAnimation();  // Cambia a la animación pre-movimiento antes de girar
        transform.Rotate(0, 180, 0);
        isTurned = true;
    }

    public void StopMovement()
    {
        isMoving = false;
    }

    public void ResumeMovement()
    {
        isMoving = true; // Reanuda el movimiento
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
        Debug.Log("Setting PreMove Animation");
        animator.SetBool("isIdle", false);
        animator.SetBool("isIdleWalking", false);
        animator.SetBool("isPreMove", true);
        animator.SetBool("isMoving", false);
    }

    private void SetMoveAnimation()
    {
        Debug.Log("Setting Move Animation");
        animator.SetBool("isIdle", false);
        animator.SetBool("isIdleWalking", false);
        animator.SetBool("isPreMove", false);
        animator.SetBool("isMoving", true);
    }
}
