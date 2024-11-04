using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class HorizontalObjectMover : MonoBehaviour
{
    public float duration = 0.18f;
    public float distance = 500f;
    public List<RectTransform> elementsToMove;

    public void MoveLeft()
    {
        StartCoroutine(MoveElementsSequentially(-distance));
    }

    public void MoveRight()
    {
        StartCoroutine(MoveElementsSequentially(distance));
    }

    private IEnumerator MoveElementsSequentially(float moveDistance)
    {
        foreach (RectTransform element in elementsToMove)
        {
            // Mueve el elemento en la dirección especificada y espera a que termine
            element.DOAnchorPosX(element.anchoredPosition.x + moveDistance, duration);
            yield return new WaitForSeconds(duration);
        }
    }

    public void MoveElements(float moveDistance)
    {
        StartCoroutine(MoveElementsSequentially(moveDistance));
    }

   
}
