using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager instance;

    [SerializeField] private LaneManager[] lanes;
    [SerializeField] private float carSpawnTimeInSeconds;

    private float m_carSpawnTime;
    private int score;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    private void Start() {
        m_carSpawnTime = carSpawnTimeInSeconds;
    }

    private void Update() {
        if (GameManager.instance.getGameState() != GameState.Playing) {
            return;
        }
        m_carSpawnTime -= Time.deltaTime;
        if (m_carSpawnTime < 0) {
            SpawnCar();
        }
    }

    public void AddScore() {
        score++;
    }

    public void EndGame() {
        GameManager.instance.changeGameState(GameState.GameOver);
    }

    public void SetLaneReference(LaneManager lane) {
        for (int i = 0; i < lanes.Length; i++) {
            if (lanes[i] == null) {
                lanes[i] = lane;
                return;
            }
        }
    }

    private void SpawnCar() {
        LaneManager randomLane = lanes[Random.Range(0, lanes.Length)];
        randomLane.SpawnCar();
        m_carSpawnTime = Random.Range(2, carSpawnTimeInSeconds);
    }
}