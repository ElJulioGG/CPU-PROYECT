using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Import DoTween namespace

public class Move1UIElement : MonoBehaviour
{
    [SerializeField] private RectTransform uiElement; // Reference to the RectTransform
    [SerializeField] private float moveDistance = 80f; // Distance to move (in pixels)
    [SerializeField] private float duration = 2f; // Duration of the animation
    [SerializeField] public float Delay;
    [SerializeField] public float ShakeDuration;
    [SerializeField] public float ShakeStrength;
    [SerializeField] public int ShakeVibrato;
    [SerializeField] private CanvasGroup canvasGroup;
    private void Start()
    {
        if (uiElement == null)
        {
            Debug.LogError("UI Element (RectTransform) is not assigned!");
        }

        MoveUp();
    }


    public void MoveUp()
    {


        if (uiElement != null)
        {
            // Animate the anchored position upwards
            uiElement.DOAnchorPosY(uiElement.anchoredPosition.y + moveDistance, duration)
                     .SetEase(Ease.OutQuad).SetDelay(Delay).OnComplete(() =>
                     {
                         uiElement.DOShakeAnchorPos(ShakeDuration, ShakeStrength, ShakeVibrato)
                                  .SetLoops(-1, LoopType.Yoyo);

                         canvasGroup.DOFade(1, 2.8f);
                     });
        }
    }
}
