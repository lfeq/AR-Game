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
        UpdateStressUI();
    }

    public void SpawnCar() {
        GameObject tempCar = carSpawner.SpawnCar();
        CarController tempCarController = tempCar.GetComponent<CarController>();
        tempCarController.SetLaneManager(this);
        tempCarController.SetTargetCross(stopPosition);
        if (!m_carsInLane.IsEmpty()) {
            GameObject carInFront = m_carsInLane.Peek();
            tempCarController.SetTargetCar(carInFront.GetComponent<CarController>().GetStopPoint());
        } else {
            tempCarController.SetTargetCar(stopPosition);
        }
        tempCarController.SetTrafficLightStatus(m_isRedLight);
        m_carsInLane.PushFront(tempCar);
        UpdateStressUI();
    }

    private void UpdateStressUI() {
        if (m_carsInLane.Count() == maxCarsInLane) {
            LevelManager.instance.EndGame();
        }
        stressIcon.fillAmount = (float)m_carsInLane.Count() / (float)maxCarsInLane;
    }
}