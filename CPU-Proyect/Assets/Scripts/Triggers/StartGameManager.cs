using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySfxLoop1("AmbientCity");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
