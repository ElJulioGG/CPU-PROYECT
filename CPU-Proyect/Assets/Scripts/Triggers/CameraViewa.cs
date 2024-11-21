using UnityEngine;

public class CameraView : MonoBehaviour
{
    private Cinemachine.CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        impulseSource = GetComponent<Cinemachine.CinemachineImpulseSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlaySfx("Alarm");
            GameManager.instance.securityLevel += 40;
            transform.position = Vector3.one *1000f;

            // Trigger the impulse
            impulseSource.GenerateImpulse();
        }
    }
}
