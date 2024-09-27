using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    [SerializeField] private GameObject canvasTransition;
    [SerializeField] private GameObject darkImage;
    [SerializeField] private Vector3 offset;
    private Animator darkImageAnimator;
    [SerializeField] private GameObject destination;
    [SerializeField] private CinemachineVirtualCamera CameraIn;
    private CinemachineVirtualCamera activeCamera; // Reference to the active camera
    private GameObject player;

    void Start()
    {
        darkImageAnimator = darkImage.GetComponent<Animator>();
        player = GameObject.Find("Player");

        // Assuming there's a tag or way to find the currently active camera
        activeCamera = FindObjectOfType<CinemachineVirtualCamera>(); // Get the current active camera
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
        player.transform.position = new Vector3(destination.transform.position.x + offset.x, destination.transform.position.y+ offset.y, destination.transform.position.z+ offset.z);

        // Set the new camera priority to 10 and the active camera's priority to 0
        if (activeCamera != null)
        {
            activeCamera.Priority = 0;
        }
        CameraIn.Priority = 10;

        yield return new WaitForSeconds(0.5f);

        
        darkImageAnimator.SetTrigger("FadeOut");
        AudioManager.instance.PlaySfx("RoomIn");
        yield return new WaitForSeconds(1f);
       
        GameManager.instance.playerCanMove = true;
        
        
    }
}
