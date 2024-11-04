using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class VerticalObjectMover : MonoBehaviour
{
    public float duration = 0.18f;
    public float distance = 500f;
    public List<RectTransform> elementsToMove;

    public void MoveUp()
    {
        StartCoroutine(MoveElementsSequentially(distance));
    }

    public void MoveDown()
    {
        StartCoroutine(MoveElementsSequentially(-distance));
    }

    private IEnumerator MoveElementsSequentially(float moveDistance)
    {
        foreach (RectTransform element in elementsToMove)
        {
            // Mueve el elemento en la dirección especificada y espera a que termine
            element.DOAnchorPosY(element.anchoredPosition.y + moveDistance, duration);
            yield return new WaitForSeconds(duration);
        }
    }
}
