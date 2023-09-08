using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {
    public float maxSpeed = 20f;
    public float currentSpeed;
    public float brakeDistance = 5f;
    public Transform targetCar;

    private void Update() {
        float distanceToTarget = Vector3.Distance(transform.position, targetCar.position);
        if (distanceToTarget <= brakeDistance) {
            currentSpeed -= 2f * Time.deltaTime;
        } else {
            currentSpeed += 2f * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }
}