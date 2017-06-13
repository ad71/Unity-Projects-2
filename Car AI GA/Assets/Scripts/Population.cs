using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour {

    private int populationSize = 3;
    public int generation = 1;
    public Transform path;
    public GameObject car;
    public bool verbose = false;

    private List<DNA> geneticData;
    public static List<float> fitness;
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
        fitness = new List<float>();
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
            GetComponent<Camera>().car = cars[index].transform;
        }
    }

    private void Update()
    {

        if (index == populationSize)
        {
            Evaluate();
            Select();
            Generate();
        }
        if (cars[index].GetComponent<CarEngine>().sw.ElapsedMilliseconds == 0)
        {
            // Cannot start stopwatch in the CarEngine class as it starts for cars that are not yet active and gives funny results
            cars[index].GetComponent<CarEngine>().sw.Start();
            GetComponent<Camera>().car = cars[index].transform;
        }
        cars[index].SetActive(true);
    }

    private void Evaluate()
    {
        Debug.Log("In evaluate");
        index = 0;
        generation++;
        float maxFit = 0;
        for (int i = 0; i < this.populationSize; ++i)
        {
            // For the ones timed-out, a small probability is added
            if (fitness[i] < 0) fitness[i] = 100;
            // Finding maximum fitness to normalize
            if (fitness[i] > maxFit) maxFit = fitness[i];
        }

        for(int i = 0; i < populationSize; ++i)
        {
            fitness[i] /= maxFit;
            Debug.Log("Normalized fitness: " + fitness[i]);
        }

        matingPool = new List<DNA>();
        // The 'not so efficient' way of adding DNA to the matingPool
        // Refactor this to use the monte carlo method
        for (int i = 0; i < populationSize; ++i)
        {
            int n = (int) Mathf.Floor(fitness[i] * 100);
            for(int j = 0; j < n; ++j)
            {
                matingPool.Add(geneticData[i]);
            }
        }
    }

    private void Select()
    {
        List<DNA> newDna = new List<DNA>();
        for(int i = 0; i < populationSize; ++i)
        {
            int indexA = Random.Range(0, matingPool.Count - 1);
            int indexB = Random.Range(0, matingPool.Count - 1);
            DNA parentA = matingPool[indexA];
            DNA parentB = matingPool[indexB];
            DNA child = parentA.Crossover(parentB);
        }
        geneticData = new List<DNA>();
        geneticData = newDna;
    }

    private void Generate()
    {
        /*for (int i = 0; i < populationSize; ++i)
        {
            geneticData.Add(new DNA());
            GameObject thisCar = Instantiate(car, new Vector3(0.5f, 10.038f, 0f), Quaternion.identity);
            CarEngine thisEngine = thisCar.GetComponent<CarEngine>();
            thisEngine.setDna(geneticData[i]);
            thisEngine.path = path;
            thisEngine.verbose = verbose;
            cars.Add(thisCar);
            GetComponent<Camera>().car = cars[index].transform;
        }*/
        cars = new List<GameObject>();
        for(int i = 0; i < populationSize; ++i)
        {
            GameObject thisCar = Instantiate(car, new Vector3(0.5f, 10.038f, 0f), Quaternion.identity);
            CarEngine thisEngine = thisCar.GetComponent<CarEngine>();
            thisEngine.setDna(geneticData[i]);
            thisEngine.path = path;
            thisEngine.verbose = verbose;
            cars.Add(thisCar);
            GetComponent<Camera>().car = cars[index].transform;
        }
    }
}