using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CameraSW : MonoBehaviour
{
    public Camera mainCamera;  // Cámara principal
    public Camera[] otherCameras;  // Otras cámaras
    public List<float> switchTimes;  // Tiempos específicos para cada cámara secundaria
    public List<float> mainCameraDurations;  // Lista de duraciones para la cámara principal desde el Inspector
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

    public Button startButton;  // Referencia al botón de inicio
    private bool startPlaying = false;  // Controla si el juego ha empezado
    private int mainCameraDurationIndex = 0;  // Índice para las duraciones de la cámara principal
    private bool completedCameraSwitches = false;  // Bandera para determinar si el ciclo de cambio de cámaras ha terminado

    void Start()
    {
        playerStartPos = player.position;

        ActivateMainCamera();

        startButton.onClick.AddListener(StartGame);
    }

    void Update()
    {
        if (!startPlaying || completedCameraSwitches) return;  // No hacer nada hasta que el jugador empiece y si el ciclo ha terminado

        MovePlayer();

        timer += Time.deltaTime;

        if (isMainCameraActive)
        {
            if (timer >= mainCameraDurations[mainCameraDurationIndex])
            {
                // Verificar si ya se han recorrido todas las cámaras
                if (mainCameraDurationIndex >= mainCameraDurations.Count - 1 && currentCameraIndex >= otherCameras.Length - 1)
                {
                    completedCameraSwitches = true;  // Marcar que ya no habrá más cambios de cámara
                }
                else
                {
                    SwitchToNextCamera();
                    mainCameraDurationIndex = (mainCameraDurationIndex + 1) % mainCameraDurations.Count;
                }
            }
        }
        else
        {
            if (timer >= switchTimes[currentCameraIndex])
            {
                ActivateMainCamera();
            }
        }

        if (!isMainCameraActive)
        {
            FollowPlayer(otherCameras[currentCameraIndex]);
        }
    }

    void ActivateMainCamera()
    {
        mainCamera.enabled = true;

        foreach (Camera cam in otherCameras)
        {
            cam.gameObject.SetActive(false);
        }

        foreach (Volume volume in otherCameraVolumes)
        {
            volume.enabled = false;
        }

        mainCameraVolume.enabled = true;
        timer = 0f;
        isMainCameraActive = true;
    }

    void SwitchToNextCamera()
    {
        mainCamera.enabled = false;
        mainCameraVolume.enabled = false;

        currentCameraIndex = (currentCameraIndex + 1) % otherCameras.Length;

        for (int i = 0; i < otherCameras.Length; i++)
        {
            otherCameras[i].gameObject.SetActive(i == currentCameraIndex);
        }

        for (int i = 0; i < otherCameraVolumes.Length; i++)
        {
            otherCameraVolumes[i].enabled = (i == currentCameraIndex);
        }

        timer = 0f;
        isMainCameraActive = false;
    }

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

    void FollowPlayer(Camera cam)
    {
        cam.transform.LookAt(player);
    }

    public void StartGame()
    {
        startPlaying = true;
        startButton.gameObject.SetActive(false);
    }
}