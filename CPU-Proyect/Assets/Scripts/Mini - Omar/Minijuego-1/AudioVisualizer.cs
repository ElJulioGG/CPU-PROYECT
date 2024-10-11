using UnityEngine;

public class AudioVisualizer : MonoBehaviour
{
    public AudioSource audioSource; // Fuente de audio
    public GameObject cubePrefab; // Prefab del cubo
    public int numberOfCubes = 64; // Número de cubos en la fila
    public float maxScale = 10f; // Escala máxima de los cubos
    public float spacing = 1.5f; // Espaciado entre cubos

    private GameObject[] cubes; // Arreglo de cubos
    private float[] audioSpectrum; // Arreglo para almacenar datos de audio

    public AudioSource startMusic; // Música que suena antes de empezar el juego
    public AudioSource winMusic; // Música que suena al ganar
    public AudioSource loseMusic; // Música que suena al perder

    void Start()
    {
        // Inicializar el array de cubos y espectro
        cubes = new GameObject[numberOfCubes];
        audioSpectrum = new float[numberOfCubes];

        // Crear los cubos
        for (int i = 0; i < numberOfCubes; i++)
        {
            float xPosition = i * spacing - ((numberOfCubes * spacing) / 2f); // Centrar los cubos en X
            Vector3 cubePosition = transform.position + new Vector3(xPosition, 0, 0);

            GameObject cube = Instantiate(cubePrefab, cubePosition, Quaternion.identity);
            cube.transform.parent = transform; // Hacer que los cubos sean hijos del objeto visualizador
            cubes[i] = cube;
        }

        // Reproducir música de inicio al comenzar el juego
        if (startMusic != null)
        {
            startMusic.Play();
        }
    }

    void Update()
    {
        // Obtener datos de audio del AudioSource
        audioSource.GetSpectrumData(audioSpectrum, 0, FFTWindow.BlackmanHarris);

        // Ajustar la escala de los cubos según el espectro de audio
        for (int i = 0; i < numberOfCubes; i++)
        {
            if (cubes[i] != null)
            {
                Vector3 previousScale = cubes[i].transform.localScale;
                previousScale.y = Mathf.Lerp(previousScale.y, audioSpectrum[i] * maxScale, Time.deltaTime * 30);
                cubes[i].transform.localScale = new Vector3(1, previousScale.y, 1);

                // Actualizar la posición del cubo basado en la posición del AudioVisualizer
                float xPosition = i * spacing - ((numberOfCubes * spacing) / 2f);
                cubes[i].transform.position = transform.position + new Vector3(xPosition, 0, 0);
            }
        }
    }

    public void StartGame()
    {
        // Detener la música de inicio y reproducir la música del juego
        if (startMusic != null)
        {
            startMusic.Stop();
        }

        if (audioSource != null)
        {
            audioSource.Play(); // Reproducir música del juego
        }
    }

    public void OnWin()
    {
        // Detener la música del juego y reproducir la música de victoria
        if (audioSource != null)
        {
            audioSource.Stop();
        }
        if (winMusic != null)
        {
            winMusic.Play();
        }
    }

    public void OnLose()
    {
        // Detener la música del juego y reproducir la música de perder
        if (audioSource != null)
        {
            audioSource.Stop();
        }
        if (loseMusic != null)
        {
            loseMusic.Play();
        }
    }
}
