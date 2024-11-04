using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManagerM1 : MonoBehaviour
{
    public AudioSource audioSource; // AudioSource to play music
    public AudioClip startMusic; // Music before starting the game
    public AudioClip gameplayMusic; // Music during gameplay
    public AudioClip winMusic; // Music when player wins
    public AudioClip loseMusic; // Music when player loses

    public bool startPlaying;
    public BeatScroller theBS;
    public static GameManagerM1 instance;
    public int currentScore;
    public int scorePetNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerfectNote = 150;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThreholds;

    public Text scoreText;
    public Text multiText;
    public Button startButton;  // Referencia al bot�n de inicio
    public Transform startButton2;  // Referencia al bot�n de inicio
    public MusicReactiveParticles musicReactiveParticles; // Referencia al script de part�culas
    public HorizontalObjectMover mover;

    void Start()
    {
        instance = this;
        scoreText.text = "Score: 0";
        currentMultiplier = 1;
        startPlaying = false;

        // Play start music
        audioSource.clip = startMusic;
        audioSource.Play();

        // Desactiva los movimientos y la m�sica hasta que el bot�n sea presionado
        theBS.hasStarted = false;
        audioSource.Stop();
        startButton2.DOMoveX(1300f, 1.4f).SetEase(Ease.OutBounce);
    }

    void Update()
    {
        if (startPlaying)
        {
            // Aqu� el c�digo relacionado con la l�gica del juego que solo ocurre cuando empieza
        }
    
    }

    public void StartGame()  // Este m�todo se ejecuta al presionar el bot�n
    {
        startPlaying = true;
        theBS.hasStarted = true;
        RhythmManager rhythmManager = FindObjectOfType<RhythmManager>();
        if (rhythmManager != null)
        {
            rhythmManager.StartGame();
        }
        // Cambiar a la m�sica de gameplay
        audioSource.clip = gameplayMusic;
        audioSource.Play();

        startButton.gameObject.SetActive(false);

        // Iniciar las part�culas
        musicReactiveParticles.StartParticles();
    }
    public void PlayLoseMusic()
    {
        // C�digo para reproducir la m�sica de perder
    }
    public void NoteHit()
    {
        if (currentMultiplier - 1 < multiplierThreholds.Length)
        {
            multiplierTracker++;
            if (multiplierThreholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        multiText.text = "Multiplier: x" + currentMultiplier;
        scoreText.text = "Score: " + currentScore;
    }

    public void NormalHit()
    {
        currentScore += scorePetNote * currentMultiplier;
        NoteHit();
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
    }

    public void PerfectHit()
    {
        currentScore += scorePerfectNote * currentMultiplier;
        NoteHit();
    }

    public void NoteMissed()
    {
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.text = "Multiplier: x" + currentMultiplier;
    }

    // New methods for win and lose conditions
    public void GameWon()
    {
        audioSource.clip = winMusic; // Set the win music
        audioSource.Play();
        // Additional win logic
    }

    public void GameLost()
    {
        audioSource.clip = loseMusic; // Set the lose music
        audioSource.Play();
        // Additional lose logic
    }

}
