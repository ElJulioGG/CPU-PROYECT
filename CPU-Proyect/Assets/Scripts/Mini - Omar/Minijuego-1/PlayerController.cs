using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveDistance = 5f;  // Distancia que se mover� el jugador
    public float stayDuration = 5f;   // Tiempo de permanencia en la nueva posici�n
    public float originalXPosition = 1f; // Posici�n original del jugador
    public bool isMoving=false; // Bandera para controlar el movimiento

    public Renderer playerRenderer;
    private Color idleColor = Color.blue;   // Color por defecto
    private Color preMoveColor = Color.yellow; // Color antes de moverse
    private Color moveColor = Color.red;    // Color cuando se mueve

    private EnemyController enemyController; // Referencia al controlador del enemigo

    void Start()
    {
        // Asigna la referencia al EnemyController
        enemyController = FindObjectOfType<EnemyController>();
    }

    void Update()
    {
        if (!GameManagerM1.instance.startPlaying)  // Espera a que empiece el juego
        {
            return;
        }

        if (!isMoving)
        {
            playerRenderer.material.color = idleColor;
            transform.position = new Vector3(originalXPosition, transform.position.y, transform.position.z);
        }
    }

    public void MovePlayerLeft()
    {
        if (!isMoving)
        {
            StartCoroutine(MovePlayer(-1f));
        }
    }

    public void MovePlayerRight()
    {
        if (!isMoving)
        {
            StartCoroutine(MovePlayer(1f));
        }
    }

    public bool IsMoving()
    {
        return isMoving; // Devuelve el estado del movimiento
    }

    IEnumerator MovePlayer(float direction)
    {
        isMoving = true;

        playerRenderer.material.color = preMoveColor;
        yield return new WaitForSeconds(0.3f);

        playerRenderer.material.color = moveColor;

        // Calcula la nueva posici�n del jugador
        Vector3 newPosition = new Vector3(originalXPosition + (direction * moveDistance), transform.position.y, transform.position.z);
        transform.position = newPosition;

        if (enemyController.isTurned)
        {
            Debug.Log("El enemigo ha girado. Verificando si el jugador se movi�...");
            if (!IsMoving())  // Si el jugador no se movi�
            {
                Debug.Log("El jugador no se movi� a tiempo. Quitando vida.");
                LoseLife(); // Implementa este m�todo para reducir la vida del jugador
                enemyController.isTurned = false; // Resetea el estado de giro del enemigo
            }
        }

        yield return new WaitForSeconds(stayDuration);

        // Regresa a la posici�n original
        transform.position = new Vector3(originalXPosition, transform.position.y, transform.position.z);
        playerRenderer.material.color = preMoveColor;

        yield return new WaitForSeconds(0.3f);
        playerRenderer.material.color = idleColor;

        isMoving = false;
    }

    private void LoseLife()
    {
        // Implementa la l�gica para reducir la vida del jugador
        Debug.Log("Vida reducida.");
    }
}
