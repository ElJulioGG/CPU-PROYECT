using UnityEngine;

public class LightPulseEffect : MonoBehaviour
{
    [SerializeField] private Light targetLight; // Referencia a la luz que queremos afectar
    [SerializeField] private float pulseIntensity = 2f; // Cuánto aumentar la intensidad cuando se activa el beat
    [SerializeField] private float returnSpeed = 5f; // Velocidad de retorno a la intensidad original
    [SerializeField] private Color pulseColor = Color.red; // Color al que cambiará la luz durante el beat
    [SerializeField] private float colorReturnSpeed = 2f; // Velocidad para volver al color original

    private float originalIntensity;
    private Color originalColor;

    private void Start()
    {
        // Guardamos los valores originales de la luz
        if (targetLight == null)
        {
            targetLight = GetComponent<Light>();
        }

        originalIntensity = targetLight.intensity;
        originalColor = targetLight.color;
    }

    private void Update()
    {
        // Gradualmente volver a la intensidad y color originales
        targetLight.intensity = Mathf.Lerp(targetLight.intensity, originalIntensity, Time.deltaTime * returnSpeed);
        targetLight.color = Color.Lerp(targetLight.color, originalColor, Time.deltaTime * colorReturnSpeed);
    }

    // Método para ser llamado por el BeatManager
    public void Pulse()
    {
        targetLight.intensity = originalIntensity * pulseIntensity;
        targetLight.color = pulseColor;
    }
}
