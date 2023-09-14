using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaneManager : MonoBehaviour {
    [SerializeField] private Transform stopPosition;
    [SerializeField] private CarSpawner carSpawner;
    [SerializeField] private Image stressIcon;
    [SerializeField] private int maxCarsInLane = 5;

    private CustomStack<GameObject> m_carsInLane = new CustomStack<GameObject>();

    private bool m_isRedLight;

    private void Start() {
        m_isRedLight = true;
        LevelManager.instance.SetLaneReference(this);
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
        LevelManager.instance.AddScore();
        m_carsInLane.PopBack();
        GameObject tempCar = m_carsInLane.PeekBack();
        CarController tempCarController = tempCar.GetComponent<CarController>();
        tempCarController.SetTargetCar(stopPosition);
        UpdateStressUI();
    }

    public void SpawnCar() {
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
        UpdateStressUI();
    }

    private void UpdateStressUI() {
        if (m_carsInLane.Count() == maxCarsInLane) {
            LevelManager.instance.EndGame();
        }
        print((float)m_carsInLane.Count() / (float)maxCarsInLane);
        stressIcon.fillAmount = (float)m_carsInLane.Count() / (float)maxCarsInLane;
    }
}