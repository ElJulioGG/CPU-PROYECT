using UnityEngine;
using UnityEngine.UI;

public class RhythmManager : MonoBehaviour
{
    public AudioSource gameplayMusic; // Música de juego
    public AudioSource startMusic; // Música que suena antes de empezar el juego
    public AudioSource winMusic; // Música que suena al ganar
    public AudioSource loseMusic; // Música que suena al perder
    public float beatMarginOfError = 0.1f;
    public int totalLives = 3;
    private int currentLives;
    public Text livesText;
    public Text gameOverText;

    public GameObject winObject;
    //public GameObject winCollider;
    public GameObject FlechasObject;
    public GameObject ButtonObject;
    public GameObject CameraObject;
    public Canvas gameOverCanvas; // Canvas para el mensaje de victoriawinObject
    public Canvas winCanvas; // Canvas para el mensaje de victoria
    public Canvas mainCanvas; // Canvas principal del juego
    public PlayerController playerController;
    public EnemyController enemyController;
    public CameraSW cameraSW;
    public bool isGameOver = false;
    public CameraShake cameraShake;
    public Text scoreText; // Texto para mostrar la puntuación
    public int score = 0; // Puntuación del jugador
    public int targetScore; // Puntuación para ganar

    private int missedArrowsCount = 0; // Contador de flechas perdidas

    void Start()
    {
        if (cameraShake == null)
        {
            cameraShake = Camera.main.GetComponent<CameraShake>();
        }

        currentLives = totalLives;
        UpdateLivesText();
        UpdateScoreText();
        gameOverText.text = "";
        gameOverCanvas.gameObject.SetActive(false);  // Desactivar el Canvas de Game Over inicialmente

        winCanvas.gameObject.SetActive(false);  // Desactivar el Canvas de victoria inicialmente
        mainCanvas.gameObject.SetActive(true);  // Inicialmente desactivamos el Canvas de victoria

        // Reproducir música de inicio antes de comenzar el juego
        if (startMusic != null)
        {
            startMusic.Play();
        }
    }

    void Update()
    {
        if (isGameOver)
        {
            return;
        }

        // Si el enemigo se voltea y el jugador no se ha movido, pierde una vida
        if (playerController.isMoving == false && enemyController.isTurned == true)
        {
            cameraShake.TriggerShake(0.2f);
            LoseLife();  // Resta una vida
            enemyController.isTurned = false;  // Resetear el estado de volteo del enemigo
        }
    }

    public void StartGame()
    {
        // Detener la música de inicio y comenzar la música de juego
        if (startMusic.isPlaying)
        {
            startMusic.Stop();
        }

        if (gameplayMusic != null)
        {
            gameplayMusic.Play(); // Comenzar la música de juego
        }
    }

    public bool CheckBeat(float currentTime)
    {
        float beatTime = GetNearestBeatTime(currentTime);

        if (Mathf.Abs(currentTime - beatTime) <= beatMarginOfError)
        {
            if (playerController.isMoving == false && enemyController.isTurned == true)
            {
                LoseLife(); // Pierde vida si el enemigo se voltea
                enemyController.isTurned = false;
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    float GetNearestBeatTime(float currentTime)
    {
        float beatInterval = 1f;
        int beatIndex = Mathf.RoundToInt(currentTime / beatInterval);
        return beatIndex * beatInterval;
    }

    // Método para incrementar el contador de flechas perdidas
    public void IncrementMissedArrows()
    {
        missedArrowsCount++;

        if (missedArrowsCount >= 10)
        {
            LoseLife();  // Pierde una vida si se pierden 10 flechas
            missedArrowsCount = 0; // Reinicia el contador después de perder una vida
        }
    }

    public void LoseLife()
    {
        currentLives--;

        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            UpdateLivesText();
        }
    }

    void UpdateLivesText()
    {
        if (livesText != null)
        {
            livesText.text = "Vidas: " + currentLives.ToString();
        }
    }

    void GameOver()
    {
        isGameOver = true;
        gameOverText.text = "Game Over";

        if (loseMusic != null)
        {
            loseMusic.Play(); // Reproducir la música de perder
        }

        if (gameplayMusic.isPlaying)
        {
            gameplayMusic.Stop(); // Detener la música de juego
        }

        playerController.enabled = false;
        enemyController.enabled = false;

        // Mostrar el Canvas de Game Over y ocultar el Canvas principal
        gameOverCanvas.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(false);
    }

    public void AddScore()
    {
        score++;
        UpdateScoreText();

        // Condición de victoria
        if (score >= targetScore)
        {
            WinGame();
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

   public void WinGame()
    {
        isGameOver = true;

        if (winMusic != null)
        {
            winMusic.Play(); // Reproducir la música de ganar
        }

        if (gameplayMusic.isPlaying)
        {
            gameplayMusic.Stop(); // Detener la música de juego
        }

        winCanvas.gameObject.SetActive(true);
        winObject.gameObject.SetActive(true);
        FlechasObject.gameObject.SetActive(false);
        ButtonObject.gameObject.SetActive(false);
        CameraObject.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(false);

        // Desactivar los controladores y elementos del juego
        playerController.enabled = false;
        enemyController.isGameWon = true;  // Notificamos al enemigo que se ganó el juego
        cameraSW.enabled = false;

        GameManagerM1.instance.GameWon(); // Llama al método GameWon de GameManagerM1
    }

    public bool HasLost()
    {
        return currentLives <= 0;
    }



}
