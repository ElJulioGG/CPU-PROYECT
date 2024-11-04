using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIElementsTransform : MonoBehaviour
{
    public Transform Imagen;
    public Transform Imagen2;
    public Transform Button;
    public Transform Button2;

    // Start is called before the first frame update
    void Start()
    {
        Button.DOMoveX(1300f, 0.6f).SetEase(Ease.OutCirc);
        Button2.DOMoveX(1300f, 0.6f).SetEase(Ease.OutCirc);
        //Imagen.DOMoveX(900f, 0.6f).SetEase(Ease.OutCirc);
        Imagen2.DOMoveY(650f, 0.6f).SetEase(Ease.OutCirc);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
