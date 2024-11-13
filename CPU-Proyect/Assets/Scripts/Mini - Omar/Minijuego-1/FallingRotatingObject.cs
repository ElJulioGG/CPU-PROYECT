using UnityEngine;

public class FallingRotatingObject : MonoBehaviour
{
    private Transform objectTransform; // Transform privado del objeto
    private Vector3 initialPosition; // Posición inicial del objeto
    private Rigidbody rb; // Rigidbody del objeto
    public float resetHeight = -10f; // Altura a la que el objeto se restablece a su posición inicial
    public ParticleSystem particles; // Sistema de partículas que se activa antes del reset

    void Start()
    {
        // Obtener el Transform y el Rigidbody del objeto
        objectTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        // Guardar la posición inicial
        initialPosition = objectTransform.position;

        // Aplicar una rotación aleatoria inicial
        ApplyRandomRotation();
    }

    void Update()
    {
        // Revisar si el objeto está por debajo de la altura de reinicio y restablecer si es necesario
        if (objectTransform.position.y < resetHeight)
        {
            ResetObject();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Cuando el objeto colisiona con cualquier superficie, restablece su posición
        ResetObject();
    }

    void ApplyRandomRotation()
    {
        // Aplicar rotación aleatoria en los ejes X, Y y Z
        rb.angularVelocity = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ) * 10f;
    }

    void ResetObject()
    {
        // Activar partículas si están configuradas
        if (particles != null)
        {
            particles.transform.position = objectTransform.position; // Colocar las partículas en la posición del objeto
            particles.Play();
        }

        // Restablecer posición y rotación, y aplicar nueva rotación aleatoria
        objectTransform.position = initialPosition;
        objectTransform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        ApplyRandomRotation();
    }
}
