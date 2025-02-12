using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
public enum GameState { Start, Gameplay, Ending }
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }
    public TimeDisplay timeDisplayScript;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ChangeState(GameState.Start);
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log("Estado actual: " + CurrentState);
        if (timeDisplayScript != null)
        {
            timeDisplayScript.ResetTime();  
        }
        switch (newState)
        {
            case GameState.Start:
                SceneManager.LoadScene("Title");
                break;
            case GameState.Gameplay:
                SceneManager.LoadScene("Gameplay");
                break;
            case GameState.Ending:
                SceneManager.LoadScene("Ending");
                break;
        }
    }

    public void StartGame() { ChangeState(GameState.Gameplay); }

    public void GoToMainMenu() { ChangeState(GameState.Start); }

    public void EndGame() { ChangeState(GameState.Ending); }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(); // close all
        }

        if (Input.GetKeyDown(KeyCode.Return)) //start
        {
            ChangeState(GameState.Gameplay);
        }
    }
}
