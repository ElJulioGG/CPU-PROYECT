using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour
{
    [SerializeField] float duration;
    private RectTransform rectTransform;
    [SerializeField] private Ease ease;
    [SerializeField] private Vector3 newScale;

    private Tweener tween;
    private void Start()
    {
       gameObject.SetActive(true); 
    }

    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        tween = rectTransform.DOScale(newScale, duration).SetLoops(-1, LoopType.Yoyo).SetEase(ease);

    }
    private void OnDisable()
    {
        tween.Kill();
        rectTransform.localScale = Vector3.one;
    }
}
