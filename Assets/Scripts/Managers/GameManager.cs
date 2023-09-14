using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    private GameState m_gameState;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
        m_gameState = GameState.None;
    }

    public void changeGameState(GameState newGameState) {
        if (m_gameState == newGameState) {
            return;
        }
        m_gameState = newGameState;
        switch (m_gameState) {
            case GameState.None:
                break;
            case GameState.LoadMainMenu:
                loadMainMenu();
                break;
            case GameState.MainMenu:
                break;
            case GameState.Playing:
                break;
            case GameState.GameOver:
                gameOver();
                break;
            case GameState.RestartLevel:
                restartLevel();
                break;
            default:
                throw new UnityException("null Game State");
        }
    }

    public void changeGameStateInEditor(string t_newState) {
        changeGameState((GameState)System.Enum.Parse(typeof(GameState), t_newState));
    }

    public void startGame() {
        changeGameState(GameState.Playing);
    }

    public void gameOver() {
        MenuManager.instance.ShowEndGamePanel();
    }

    public IEnumerator resetLevel() {
        changeGameState(GameState.MainMenu);
        yield return new WaitForSeconds(3);
        restartLevel();
    }

    public void restartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //setPlayerSpawn();
    }

    //private void setPlayerSpawn() {
    //    PlayerManager.instance.transform.position = LevelManager.instance.spawnPoint.transform.position;
    //}
    public GameState getGameState() {
        return m_gameState;
    }

    public void loadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    private void exitGame() {
        Application.Quit();
    }
}

public enum GameState {
    None,
    LoadMainMenu,
    MainMenu,
    LoadLevel,
    Playing,
    GameOver,
    Win,
    QuitGame,
    RestartLevel
}