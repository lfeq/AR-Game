using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] carPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        GameObject randomCar = carPrefabs[Random.Range(0, carPrefabs.Length)];
        Instantiate(randomCar, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
