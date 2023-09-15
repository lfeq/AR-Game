using UnityEngine;

public class CarSpawner : MonoBehaviour {
    [SerializeField] private Vector3 spawnRotation;

    private GameObject[] carPrefabs;

    private void Start() {
        carPrefabs = LevelManager.instance.GetCarPrefabs();
    }

    public GameObject SpawnCar() {
        GameObject randomCar = carPrefabs[Random.Range(0, carPrefabs.Length)];
        GameObject tempcar = Instantiate(randomCar, transform.position, Quaternion.Euler(spawnRotation));
        return tempcar;
    }
}