using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour {

    private int populationSize = 2;
    public int generation = 1;
    public Transform path;
    public GameObject car;
    public bool verbose = false;

    private List<DNA> geneticData;
    private List<float> weakness;
    // Improve matingPool system and replace it with monteCarlo method probably
    private List<DNA> matingPool;
    // private CarEngine thisEngine;
    // private GameObject thisCar;
    public static int index = 0;
    private bool timed = false;
    public static float tempWeakness = -1f;
    private List<GameObject> cars;
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

      ////////////////////////////////////////////////////////
     /// Do not forget to assign camera to follow new car ///
    ////////////////////////////////////////////////////////

    private void Start()
    {
        cars = new List<GameObject>();
        geneticData = new List<DNA>();
        weakness = new List<float>();
        matingPool = new List<DNA>();
        for(int i = 0; i < populationSize; ++i)
        {
            geneticData.Add(new DNA());
            GameObject thisCar = Instantiate(car, new Vector3(0.5f, 10.038f, 0f), Quaternion.identity);
            CarEngine thisEngine = thisCar.GetComponent<CarEngine>();
            thisEngine.setDna(geneticData[i]);
            thisEngine.path = path;
            thisEngine.verbose = verbose;
            cars.Add(thisCar);
        }
        GetComponent<Camera>().car = cars[index].transform;
    }

    private void Update()
    {
        // GameObject thisCar;
        // CarEngine thisEngine;
        // thisCar = Instantiate(car, new Vector3(0.5f, 10.038f, 0f), Quaternion.identity);
        // thisEngine = thisCar.GetComponent<CarEngine>();
        // thisEngine.setDna(geneticData[index]);
        // thisEngine.path = path;
        // thisEngine.verbose = verbose;
        // GetComponent<Camera>().car = thisCar.transform;
        // Destroy condition?
        // if (weakness.Count == index) Run(true);
        // else Run(false);
        cars[index].SetActive(true);
        // Debug.Log(index);
    }
}