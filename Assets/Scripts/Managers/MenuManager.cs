using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    public static MenuManager instance;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject startPanel;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    // Start is called before the first frame update
    private void Start() {
        GameManager.instance.changeGameState(GameState.MainMenu);
    }

    public void StartPlay() {
        GameManager.instance.changeGameState(GameState.Playing);
    }

    public void ShowEndGamePanel() {
        gameOverPanel.SetActive(true);
    }

    public void RestartLevel() {
        GameManager.instance.changeGameState(GameState.RestartLevel);
        startPanel.SetActive(true);
    }
}