using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public RhythmManager rhythmManager;
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    public Renderer playerRenderer;
    public Text feedbackText; // Añadir referencia al texto para mostrar el mensaje de error

    void Update()
    {
        // Verificación de nulos
        if (rhythmManager == null || playerRenderer == null || feedbackText == null)
        {
            return; // Salir si alguna referencia es nula
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Puedes cambiar esto a cualquier tecla que prefieras
        {
            float currentTime = rhythmManager.audioSource.time;
            if (rhythmManager.CheckBeat(currentTime))
            {
                playerRenderer.material.color = correctColor;
                feedbackText.text = ""; // Limpiar el mensaje si acierta
            }
            else
            {
                playerRenderer.material.color = incorrectColor;
                rhythmManager.LoseLife();
                feedbackText.text = "Te equivocaste"; // Mostrar el mensaje de error
            }
        }
    }
}
