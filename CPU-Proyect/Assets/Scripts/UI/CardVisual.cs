using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro; // Required for TextMeshPro
using UnityEngine.EventSystems;
using Microsoft.Unity.VisualStudio.Editor;

public class CardVisual : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    [SerializeField] private Transform cardTransform; // Reference to the parent card's transform
    [SerializeField] private float followDuration = 0.5f;
    [SerializeField] private float scaleDuration = 0.5f;
    [SerializeField] private float curveYOffset = 1f; // Offset for the Y-axis when dragging
    [SerializeField] private float scaleUpAmount = 1.2f; // Scale factor when hovering
    [SerializeField] private float scaleUpAmount2 = 1.2f;
  
    private bool hovering;
    

    public ButtonMenu button;

    private TextMeshProUGUI childText; // TextMeshPro reference
    private Vector3 originalScale;
    private Color normalColor = Color.white;
    private Color selectedColor = Color.green;

    private void Start()
    {
        
        rectTransform = GetComponent<RectTransform>();
        originalScale = transform.localScale; // Save the original scale

        // Automatically find the TextMeshProUGUI component in children
        childText = GetComponentInChildren<TextMeshProUGUI>();

        if (childText == null)
        {
            Debug.LogError("TextMeshProUGUI component not found in children.");
        }
    }

    void Update()
    {
        if (button.isDragging)
        {
            // Smoothly move to the target position with offset when dragging
            rectTransform.DOMove(cardTransform.position + Vector3.up * curveYOffset, followDuration);
            

        }
        else
        {
            //transform.SetAsLastSibling();
            if (!hovering)
            {
                rectTransform.DOScale(originalScale, 0.3f).SetEase(Ease.OutBack);
            }
            // Smoothly move back to the target position without offset
            rectTransform.DOMove(cardTransform.position, followDuration);
        }

        if (GameManager.instance.lastSelectedNote == button.menuIndex)
        {
            // Smoothly change text color to green
            childText.DOColor(selectedColor, 0.5f);
        }
        else
        {
            // Smoothly change text color back to white
            childText.DOColor(normalColor, 0.5f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       
        if (!button.isDragging)
        {
            hovering = true;
            // Scale up to 1.2 and apply shake effect on hover
            rectTransform.DOScale(originalScale * scaleUpAmount, 0.3f).SetEase(Ease.OutBack);
            rectTransform.DOShakePosition(0.3f, strength: new Vector3(5, 5, 0), vibrato: 10, randomness: 90);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {     
       
            hovering = false;
        if (!button.isDragging)
        {
            // Scale back down to the original scale on exit
            rectTransform.DOScale(originalScale, 0.3f).SetEase(Ease.OutBack);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        hovering = false;
        // Optional: Scale up and shake on click (you can remove this if not needed)
        rectTransform.DOScale(originalScale * scaleUpAmount2, 0.3f).SetEase(Ease.OutBack);
        //rectTransform.DOShakePosition(shakeDuration, strength: new Vector3(1*shake, 1* shake, 0), vibrato: shake2, randomness: shake3);
    }
}
