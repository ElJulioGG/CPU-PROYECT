using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class colEnemigo : MonoBehaviour
{
    [SerializeField] private float delayBeforeLoading = 2f; // Delay in seconds before loading the scene
    [SerializeField] private GameObject transition1;
    [SerializeField] private GameObject transition2;
    [SerializeField] private GameObject transition3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.playerCanMove = false;
            AudioManager.instance.musicSource.Stop();
            AudioManager.instance.PlaySfx("Encounter");
            // Choose a random transition and activate it
            ActivateRandomTransition();

            StartCoroutine(LoadSceneAfterDelay("M2", delayBeforeLoading));
        }
    }

    private void ActivateRandomTransition()
    {
        // Deactivate all transitions first
        transition1.SetActive(false);
        transition2.SetActive(false);
        transition3.SetActive(false);

        // Pick a random number between 0 and 2
        int randomIndex = Random.Range(0, 3);

        // Activate the chosen transition
        switch (randomIndex)
        {
            case 0:
                transition1.SetActive(true);
                break;
            case 1:
                transition2.SetActive(true);
                break;
            case 2:
                transition3.SetActive(true);
                break;
        }
    }

    private IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        SceneManager.LoadSceneAsync(sceneName);
        gameObject.SetActive(false);
    }
}
