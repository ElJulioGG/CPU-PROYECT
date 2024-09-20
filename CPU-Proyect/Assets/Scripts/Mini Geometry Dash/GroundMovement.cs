using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    // Velocidad de movimiento
    private float speed = -3.7f;

    // Update is called once per frame
    void Update()
    {

        // Avanzar en el eje X a una velocidad constante
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
    }
}