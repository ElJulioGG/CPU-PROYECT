using UnityEngine;
using System.Collections.Generic;

public class MusicReactiveParticles : MonoBehaviour
{
    public AudioSource audioSource;                      // Fuente de audio con la m�sica
    public List<ParticleSystem> particleSystems;         // Lista de sistemas de part�culas
    public float sensitivity = 100f;                      // Sensibilidad de la reacci�n
    public int band = 1;                                  // El rango de frecuencia que queremos usar (0-512)
    public float maxSpeed = 5f;                           // Velocidad m�xima de las part�culas
    private List<ParticleSystem.MainModule> mainModules;  // Lista para almacenar los m�dulos principales

    private float[] spectrum = new float[512];            // Almacenar� el espectro de frecuencias

    void Start()
    {
        // Inicializamos la lista de m�dulos principales
        mainModules = new List<ParticleSystem.MainModule>();

        // Asegurarnos de que hay part�culas y obtener sus m�dulos principales
        foreach (var ps in particleSystems)
        {
            if (ps != null)
            {
                mainModules.Add(ps.main); // A�adir el m�dulo principal de cada sistema de part�culas
                ps.Stop(); // Detener la emisi�n al inicio
            }
        }
    }

    void Update()
    {
        if (!GameManagerM1.instance.startPlaying) return; // Solo actualiza si el juego est� en marcha

        // Obtener el espectro de frecuencias
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);

        // Usar la frecuencia del rango seleccionado para ajustar la velocidad de las part�culas
        float intensity = spectrum[band] * sensitivity;
        intensity = Mathf.Clamp(intensity, 0, maxSpeed); // Limitar el valor para que no sea demasiado alto

        // Ajustar la velocidad de las part�culas en la lista
        foreach (var ps in particleSystems)
        {
            if (ps != null)
            {
                var mainModule = ps.main; // Obtener el m�dulo principal del sistema de part�culas
                mainModule.startSpeed = intensity; // Ajustar la velocidad
            }
        }
    }

    public void StartParticles() // M�todo para iniciar las part�culas
    {
        foreach (var ps in particleSystems)
        {
            if (ps != null)
            {
                ps.Play(); // Iniciar la emisi�n de part�culas
            }
        }
    }
}
