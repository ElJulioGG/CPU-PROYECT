using UnityEngine;
using UnityEngine.UI;

public class RhythmManager : MonoBehaviour
{
    public AudioSource gameplayMusic; // M�sica de juego
    public AudioSource startMusic; // M�sica que suena antes de empezar el juego
    public AudioSource winMusic; // M�sica que suena al ganar
    public AudioSource loseMusic; // M�sica que suena al perder
    public float beatMarginOfError = 0.1f;
    public int totalLives = 3;
    private int currentLives;
    public Text livesText;
    public Text gameOverText;
    public Canvas winCanvas; // Canvas para el mensaje de victoria
    public GameObject winObject;
    public Canvas mainCanvas; // Canvas principal del juego
    public PlayerController playerController;
    public EnemyController enemyController;
    public bool isGameOver = false;
    public CameraShake cameraShake;
    public Text scoreText; // Texto para mostrar la puntuaci�n
    public int score = 0; // Puntuaci�n del jugador
    public int targetScore = 10; // Puntuaci�n para ganar

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
        winCanvas.gameObject.SetActive(false);  // Desactivar el Canvas de victoria inicialmente
        mainCanvas.gameObject.SetActive(true);  // Inicialmente desactivamos el Canvas de victoria

        // Reproducir m�sica de inicio antes de comenzar el juego
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
        if (playerController.isMoving == false && enemyController.isTurned == true)
        {
            cameraShake.TriggerShake(0.2f);
            LoseLife();
            enemyController.isTurned = false;
        }
    }

    public void StartGame()
    {
        // Detener la m�sica de inicio y comenzar la m�sica de juego
        if (startMusic.isPlaying)
        {
            startMusic.Stop();
        }

        if (gameplayMusic != null)
        {
            gameplayMusic.Play(); // Comenzar la m�sica de juego
        }
    }

    public bool CheckBeat(float currentTime)
    {
        float beatTime = GetNearestBeatTime(currentTime);

        if (Mathf.Abs(currentTime - beatTime) <= beatMarginOfError)
        {
            if (playerController.isMoving == false && enemyController.isTurned == true)
            {
                LoseLife();
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
            loseMusic.Play(); // Reproducir la m�sica de perder
        }

        if (gameplayMusic.isPlaying)
        {
            gameplayMusic.Stop(); // Detener la m�sica de juego
        }

        playerController.enabled = false;
        enemyController.enabled = false;
    }

    public void AddScore()
    {
        score++;
        UpdateScoreText();

        // Condici�n de victoria
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

    void WinGame()
    {
        isGameOver = true;

        if (winMusic != null)
        {
            winMusic.Play(); // Reproducir la m�sica de ganar
        }

        if (gameplayMusic.isPlaying)
        {
            gameplayMusic.Stop(); // Detener la m�sica de juego
        }

        winCanvas.gameObject.SetActive(true);
        winObject.gameObject.SetActive(true);  // Activamos el Canvas de victoria
        mainCanvas.gameObject.SetActive(false);  // Desactivamos el Canvas principal
        playerController.enabled = false;
        enemyController.enabled = false;

        GameManagerM1.instance.GameWon(); // Llama al m�todo GameWon de GameManagerM1
    }

    public bool HasLost()
    {
        return currentLives <= 0;
    }
}
