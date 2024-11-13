using UnityEngine;

public class CircularPulsingEffect : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject spherePrefab;
    public int numberOfSpheres = 20;
    public float radius = 5f;
    public float maxScale = 2f;

    private GameObject[] spheres;
    private float[] audioSpectrum;

    void Start()
    {
        spheres = new GameObject[numberOfSpheres];
        audioSpectrum = new float[numberOfSpheres];

        // Crear esferas dispuestas en círculo
        for (int i = 0; i < numberOfSpheres; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfSpheres;
            Vector3 position = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            GameObject sphere = Instantiate(spherePrefab, position, Quaternion.identity);
            sphere.transform.parent = transform;
            spheres[i] = sphere;
        }
    }

    void Update()
    {
        // Obtener datos de audio
        audioSource.GetSpectrumData(audioSpectrum, 0, FFTWindow.BlackmanHarris);

        // Ajustar la escala de las esferas según el audio
        for (int i = 0; i < numberOfSpheres; i++)
        {
            float scale = audioSpectrum[i] * maxScale;
            spheres[i].transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
