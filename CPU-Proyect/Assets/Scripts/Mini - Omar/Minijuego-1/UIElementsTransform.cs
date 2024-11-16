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

    public float animationDuration = 0.6f; // Duración de la animación

    // Start is called before the first frame update
    void Start()
    {
        // Guardar la escala original de los elementos
        Vector3 originalScaleButton = Button.localScale;
        Vector3 originalScaleButton2 = Button2.localScale;
        Vector3 originalScaleImagen = Imagen.localScale;
        Vector3 originalScaleImagen2 = Imagen2.localScale;

        // Establecer escala inicial pequeña
        Button.localScale = Vector3.zero;
        Button2.localScale = Vector3.zero;
        Imagen.localScale = Vector3.zero;
        Imagen2.localScale = Vector3.zero;

        // Animar escala desde pequeño hasta la escala original
        Button.DOScale(originalScaleButton, animationDuration).SetEase(Ease.OutBounce);
        Button2.DOScale(originalScaleButton2, animationDuration).SetEase(Ease.OutBounce);
        Imagen.DOScale(originalScaleImagen, animationDuration).SetEase(Ease.OutBounce);
        Imagen2.DOScale(originalScaleImagen2, animationDuration).SetEase(Ease.OutBounce);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
