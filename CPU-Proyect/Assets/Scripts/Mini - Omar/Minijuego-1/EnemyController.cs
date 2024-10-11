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
    public Color turnColor = Color.yellow;     // Color cuando va a girar
    public Color turnedColor = Color.red;      // Color cuando ya ha girado

    private Renderer enemyRenderer;            // Referencia al componente Renderer
    private bool isMoving = true;              // Bandera para controlar el movimiento
    public bool isTurned = false;
    private bool hasStartedBehavior = false;   // Nuevo: bandera para iniciar el comportamiento

    void Start()
    {
        enemyRenderer = GetComponent<Renderer>(); // Obtiene el componente Renderer
        enemyRenderer.material.color = defaultColor; // Establece el color inicial
    }

    void Update()
    {
        if (!GameManagerM1.instance.startPlaying)  // Espera a que empiece el juego
        {
            return;
        }

        if (!hasStartedBehavior)
        {
            // Inicia la Coroutine de comportamiento solo cuando el juego comienza
            StartCoroutine(EnemyBehavior());
            hasStartedBehavior = true;
        }

        if (isMoving)
        {
            // Mueve al enemigo hacia adelante
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    // Coroutine que controla el comportamiento del enemigo
    private IEnumerator EnemyBehavior()
    {
        // Camina durante el primer tiempo específico
        yield return new WaitForSeconds(firstWalkDuration);  // Tiempo de caminata inicial

        while (true) // Loop infinito para el comportamiento
        {
            // Cambia a color amarillo antes de girar
            enemyRenderer.material.color = turnColor;

            // Gira el enemigo
            yield return new WaitForSeconds(turnDuration); // Tiempo de giro
            isTurned = true;

            enemyRenderer.material.color = turnedColor;
            TurnEnemy();
            yield return new WaitForSeconds(turnedDuration); // Tiempo girado

            // Cambia de nuevo a color amarillo antes de girar de nuevo
            isTurned = false;
            enemyRenderer.material.color = turnColor;
            yield return new WaitForSeconds(turnDuration); // Tiempo de giro

            // Cambia de nuevo al color por defecto
            enemyRenderer.material.color = defaultColor;

            // Camina durante el tiempo específico de 10 segundos en ciclos posteriores
            yield return new WaitForSeconds(subsequentWalkDuration); // Tiempo de caminata en ciclos posteriores
        }
    }

    public void TurnEnemy()
    {
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
}
