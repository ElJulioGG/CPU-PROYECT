using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class SelectText : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private TextMeshProUGUI tmpText;
    [SerializeField] private Color targetColor = Color.green;
    private RectTransform rectTransform;

    private int previousSelectedNote = -1; // Initialize with a value that won't match any note

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        if (GameManager.instance.lastSelectedNote != previousSelectedNote)
        {
            previousSelectedNote = GameManager.instance.lastSelectedNote;
            tmpText.fontSize = 36;
            switch (GameManager.instance.lastSelectedNote)
            {

                case 0:
                    SetTextWithAnimation("Nueva Partida");
                    tmpText.fontSize = 34;
                    break;
                case 1:
                    SetTextWithAnimation("Continuar");
                    break;
                case 2:
                    SetTextWithAnimation("Configuracion");
                    tmpText.fontSize = 30;
                    break;
                case 3:
                    SetTextWithAnimation("Logros");
                    break;
                case 4:
                    SetTextWithAnimation("Creditos");
                    break;
                case 5:
                    SetTextWithAnimation("Salir");
                    break;
                default:
                    SetTextWithAnimation("-");
                    break;
            }
        }
    }

    private void SetTextWithAnimation(string newText)
    {
        tmpText.text = newText;

        tmpText.transform.localScale = Vector3.one;
        tmpText.color = Color.white;

        tmpText.transform.DOScale(1.2f, 0.2f).SetEase(Ease.InCirc)
            .OnComplete(() => tmpText.transform.DOScale(1f, 0.2f).SetEase(Ease.InCirc));

        tmpText.DOColor(targetColor, 0.2f)
            .OnComplete(() => tmpText.DOColor(Color.green, 0.2f));
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Scale up and apply a shake effect
       //rectTransform.DOScale(1.2f, 0.3f).SetEase(Ease.OutBack);
       // rectTransform.DOShakePosition(0.3f, strength: new Vector3(5, 5, 0), vibrato: 10, randomness: 90);
    }

}
