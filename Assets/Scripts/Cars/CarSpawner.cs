using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {
    [SerializeField] private GameObject[] carPrefabs;

    public GameObject SpawnCar() {
        GameObject randomCar = carPrefabs[Random.Range(0, carPrefabs.Length)];
        GameObject tempcar = Instantiate(randomCar, transform.position, Quaternion.identity);
        return tempcar;
    }
}