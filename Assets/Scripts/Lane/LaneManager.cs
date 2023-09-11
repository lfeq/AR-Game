using System;
using System.Collections.Generic;
using UnityEngine;

public class LaneManager : MonoBehaviour {
    [SerializeField] private Transform stopPosition;
    [SerializeField] private CarSpawner carSpawner;
    [SerializeField] private float carSpawnTimeInSeconds;

    private CustomStack<GameObject> m_carsInLane = new CustomStack<GameObject>();
    private float m_carSpawnTime;

    private void Start() {
        m_carSpawnTime = carSpawnTimeInSeconds;
    }

    private void Update() {
        m_carSpawnTime -= Time.deltaTime;
        if (m_carSpawnTime < 0) {
            SpawnCar();
        }
    }

    private void SpawnCar() {
        GameObject tempCar = carSpawner.SpawnCar();
        if (m_carsInLane.Count() == 0) {
            tempCar.GetComponent<CarController>().SetTargetCar(stopPosition);
        } else {
            GameObject carInFront = m_carsInLane.Peek();
            tempCar.GetComponent<CarController>().SetTargetCar(carInFront.GetComponent<CarController>().GetStopPoint());
        }
        m_carsInLane.PushFront(tempCar);
        m_carSpawnTime = carSpawnTimeInSeconds;
    }
}