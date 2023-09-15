using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LevelManager : MonoBehaviour {
    public static LevelManager instance;

    [SerializeField] private LaneManager[] lanes;
    [SerializeField] private float carSpawnTimeInSeconds;
    [SerializeField] private GameObject[] carPrefabsDownloaded;

    private float m_carSpawnTime;
    private int score;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
        StartCoroutine(DownloadModel());
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

    public GameObject[] GetCarPrefabs() {
        return carPrefabsDownloaded;
    }

    private void SpawnCar() {
        LaneManager randomLane = lanes[Random.Range(0, lanes.Length)];
        randomLane.SpawnCar();
        m_carSpawnTime = Random.Range(3f, carSpawnTimeInSeconds);
    }

    private IEnumerator DownloadModel() {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle("http://192.168.68.82:3000/descargar-archivo/cars");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError) {
            Debug.LogError("Error al descargar el Asset Bundle: " + www.error);
        } else {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            if (bundle != null) {
                var prefab = bundle.LoadAllAssets<GameObject>(); //Aqui estan todos los prefabs de los carritos
                carPrefabsDownloaded = new GameObject[prefab.Length];
                for (int i = 0; i < prefab.Length; i++) {
                    carPrefabsDownloaded[i] = prefab[i];
                }
            }
        }
    }
}