using UnityEngine;
using UnityEngine.SceneManagement;  // Necesario para manejar escenas

public class SceneController : MonoBehaviour
{
    // M�todo para salir del juego
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game has been quit.");
    }

    // M�todo para reiniciar el escenario actual
    public void RestartScene()
    {
        // Obtiene el nombre de la escena actual y la recarga
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        Debug.Log("Scene restarted: " + currentSceneName);
    }

    // M�todo para cargar una nueva escena
    public void LoadNewScene(string sceneName)
    {
        // Carga la escena por nombre
        SceneManager.LoadScene(sceneName);
        Debug.Log("New scene loaded: " + sceneName);
    }
}
