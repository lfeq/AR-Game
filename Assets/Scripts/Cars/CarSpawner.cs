using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {
    [SerializeField] private GameObject[] carPrefabs;
    [SerializeField] private Vector3 spawnRotation;

    public GameObject SpawnCar() {
        GameObject randomCar = carPrefabs[Random.Range(0, carPrefabs.Length)];
        GameObject tempcar = Instantiate(randomCar, transform.position, Quaternion.Euler(spawnRotation));
        return tempcar;
    }
}