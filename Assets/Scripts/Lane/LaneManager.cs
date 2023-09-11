using System;
using System.Collections.Generic;
using UnityEngine;

public class LaneManager : MonoBehaviour {
    [SerializeField] private Transform stopPosition;
    [SerializeField] private CarSpawner carSpawner;
    [SerializeField] private float carSpawnTimeInSeconds;

    private CustomStack<GameObject> m_carsInLane = new CustomStack<GameObject>();
    private float m_carSpawnTime;
    private bool m_isRedLight;

    private void Start() {
        m_carSpawnTime = carSpawnTimeInSeconds;
        m_isRedLight = true;
    }

    private void Update() {
        m_carSpawnTime -= Time.deltaTime;
        if (m_carSpawnTime < 0) {
            SpawnCar();
        }
    }

    public void ChangeTrafficLight(bool t_trafficLightStatus) {
        GameObject[] carsInLaneTemp = m_carsInLane.ToArray();
        for (int i = 0; i < carsInLaneTemp.Length; i++) {
            CarController tempCarController = carsInLaneTemp[i].GetComponent<CarController>();
            tempCarController.SetTrafficLightStatus(t_trafficLightStatus);
        }
        m_isRedLight = t_trafficLightStatus;
    }

    public void PopLast() {
        m_carsInLane.PopBack();
        GameObject tempCar = m_carsInLane.PeekBack();
        CarController tempCarController = tempCar.GetComponent<CarController>();
        tempCarController.SetTargetCar(stopPosition);
    }

    private void SpawnCar() {
        GameObject tempCar = carSpawner.SpawnCar();
        CarController tempCarController = tempCar.GetComponent<CarController>();
        tempCarController.SetLaneManager(this);
        if (m_carsInLane.Count() == 0) {
            tempCarController.SetTargetCar(stopPosition);
        } else {
            GameObject carInFront = m_carsInLane.Peek();
            tempCarController.SetTargetCar(carInFront.GetComponent<CarController>().GetStopPoint());
        }
        tempCarController.SetTrafficLightStatus(m_isRedLight);
        m_carsInLane.PushFront(tempCar);
        m_carSpawnTime = carSpawnTimeInSeconds;
    }
}