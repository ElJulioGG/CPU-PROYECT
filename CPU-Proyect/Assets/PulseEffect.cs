using UnityEngine;
using System.Collections;

public class PulseEffect : MonoBehaviour
{
    [SerializeField] private float pulseScale = 1.25f; // How much to scale up when the beat hits
    [SerializeField] private float returnSpeed = 5f; // Speed of returning to original size
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        // Gradually return to the original scale
        transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * returnSpeed);
    }

    // Method to be called by the BeatManager
    public void Pulse()
    {
        transform.localScale = originalScale * pulseScale;
    }
}
