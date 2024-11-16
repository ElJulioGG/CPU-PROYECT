using System.Collections;
using UnityEngine;
using DG.Tweening;  // Necesario para DOTween
using UnityEngine.UI;  // Necesario para trabajar con el bot�n

public class Dootwen : MonoBehaviour
{
    public float duration = 1f;  // Duraci�n de la animaci�n
    public Vector3 punchScale = new Vector3(0.2f, 0.2f, 0.2f);  // Escala para efecto de punch
    private Vector3 originalScale;  // Escala original del globo
    private CanvasGroup canvasGroup;  // Para manejar la opacidad

    public Button myButton;  // Primer bot�n para activar la animaci�n
    //public Button myButton2;  // Segundo bot�n que aparecer� despu�s de 2 segundos
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

        // Hacemos la animaci�n de aparecer
        AnimateAppear();

        // Asignar eventos a los botones
        if (myButton != null)
        {
            myButton.onClick.AddListener(OnButtonPressed);  // Asigna la funci�n al evento del primer bot�n
        }
        else
        {
            Debug.LogError("El primer bot�n no ha sido asignado.");
        }

        // Hacer aparecer el segundo bot�n despu�s de 2 segundos
     

    }

    // Funci�n para animar el objeto con fade-in y punch scale
    void AnimateAppear()
    {
        canvasGroup.DOFade(1f, duration).SetEase(Ease.OutQuad);  // Animaci�n de opacidad (fade-in)
        transform.DOPunchScale(punchScale, duration, 10, 1).SetEase(Ease.OutCirc);  // Efecto de punch scale
    }

    // Funci�n para animar el objeto con fade-out y desactivar el extraObject
    void AnimateDisappear()
    {
        // Animaci�n de opacidad (fade-out)
        canvasGroup.DOFade(0f, duration).SetEase(Ease.InQuad).OnComplete(() =>
        {
            // Desactivamos el GameObject principal despu�s de la animaci�n
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

    // Este m�todo debe ser llamado desde el primer bot�n para hacer la animaci�n inversa
    void OnButtonPressed()
    {
        AnimateDisappear();
    }

    // Coroutine para mostrar el segundo bot�n despu�s de un retraso
    IEnumerator ShowSecondButtonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Espera el tiempo especificado
    }
}
