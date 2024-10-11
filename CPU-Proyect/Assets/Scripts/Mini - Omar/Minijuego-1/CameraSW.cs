using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CameraSW : MonoBehaviour
{
    public Camera mainCamera;  // C�mara principal
    public Camera[] otherCameras;  // Otras c�maras
    public List<float> switchTimes;  // Tiempos espec�ficos para cada c�mara secundaria
    public List<float> mainCameraDurations;  // Lista de duraciones para la c�mara principal desde el Inspector
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

    public Button startButton;  // Referencia al bot�n de inicio
    private bool startPlaying = false;  // Controla si el juego ha empezado
    private int mainCameraDurationIndex = 0;  // �ndice para las duraciones de la c�mara principal
    private bool completedCameraSwitches = false;  // Bandera para determinar si el ciclo de cambio de c�maras ha terminado

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
                // Verificar si ya se han recorrido todas las c�maras
                if (mainCameraDurationIndex >= mainCameraDurations.Count - 1 && currentCameraIndex >= otherCameras.Length - 1)
                {
                    completedCameraSwitches = true;  // Marcar que ya no habr� m�s cambios de c�mara
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