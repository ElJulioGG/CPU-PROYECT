using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private GameObject Player; // Referencia al componente CubeMovement
    public CubeMovement cubeMovement;
    [SerializeField] private float RotationSpeed = 1f;
  
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        cubeMovement = Player.GetComponent<CubeMovement>();
    }

    void Update()
    {
        // Comprueba si isGrounded es falso en CubeMovement
        if (!cubeMovement.isGrounded)   
        {
            transform.Rotate(0, 0, -RotationSpeed); // Rota si no está en el suelo
        }

        // Detecta cuando el estado cambia de no estar en el suelo a estar en el suelo
        if (cubeMovement.isGrounded)
        {
            AdjustRotationToNearest90(); // Ajusta la rotación al ángulo más cercano
        }

    }

    private void AdjustRotationToNearest90()
    {

        float currentRotationZ = transform.eulerAngles.z;

        float nearest90 = Mathf.Round(currentRotationZ / 90f) * 90f;

        transform.rotation = Quaternion.Euler(0, 0, nearest90);
    }
}

