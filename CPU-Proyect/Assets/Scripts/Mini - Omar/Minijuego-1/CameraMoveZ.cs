using UnityEngine;

public class CameraMoveZ : MonoBehaviour
{
    public float speed = 5f;
    public float[] movementTimes;
    public float[] idleTimes;
    private int currentIndex = 0;
    private bool isMoving = false;
    private float timer = 0f;
    public PlayerController playerController;
    private RhythmManager rhythmManager; // Referencia al RhythmManager

     void Start()
    {
        rhythmManager = FindObjectOfType<RhythmManager>(); // Obtener la instancia del RhythmManager

    }
    void Update()
    {
        // Verifica si el juego ha terminado (ya sea por ganar o perder)
        if (rhythmManager.isGameOver)
        {
            return; // Si el juego ha terminado, no se ejecuta el movimiento
        }

        if (!GameManagerM1.instance.startPlaying)  // Espera a que empiece el juego
        {
            return;
        }

        timer += Time.deltaTime;



        timer += Time.deltaTime;

        if (isMoving)
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;

            if (timer >= movementTimes[currentIndex])
            {
                timer = 0f;
                isMoving = false;
            }
        }
        else
        {
            if (timer >= idleTimes[currentIndex])
            {
                timer = 0f;
                isMoving = true;
                currentIndex++;

                if (currentIndex >= movementTimes.Length || currentIndex >= idleTimes.Length)
                {
                    currentIndex = 0;
                }

            }
        }
    }
}
