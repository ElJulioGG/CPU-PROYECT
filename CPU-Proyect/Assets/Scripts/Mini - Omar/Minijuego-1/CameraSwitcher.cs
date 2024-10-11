using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;  // Para el bot�n

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;  // C�mara principal
    public Camera[] otherCameras;  // Otras c�maras
    public List<float> switchTimes;  // Tiempos espec�ficos para cada c�mara secundaria
    public float mainCameraDuration;  // Duraci�n inicial de la c�mara principal
    public Transform player;  // Referencia al jugador
    public float playerSpeed = 2f;  // Velocidad del jugador
    public float moveDistance = 10f;  // Distancia de movimiento del jugador

    public Volume mainCameraVolume;  // Volumen para la c�mara principal
    public Volume[] otherCameraVolumes;  // Vol�menes para las c�maras secundarias

    private int currentCameraIndex = -1;  // �ndice de la c�mara secundaria actual
    private float timer = 0f;  // Temporizador para cambiar las c�maras
    private bool isMainCameraActive = true;  // Indica si la c�mara principal est� activa
    private Vector3 playerStartPos;  // Posici�n inicial del jugador
    private bool movingRight = true;  // Determina si el jugador se mueve a la derecha o izquierda

    // Nuevo: Control de inicio del juego
    public Button startButton;  // Referencia al bot�n de inicio
    private bool startPlaying = false;  // Controla si el juego ha empezado

    void Start()
    {
        // Guardar la posici�n inicial del jugador
        playerStartPos = player.position;

        // Desactiva todo hasta que el jugador comience
        ActivateMainCamera();

        // Asignar un listener al bot�n de inicio
        startButton.onClick.AddListener(StartGame);
    }

    void Update()
    {
        if (!startPlaying) return;  // No hacer nada hasta que el jugador empiece

        // Mover al jugador de izquierda a derecha
        MovePlayer();

        // Incrementar el temporizador
        timer += Time.deltaTime;

        // Verificar si es momento de cambiar de c�mara
        if (isMainCameraActive)
        {
            // Si la c�mara principal est� activa, cambiar a la siguiente c�mara despu�s del tiempo establecido
            if (timer >= mainCameraDuration)
            {
                SwitchToNextCamera();
            }
        }
        else
        {
            // Si una c�mara secundaria est� activa, volver a la principal despu�s del tiempo espec�fico
            if (timer >= switchTimes[currentCameraIndex])
            {
                ActivateMainCamera();
            }
        }

        // Si una c�mara secundaria est� activa, seguir al jugador
        if (!isMainCameraActive)
        {
            FollowPlayer(otherCameras[currentCameraIndex]);
        }
    }

    // M�todo para activar la c�mara principal
    void ActivateMainCamera()
    {
        mainCamera.enabled = true;  // Solo activar el componente, no el GameObject

        foreach (Camera cam in otherCameras)
        {
            cam.gameObject.SetActive(false);  // Desactivar c�maras secundarias
        }

        foreach (Volume volume in otherCameraVolumes)
        {
            volume.enabled = false;  // Desactivar vol�menes secundarios
        }

        mainCameraVolume.enabled = true;  // Activar volumen de la c�mara principal
        timer = 0f;
        isMainCameraActive = true;
    }

    // M�todo para cambiar a la siguiente c�mara
    void SwitchToNextCamera()
    {
        mainCamera.enabled = false;  // Desactivar solo el componente Camera
        mainCameraVolume.enabled = false;  // Desactivar el volumen de la c�mara principal

        currentCameraIndex = (currentCameraIndex + 1) % otherCameras.Length;

        for (int i = 0; i < otherCameras.Length; i++)
        {
            otherCameras[i].gameObject.SetActive(i == currentCameraIndex);  // Activar la c�mara correspondiente
        }

        for (int i = 0; i < otherCameraVolumes.Length; i++)
        {
            otherCameraVolumes[i].enabled = (i == currentCameraIndex);  // Activar volumen correspondiente
        }

        timer = 0f;
        isMainCameraActive = false;
    }

    // M�todo para mover al jugador
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

    // M�todo para hacer que una c�mara siga al jugador
    void FollowPlayer(Camera cam)
    {
        cam.transform.LookAt(player);  // La c�mara sigue al jugador
    }

    // M�todo que se llama cuando el bot�n "Jugar" es presionado
    public void StartGame()
    {
        startPlaying = true;  // Inicia el cambio de c�mara
        startButton.gameObject.SetActive(false);  // Ocultar el bot�n de inicio
    }
}
