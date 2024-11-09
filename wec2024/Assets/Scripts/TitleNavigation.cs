using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleNavigation: MonoBehaviour
{
    // Method to be called when the Play button is pressed
    public void PlayGame()
    {
        // Load the sample screen scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Method to be called when the Quit button is pressed
    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
        Debug.Log("Player Quit");

        #if UNITY_EDITOR
        // If the application is running in the Unity Editor, stop the play mode
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}

