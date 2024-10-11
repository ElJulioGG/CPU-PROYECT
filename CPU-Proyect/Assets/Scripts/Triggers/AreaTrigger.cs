using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AreaTrigger : MonoBehaviour
{
    [SerializeField] private GameObject canvasTransition;
    [SerializeField] private GameObject darkImage;
    [SerializeField] private Vector3 offset;
    private Animator darkImageAnimator;
    [SerializeField] private GameObject destination;
    [SerializeField] private CinemachineVirtualCamera CameraIn;
    private CinemachineVirtualCamera activeCamera; // Reference to the current live camera
    private GameObject player;
    public UnityEvent transitionEndEvent;

    private CinemachineBrain brain; 

    void Start()
    {
        darkImageAnimator = darkImage.GetComponent<Animator>();
        player = GameObject.Find("Player");

        
        brain = Camera.main.GetComponent<CinemachineBrain>();

        if (brain != null)
        {
            activeCamera = brain.ActiveVirtualCamera as CinemachineVirtualCamera;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(RoomTransition());
        }
    }

    private IEnumerator RoomTransition()
    {
        GameManager.instance.playerCanMove = false;

        darkImageAnimator.SetTrigger("FadeIn");
        AudioManager.instance.PlaySfx("RoomOut");

        yield return new WaitForSeconds(0.5f);

       
        player.transform.position = new Vector3(
            destination.transform.position.x + offset.x,
            destination.transform.position.y + offset.y,
            player.transform.position.z
        );

       
        if (brain != null)
        {
            ICinemachineCamera liveCamera = brain.ActiveVirtualCamera;

            if (liveCamera != null && liveCamera != CameraIn)
            {
                activeCamera = liveCamera as CinemachineVirtualCamera;
                if (activeCamera != null)
                {
                    activeCamera.Priority = 0; 
                }
            }
        }

        CameraIn.Priority = 10;

        yield return new WaitForSeconds(0.5f);

        darkImageAnimator.SetTrigger("FadeOut");
        AudioManager.instance.PlaySfx("RoomIn");

        yield return new WaitForSeconds(1f);

        GameManager.instance.playerCanMove = true;
        transitionEndEvent.Invoke();

        // Update the active camera reference to the new live camera
        activeCamera = CameraIn;
    }
}
