using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Intensidad y duraci�n del temblor
    public float shakeDuration = 0.5f;
    public float shakeIntensity = 0.5f;
    public float decreaseFactor = 1.0f;

    private float currentShakeDuration = 0f;
    private Vector3 originalPosition; // Para almacenar la posici�n original de la c�mara (x y)

    void Start()
    {
        // Almacenar la posici�n original de la c�mara al iniciar
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (currentShakeDuration > 0)
        {
            // Movimiento aleatorio basado en la intensidad
            Vector3 randomPosition = originalPosition + Random.insideUnitSphere * shakeIntensity;

            // Actualizar la posici�n de la c�mara mientras se mueve
            transform.localPosition = new Vector3(randomPosition.x, randomPosition.y, transform.localPosition.z);

            // Reducir el tiempo del temblor
            currentShakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            // Asegurarse de que la c�mara vuelva a su posici�n original en x e y, manteniendo z constante
            transform.localPosition = new Vector3(originalPosition.x, originalPosition.y, transform.localPosition.z);
        }
    }

    // M�todo p�blico para activar el temblor desde otro script
    public void TriggerShake(float duration)
    {
        currentShakeDuration = duration;
    }
}
