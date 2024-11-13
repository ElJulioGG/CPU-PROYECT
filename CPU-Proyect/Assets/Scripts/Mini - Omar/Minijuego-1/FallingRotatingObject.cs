using UnityEngine;

public class FallingRotatingObject : MonoBehaviour
{
    private Transform objectTransform; // Transform privado del objeto
    private Vector3 initialPosition; // Posici�n inicial del objeto
    private Rigidbody rb; // Rigidbody del objeto
    public float resetHeight = -10f; // Altura a la que el objeto se restablece a su posici�n inicial
    public ParticleSystem particles; // Sistema de part�culas que se activa antes del reset

    void Start()
    {
        // Obtener el Transform y el Rigidbody del objeto
        objectTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        // Guardar la posici�n inicial
        initialPosition = objectTransform.position;

        // Aplicar una rotaci�n aleatoria inicial
        ApplyRandomRotation();
    }

    void Update()
    {
        // Revisar si el objeto est� por debajo de la altura de reinicio y restablecer si es necesario
        if (objectTransform.position.y < resetHeight)
        {
            ResetObject();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Cuando el objeto colisiona con cualquier superficie, restablece su posici�n
        ResetObject();
    }

    void ApplyRandomRotation()
    {
        // Aplicar rotaci�n aleatoria en los ejes X, Y y Z
        rb.angularVelocity = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ) * 10f;
    }

    void ResetObject()
    {
        // Activar part�culas si est�n configuradas
        if (particles != null)
        {
            particles.transform.position = objectTransform.position; // Colocar las part�culas en la posici�n del objeto
            particles.Play();
        }

        // Restablecer posici�n y rotaci�n, y aplicar nueva rotaci�n aleatoria
        objectTransform.position = initialPosition;
        objectTransform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        ApplyRandomRotation();
    }
}
