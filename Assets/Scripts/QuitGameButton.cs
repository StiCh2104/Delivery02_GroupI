using UnityEngine;
using UnityEngine.SceneManagement; // Si est�s usando escenas.

public class QuitGameButton : MonoBehaviour
{
    public void QuitGame()
    {
#if UNITY_EDITOR
        // Si est�s en el editor, el juego se detiene.
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // Si no est�s en el editor, se cierra la aplicaci�n.
            Application.Quit();
#endif
    }
}
