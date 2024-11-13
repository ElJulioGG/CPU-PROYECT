using UnityEngine;

public class ColorExplosionEffect : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject particlePrefab;
    public int numberOfParticles = 100;  // Ajusta la cantidad de part�culas seg�n tu necesidad
    public float radius = 10f;
    public float maxForce = 10f;

    private GameObject[] particles;
    private float[] audioSpectrum;

    void Start()
    {
        particles = new GameObject[numberOfParticles];
        // Cambiar el tama�o del buffer a una potencia de 2 v�lida, como 256
        audioSpectrum = new float[256];  // 256 es una potencia de dos v�lida

        // Crear part�culas dispersas alrededor del centro
        for (int i = 0; i < numberOfParticles; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfParticles;
            Vector3 position = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            GameObject particle = Instantiate(particlePrefab, position, Quaternion.identity);
            particle.transform.parent = transform;
            particles[i] = particle;
        }
    }

    void Update()
    {
        // Obtener datos de audio (aseg�rate de que el tama�o del array sea correcto)
        audioSource.GetSpectrumData(audioSpectrum, 0, FFTWindow.BlackmanHarris);

        // Expulsar part�culas seg�n el espectro de audio
        for (int i = 0; i < numberOfParticles; i++)
        {
            float intensity = audioSpectrum[i % audioSpectrum.Length] * maxForce;
            Vector3 direction = (particles[i].transform.position - transform.position).normalized;
            particles[i].transform.position += direction * intensity * Time.deltaTime;
            particles[i].GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value); // Color aleatorio
        }
    }
}
