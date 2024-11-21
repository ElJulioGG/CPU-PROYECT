using UnityEngine;
using DG.Tweening;

public class Combi : MonoBehaviour
{
    public Transform auto; // The object to move.
    public float duration = 5f; // Duration for the upward movement.
    public float movementDistance = 10f; // Distance to move upwards.
    public float pauseDuration = 10f; // Duration of the pause between loops.
    public AudioSource audioSource; // The AudioSource to play the sound.
    public AudioClip clip; // The sound clip to play.

    private void Start()
    {
        // Save the initial position.
        Vector3 startPosition = auto.position;

        // Calculate the upward end position.
        Vector3 endPosition = startPosition + new Vector3(0f, movementDistance, 0f);

        // Assign the audio clip to the AudioSource.
        audioSource.clip = clip;

        // Start the looping process.
        StartMovementLoop(startPosition, endPosition);
    }

    private void StartMovementLoop(Vector3 startPosition, Vector3 endPosition)
    {
        // Play the sound at the start of the loop.
        PlaySound();

        // Move upwards.
        auto.DOMove(endPosition, duration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                // Instantly reset to the starting position.
                auto.position = startPosition;

                // Pause for the specified duration and restart the loop.
                DOVirtual.DelayedCall(pauseDuration, () =>
                {
                    StartMovementLoop(startPosition, endPosition);
                });
            });
    }

    public void PlaySound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}
