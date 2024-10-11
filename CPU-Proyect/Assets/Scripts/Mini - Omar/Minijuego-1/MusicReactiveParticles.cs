using UnityEngine;
using System.Collections.Generic;

public class MusicReactiveParticles : MonoBehaviour
{
    public AudioSource audioSource;                      // Fuente de audio con la música
    public List<ParticleSystem> particleSystems;         // Lista de sistemas de partículas
    public float sensitivity = 100f;                      // Sensibilidad de la reacción
    public int band = 1;                                  // El rango de frecuencia que queremos usar (0-512)
    public float maxSpeed = 5f;                           // Velocidad máxima de las partículas
    private List<ParticleSystem.MainModule> mainModules;  // Lista para almacenar los módulos principales

    private float[] spectrum = new float[512];            // Almacenará el espectro de frecuencias

    void Start()
    {
        // Inicializamos la lista de módulos principales
        mainModules = new List<ParticleSystem.MainModule>();

        // Asegurarnos de que hay partículas y obtener sus módulos principales
        foreach (var ps in particleSystems)
        {
            if (ps != null)
            {
                mainModules.Add(ps.main); // Añadir el módulo principal de cada sistema de partículas
                ps.Stop(); // Detener la emisión al inicio
            }
        }
    }

    void Update()
    {
        if (!GameManagerM1.instance.startPlaying) return; // Solo actualiza si el juego está en marcha

        // Obtener el espectro de frecuencias
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);

        // Usar la frecuencia del rango seleccionado para ajustar la velocidad de las partículas
        float intensity = spectrum[band] * sensitivity;
        intensity = Mathf.Clamp(intensity, 0, maxSpeed); // Limitar el valor para que no sea demasiado alto

        // Ajustar la velocidad de las partículas en la lista
        foreach (var ps in particleSystems)
        {
            if (ps != null)
            {
                var mainModule = ps.main; // Obtener el módulo principal del sistema de partículas
                mainModule.startSpeed = intensity; // Ajustar la velocidad
            }
        }
    }

    public void StartParticles() // Método para iniciar las partículas
    {
        foreach (var ps in particleSystems)
        {
            if (ps != null)
            {
                ps.Play(); // Iniciar la emisión de partículas
            }
        }
    }
}
