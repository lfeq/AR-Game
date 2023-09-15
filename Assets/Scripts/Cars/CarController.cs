using System;
using UnityEngine;

public class CarController : MonoBehaviour {
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float currentSpeed = 10;
    [SerializeField] private float brakeDistance = 5f;
    [SerializeField] private float stopDistance = 5f;
    [SerializeField] private float stopSpeed = 3.5f;
    [SerializeField] private Transform targetCar; // Car infront of this car.
    [SerializeField] private Transform targetCross; // Cross.
    [SerializeField] private Transform stopPoint; // Point so other cars can calculate the distance to stop from this point.

    private bool m_atRedLight;
    private LaneManager m_laneManager;

    private void Update() {
        if (m_atRedLight) {
            MoveAndStop();
        } else {
            Move();
        }
    }

    public void SetTargetCar(Transform t_target) {
        targetCar = t_target;
    }

    public void SetTargetCross(Transform t_target) {
        targetCross = t_target;
    }

    public Transform GetStopPoint() {
        return stopPoint;
    }

    public void SetTrafficLightStatus(bool t_status) {
        m_atRedLight = t_status;
    }

    public void SetLaneManager(LaneManager t_laneManager) {
        m_laneManager = t_laneManager;
    }

    private void MoveAndStop() {
        float distanceToCarInfront = Vector3.Distance(transform.position, targetCar.position);
        float distanceToCross = Vector3.Distance(transform.position, targetCross.position);
        if (distanceToCarInfront <= stopDistance || distanceToCross <= stopDistance) {
            currentSpeed = 0f;
        } else if (distanceToCarInfront <= brakeDistance || distanceToCross <= stopDistance) {
            currentSpeed -= stopSpeed * Time.deltaTime;
        } else {
            currentSpeed += 2f * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    private void Move() {
        if (targetCar.CompareTag("Cross")) {
            currentSpeed += 2f * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 1f, maxSpeed);
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
            return;
        }
        float distanceToCarInfront = Vector3.Distance(transform.position, targetCar.position);
        if (distanceToCarInfront <= brakeDistance) {
            currentSpeed -= (stopSpeed * 2) * Time.deltaTime;
        }
        currentSpeed += 4f * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 1f, maxSpeed);
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Cross")) {
            m_laneManager.PopLast();
            Destroy(gameObject, 30f);
        } else if (other.gameObject.CompareTag("Car")) {
            GameManager.instance.changeGameState(GameState.GameOver);
        }
    }
}