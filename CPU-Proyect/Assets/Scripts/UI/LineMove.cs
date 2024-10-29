using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LineMove : MonoBehaviour
{
    public float distanceX;
    public float maxDuration = 1.0f;
    [Range(0.1f, 1.0f)] public float speed = 1.0f; // Speed range from 0.1 to 1.0
    [SerializeField] private Button button;

    public UnityEvent onCompleteEvent; // Unity Event to be triggered on completion

    private Collider2D col2d;
    private void Start()
    {
        col2d = GetComponent<Collider2D>();
        col2d.enabled = false;
    }

    public void moveLine()
    {
        button.enabled = false;
        Vector3 startPos = transform.position;
        float startX = transform.position.x;
        col2d.enabled = true;

        // Calculate duration based on speed
        float duration = maxDuration / speed;

        transform.DOMoveX(startX + distanceX, duration).SetEase(Ease.Linear).OnComplete(() => {
            transform.position = startPos;
            col2d.enabled = false;
            button.enabled = true;

            // Invoke the onCompleteEvent
            onCompleteEvent?.Invoke();
        });
    }


}
