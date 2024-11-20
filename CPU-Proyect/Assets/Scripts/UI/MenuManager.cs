using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public List<CinemachineVirtualCamera> cameras; // Assign your cameras in the inspector

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayMusic("MenuTheme");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            changeCamera(0,1); // change cam from 0 to 1
        }
    }

    private void changeCamera(int from, int into)
    {

        // Check the ID of the currently active camera
        CinemachineVirtualCamera activeCamera = GetActiveCamera();
        if (activeCamera != null && activeCamera.Priority == 10) // Assuming Priority 10 means "active"
        {
            int cameraID = cameras.IndexOf(activeCamera);

            // If the active camera's ID is 1, adjust priorities
            if (cameraID == from)
            {
                // Set active camera's priority to 0
                activeCamera.Priority = 0;

                // Set camera 2's priority to 10
                cameras[into].Priority = 10;
            }
        }
    }

    // Function to get the active camera (with highest priority)
    private CinemachineVirtualCamera GetActiveCamera()
    {
        CinemachineVirtualCamera activeCam = null;
        int highestPriority = int.MinValue;

        foreach (var cam in cameras)
        {
            if (cam.Priority > highestPriority)
            {
                highestPriority = cam.Priority;
                activeCam = cam;
            }
        }

        return activeCam;
    }
    public void returnToMenu(int from)
    {
        changeCamera(from, 1);
    }

    public void checkSelection()
    {
        switch (GameManager.instance.lastSelectedNote)
        {
            case 0:

                SceneManager.LoadScene("JulioScene");
                AudioManager.instance.musicSource.Stop();
                break;
            case 1:
                break;
            case 2:
                changeCamera(1, 2);
                break;
            case 3:
                changeCamera(1, 3);
                break;
            case 4:
                changeCamera(1, 4);
                break;
            case 5:
                break;
        }
    }
}

