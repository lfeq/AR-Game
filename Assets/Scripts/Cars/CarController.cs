using UnityEngine;

public class CarController : MonoBehaviour {
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float currentSpeed = 10;
    [SerializeField] private float brakeDistance = 5f;
    [SerializeField] private float stopDistance = 5f;
    [SerializeField] private float stopSpeed = 3.5f;
    [SerializeField] private Transform targetCar;
    [SerializeField] private Transform stopPoint;

    private float distanceToTarget;

    private void Update() {
        float distanceToTarget = Vector3.Distance(transform.position, targetCar.position);

        if (distanceToTarget <= stopDistance) {
            // Stop the car
            currentSpeed = 0f;
        } else if (distanceToTarget <= brakeDistance) {
            // Apply brakes to slow down gradually
            currentSpeed -= stopSpeed * Time.deltaTime;
        } else {
            // Accelerate if there's no need to brake
            currentSpeed += 2f * Time.deltaTime;
        }

        // Clamp the speed within the maximum speed limit
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);

        // Move the car forward based on the current speed
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    public void SetTargetCar(Transform t_target) {
        targetCar = t_target;
    }

    public Transform GetStopPoint() {
        return stopPoint;
    }
}