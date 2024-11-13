using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject targetObject; // Objeto a disolver
    public float dissolveSpeed = 0.1f;
    private Material objectMaterial;
    private float dissolveAmount = 1f; // Inicialmente está completamente disuelto

    void Start()
    {
        // Obtener el material del objeto
        objectMaterial = targetObject.GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Obtener datos del espectro de audio
        float[] audioSpectrum = new float[256];
        audioSource.GetSpectrumData(audioSpectrum, 0, FFTWindow.BlackmanHarris);

        // Calcular la cantidad de disolución en función de la amplitud de la música
        dissolveAmount = Mathf.Lerp(dissolveAmount, audioSpectrum[0] * 2f, Time.deltaTime * dissolveSpeed);
        objectMaterial.SetFloat("_DissolveAmount", dissolveAmount);
    }
}
