using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDisableDialog : MonoBehaviour
{

    public GameObject block;

    private void OnDisable()
    {
        block.SetActive(false);
       // Destroy(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
