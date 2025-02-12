using UnityEngine;
using UnityEngine.SceneManagement; // If you are using scenes.

public class QuitGameButton : MonoBehaviour
{
    public void QuitGame()
    {
#if UNITY_EDITOR
        // If you are in the editor, the game stops.
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // If you are not in the editor, the application closes.
            Application.Quit();
#endif
    }
}
