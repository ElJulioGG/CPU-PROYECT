using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public RhythmManager rhythmManager;
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    public Renderer playerRenderer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float currentTime = rhythmManager.audioSource.time;
            if (rhythmManager.CheckBeat(currentTime))
            {
                playerRenderer.material.color = correctColor;
            }
            else
            {
                playerRenderer.material.color = incorrectColor;
                rhythmManager.LoseLife();
            }
        }
    }
}
