using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour {

    public int populationSize = 1;
    public GameObject car;

    private List<DNA> geneticData;
    /* public static List<GameObject> cars = new List<GameObject>();
    public static int numSpawned = 0;

    private void Start()
    {
        Object[] subListObjects = Resources.LoadAll("Prefabs", typeof(GameObject));
        foreach (GameObject subListObject in subListObjects)
        {
            GameObject car = (GameObject)subListObject;
            cars.Add(car);
        }
    } */


    // Do not forget to assign camera to follow new car
    private void Start()
    {
        geneticData = new List<DNA>();
        for(int i = 0; i < populationSize; ++i)
        {
            geneticData.Add(new DNA());
        }
        GameObject thisCar = (GameObject) Instantiate(car, new Vector3(0.5f, 10.038f, 0f), Quaternion.identity);
        thisCar.GetComponent<CarEngine>().setDna(geneticData[0]);
        GetComponent<Camera>().car = thisCar.transform;
    }
}
