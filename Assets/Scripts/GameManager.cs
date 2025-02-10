using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Start, Gameplay, Pause, Ending }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject); // Mantener el GameManager al cambiar de escena
    }

    private void Start()
    {
        ChangeState(GameState.Start);
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log("Estado actual: " + CurrentState);

        switch (newState)
        {
            case GameState.Start:
                SceneManager.LoadScene("StartScene");
                break;
            case GameState.Gameplay:
                SceneManager.LoadScene("Gameplay");
                break;
            case GameState.Pause:
                Time.timeScale = 0; // Pausa el juego
                break;
            case GameState.Ending:
                SceneManager.LoadScene("EndingScene");
                break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && CurrentState == GameState.Gameplay)
        {
            ChangeState(GameState.Pause);
        }
        else if (Input.GetKeyDown(KeyCode.P) && CurrentState == GameState.Pause)
        {
            ChangeState(GameState.Gameplay);
            Time.timeScale = 1; // Reanuda el juego
            //añadir: canvas false (invisible) -> true (visible) o algo ...
        }
    }
}
