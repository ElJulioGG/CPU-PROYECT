using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;  // Para el botón

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;  // Cámara principal
    public Camera[] otherCameras;  // Otras cámaras
    public List<float> switchTimes;  // Tiempos específicos para cada cámara secundaria
    public float mainCameraDuration;  // Duración inicial de la cámara principal
    public Transform player;  // Referencia al jugador
    public float playerSpeed = 2f;  // Velocidad del jugador
    public float moveDistance = 10f;  // Distancia de movimiento del jugador

    public Volume mainCameraVolume;  // Volumen para la cámara principal
    public Volume[] otherCameraVolumes;  // Volúmenes para las cámaras secundarias

    private int currentCameraIndex = -1;  // Índice de la cámara secundaria actual
    private float timer = 0f;  // Temporizador para cambiar las cámaras
    private bool isMainCameraActive = true;  // Indica si la cámara principal está activa
    private Vector3 playerStartPos;  // Posición inicial del jugador
    private bool movingRight = true;  // Determina si el jugador se mueve a la derecha o izquierda

    // Nuevo: Control de inicio del juego
    public Button startButton;  // Referencia al botón de inicio
    private bool startPlaying = false;  // Controla si el juego ha empezado

    void Start()
    {
        // Guardar la posición inicial del jugador
        playerStartPos = player.position;

        // Desactiva todo hasta que el jugador comience
        ActivateMainCamera();

        // Asignar un listener al botón de inicio
        startButton.onClick.AddListener(StartGame);
    }

    void Update()
    {
        if (!startPlaying) return;  // No hacer nada hasta que el jugador empiece

        // Mover al jugador de izquierda a derecha
        MovePlayer();

        // Incrementar el temporizador
        timer += Time.deltaTime;

        // Verificar si es momento de cambiar de cámara
        if (isMainCameraActive)
        {
            // Si la cámara principal está activa, cambiar a la siguiente cámara después del tiempo establecido
            if (timer >= mainCameraDuration)
            {
                SwitchToNextCamera();
            }
        }
        else
        {
            // Si una cámara secundaria está activa, volver a la principal después del tiempo específico
            if (timer >= switchTimes[currentCameraIndex])
            {
                ActivateMainCamera();
            }
        }

        // Si una cámara secundaria está activa, seguir al jugador
        if (!isMainCameraActive)
        {
            FollowPlayer(otherCameras[currentCameraIndex]);
        }
    }

    // Método para activar la cámara principal
    void ActivateMainCamera()
    {
        mainCamera.enabled = true;  // Solo activar el componente, no el GameObject

        foreach (Camera cam in otherCameras)
        {
            cam.gameObject.SetActive(false);  // Desactivar cámaras secundarias
        }

        foreach (Volume volume in otherCameraVolumes)
        {
            volume.enabled = false;  // Desactivar volúmenes secundarios
        }

        mainCameraVolume.enabled = true;  // Activar volumen de la cámara principal
        timer = 0f;
        isMainCameraActive = true;
    }

    // Método para cambiar a la siguiente cámara
    void SwitchToNextCamera()
    {
        mainCamera.enabled = false;  // Desactivar solo el componente Camera
        mainCameraVolume.enabled = false;  // Desactivar el volumen de la cámara principal

        currentCameraIndex = (currentCameraIndex + 1) % otherCameras.Length;

        for (int i = 0; i < otherCameras.Length; i++)
        {
            otherCameras[i].gameObject.SetActive(i == currentCameraIndex);  // Activar la cámara correspondiente
        }

        for (int i = 0; i < otherCameraVolumes.Length; i++)
        {
            otherCameraVolumes[i].enabled = (i == currentCameraIndex);  // Activar volumen correspondiente
        }

        timer = 0f;
        isMainCameraActive = false;
    }

    // Método para mover al jugador
    void MovePlayer()
    {
        if (movingRight)
        {
            player.position += Vector3.right * playerSpeed * Time.deltaTime;
            if (player.position.x >= playerStartPos.x + moveDistance)
            {
                movingRight = false;
            }
        }
        else
        {
            player.position += Vector3.left * playerSpeed * Time.deltaTime;
            if (player.position.x <= playerStartPos.x)
            {
                movingRight = true;
            }
        }
    }

    // Método para hacer que una cámara siga al jugador
    void FollowPlayer(Camera cam)
    {
        cam.transform.LookAt(player);  // La cámara sigue al jugador
    }

    // Método que se llama cuando el botón "Jugar" es presionado
    public void StartGame()
    {
        startPlaying = true;  // Inicia el cambio de cámara
        startButton.gameObject.SetActive(false);  // Ocultar el botón de inicio
    }
}
