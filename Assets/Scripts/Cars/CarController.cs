using UnityEngine;

public class CarController : MonoBehaviour {
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float currentSpeed = 10;
    [SerializeField] private float brakeDistance = 5f;
    [SerializeField] private float stopDistance = 5f;
    [SerializeField] private float stopSpeed = 3.5f;
    [SerializeField] private Transform targetCar;
    [SerializeField] private Transform stopPoint;

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
        float distanceToTarget = Vector3.Distance(transform.position, targetCar.position);
        if (distanceToTarget <= stopDistance) {
            currentSpeed = 0f;
        } else if (distanceToTarget <= brakeDistance) {
            currentSpeed -= stopSpeed * Time.deltaTime;
        } else {
            currentSpeed += 2f * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    private void Move() {
        currentSpeed += 2f * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);
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