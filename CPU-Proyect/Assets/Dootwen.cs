using System.Collections;
using UnityEngine;
using DG.Tweening;  // Necesario para DOTween
using UnityEngine.UI;  // Necesario para trabajar con el botón

public class Dootwen : MonoBehaviour
{
    public float duration = 1f;  // Duración de la animación
    public Vector3 punchScale = new Vector3(0.2f, 0.2f, 0.2f);  // Escala para efecto de punch
    private Vector3 originalScale;  // Escala original del globo
    private CanvasGroup canvasGroup;  // Para manejar la opacidad

    public Button myButton;  // Primer botón para activar la animación
    //public Button myButton2;  // Segundo botón que aparecerá después de 2 segundos
    public GameObject extraObject;  // Objeto adicional para desactivar

    // Start is called before the first frame update
    void Start()
    {
        // Guardamos la escala original del objeto
        originalScale = transform.localScale;

        // Agregamos un CanvasGroup al objeto si no tiene uno
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // Configuramos la opacidad inicial
        canvasGroup.alpha = 0f;

        // Hacemos la animación de aparecer
        AnimateAppear();

        // Asignar eventos a los botones
        if (myButton != null)
        {
            myButton.onClick.AddListener(OnButtonPressed);  // Asigna la función al evento del primer botón
        }
        else
        {
            Debug.LogError("El primer botón no ha sido asignado.");
        }

        // Hacer aparecer el segundo botón después de 2 segundos
     

    }

    // Función para animar el objeto con fade-in y punch scale
    void AnimateAppear()
    {
        canvasGroup.DOFade(1f, duration).SetEase(Ease.OutQuad);  // Animación de opacidad (fade-in)
        transform.DOPunchScale(punchScale, duration, 10, 1).SetEase(Ease.OutCirc);  // Efecto de punch scale
    }

    // Función para animar el objeto con fade-out y desactivar el extraObject
    void AnimateDisappear()
    {
        // Animación de opacidad (fade-out)
        canvasGroup.DOFade(0f, duration).SetEase(Ease.InQuad).OnComplete(() =>
        {
            // Desactivamos el GameObject principal después de la animación
            gameObject.SetActive(false);

            // Desactivamos el objeto adicional
            if (extraObject != null)
            {
                extraObject.SetActive(false);
            }
        });

        // Regresa a la escala original
        transform.DOScale(originalScale, duration).SetEase(Ease.InCirc);
    }

    // Este método debe ser llamado desde el primer botón para hacer la animación inversa
    void OnButtonPressed()
    {
        AnimateDisappear();
    }

    // Coroutine para mostrar el segundo botón después de un retraso
    IEnumerator ShowSecondButtonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Espera el tiempo especificado
    }
}
