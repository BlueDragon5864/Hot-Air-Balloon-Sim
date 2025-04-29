using UnityEngine;

public class QuitButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ButtonClicked()
    {
        QuitApplication();
    }

    public void QuitApplication()
    {
#if UNITY_EDITOR
        // If we're in the editor, stop playing the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Otherwise, quit the application
        Application.Quit();
#endif
    }
}
