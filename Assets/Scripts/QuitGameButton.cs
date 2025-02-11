using UnityEngine;
using UnityEngine.SceneManagement; // Si estás usando escenas.

public class QuitGameButton : MonoBehaviour
{
    public void QuitGame()
    {
#if UNITY_EDITOR
        // Si estás en el editor, el juego se detiene.
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // Si no estás en el editor, se cierra la aplicación.
            Application.Quit();
#endif
    }
}
