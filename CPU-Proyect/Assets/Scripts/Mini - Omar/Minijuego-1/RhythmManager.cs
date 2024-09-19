using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RhythmManager : MonoBehaviour
{
    public AudioSource audioSource;
    public Text instructionText;
    public Text timeText;
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    public int totalLives = 3;

    public float[] instructionTimes; // Asigna estos tiempos desde Unity
    public KeyCode[] keysToPress; // Asigna las teclas correspondientes desde Unity

    private string[] instructions = { "espera", "presiona!!" };
    private int currentInstructionIndex = 0;
    public float timeThreshold;
    private bool isPlayerActive = true;

    void Start()
    {
        audioSource.Play();
        StartCoroutine(ShowInstructionsRoutine());
    }

    void Update()
    {
        if (!isPlayerActive) return;

        float currentTime = audioSource.time;

        // Actualizar el texto de tiempo
        timeText.text = $"Tiempo: {currentTime:F2}";

        // Comprobar si el jugador ha presionado la tecla correcta en el momento adecuado
        if (currentInstructionIndex < instructionTimes.Length)
        {
            if (Input.GetKeyDown(keysToPress[currentInstructionIndex]))
            {
                if (CheckBeat(currentTime))
                {
                    instructionText.color = correctColor;
                }
                else
                {
                    instructionText.color = incorrectColor;
                    LoseLife();
                }
            }
        }
    }

    public bool CheckBeat(float currentTime)
    {
        if (currentInstructionIndex < instructionTimes.Length)
        {
            return Mathf.Abs(currentTime - instructionTimes[currentInstructionIndex]) < timeThreshold;
        }
        return false;
    }

    IEnumerator ShowInstructionsRoutine()
    {
        while (isPlayerActive)
        {
            if (currentInstructionIndex < instructions.Length)
            {
                instructionText.text = instructions[currentInstructionIndex];
                yield return new WaitForSeconds(instructionTimes[currentInstructionIndex] - audioSource.time);
                currentInstructionIndex++;
            }
            else
            {
                yield break; // Finaliza la Coroutine si no hay más instrucciones
            }
        }
    }

    public void LoseLife()
    {
        if (totalLives <= 0)
        {
            isPlayerActive = false;
            instructionText.text = "Perdiste";
            StartCoroutine(RestartSceneAfterDelay(2f));
        }
        else
        {
            instructionText.text = $"Te quedan {totalLives} vidas";
        }
        totalLives--;
    }

    private IEnumerator RestartSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Aquí puedes reiniciar la escena
    }

    public bool HasLost()
    {
        return totalLives <= 0;
    }
}
