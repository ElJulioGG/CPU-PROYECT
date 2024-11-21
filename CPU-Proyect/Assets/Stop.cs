using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Stop : MonoBehaviour
{
    [SerializeField] CubeMovement cubeMovement;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if(cubeMovement.GameOver == true)
        {
            gameObject.SetActive(false);
        }
    }
}
