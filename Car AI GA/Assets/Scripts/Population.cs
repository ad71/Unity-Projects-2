using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Population : MonoBehaviour {

    private int populationSize = 2;
    public int generation = 1;
    public Transform path;
    public GameObject car;
    public bool verbose = false;
    public bool debug = true;
    public Text timeText;
    public Text genomeText;
    public Text generationText;
    public Text maxFitnessText;

    private List<DNA> geneticData;
    public static List<float> fitness;
    // Improve matingPool system and replace it with monteCarlo method probably
    private List<DNA> matingPool;
    public static int index = 0;
    public static float tempWeakness = -1f;
    private List<GameObject> cars;

    private void Start()
    {
        maxFitnessText.text = "";
        cars = new List<GameObject>();
        if (debug) Debug.Log("Init, gameobject array cars count: " + cars.Count);
        geneticData = new List<DNA>();
        if (debug) Debug.Log("Init, dna list count: " + geneticData.Count);
        fitness = new List<float>();
        if (debug) Debug.Log("Init, fitness list count: " + fitness.Count);
        matingPool = new List<DNA>();
        if (debug) Debug.Log("Init, mating pool count: " + matingPool.Count);
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
        if (debug)
        {
            Debug.Log("Count of cars after population: " + cars.Count);
            Debug.Log("Genes of the first car: " + cars[0].GetComponent<CarEngine>().getDna().genes[0] + " " + cars[0].GetComponent<CarEngine>().getDna().genes[1]);
            Debug.Log("Genes of the second car: " + cars[1].GetComponent<CarEngine>().getDna().genes[0] + " " + cars[1].GetComponent<CarEngine>().getDna().genes[1]);
        }
    }

    private void Update()
    {
        if (index == populationSize)
        {
            Evaluate();
            Select();
        }
        if (cars[index].GetComponent<CarEngine>().sw.IsRunning == false)
        {
            // Cannot start stopwatch in the CarEngine class as it starts for cars that are not yet active and gives funny results
            cars[index].GetComponent<CarEngine>().sw.Start();
            GetComponent<Camera>().car = cars[index].transform;
            cars[index].SetActive(true);
        }
        // If cars don't activate, put the last line of the previous if statement below this comment
        else
        {
            timeText.text = cars[index].GetComponent<CarEngine>().sw.ElapsedMilliseconds.ToString();
            genomeText.text = "Genome: " + (index + 1).ToString();
            generationText.text = "Generation: " + generation.ToString();
        }
        // GUIData();
    }

    /* private void OnGUI()
    {
        if (GUI.Button(new Rect(3, 50, 100, 25), "Kill"))
            Kill();
    } */

    private void Evaluate()
    {
        if (debug) Debug.Log("In evaluate");
        index = 0;
        generation++;
        float maxFit = 0;
        for (int i = 0; i < this.populationSize; ++i)
        {
            // For the ones timed-out, a small probability is added
            if (fitness[i] < 0) fitness[i] = 1000;
            // Finding maximum fitness to normalize
            if (fitness[i] > maxFit) maxFit = fitness[i];
        }
        maxFitnessText.text = "Gen" + generation.ToString() + " minimum: " + (120000f - maxFit) / 1000f + " s";
        if (debug) Debug.Log("Maximum fitness of this generation is: " + maxFit);
        for(int i = 0; i < populationSize; ++i)
        {
            fitness[i] /= maxFit;
            if (debug) Debug.Log("Normalized fitness: " + fitness[i]);
        }

        matingPool = new List<DNA>();
        // The 'not so efficient' way of adding DNA to the matingPool
        // Refactor this to use the monte carlo method
        if (debug) Debug.Log("Filling Mating pool");
        for (int i = 0; i < populationSize; ++i)
        {
            int n = (int) Mathf.Floor(fitness[i] * 100);
            for(int j = 0; j < n; ++j)
            {
                matingPool.Add(geneticData[i]);
            }
        }
        if (matingPool.Count < 1) Debug.Log("Mating pool empty");
        if (debug) Debug.Log("Mating pool size: " + matingPool.Count);
    }

    private void Select()
    {
        if (debug) Debug.Log("In select");
        cars = new List<GameObject>();
        if (debug) Debug.Log("Cars count: " + cars.Count);
        // List<DNA> newDna = new List<DNA>();
        for (int i = 0; i < populationSize; ++i)
        {
            // To do: prevent both parents from being the same

            int indexA = Random.Range(0, matingPool.Count - 1);
            if (debug) Debug.Log("IndexA: " + indexA);
            int indexB = Random.Range(0, matingPool.Count - 1);
            if (debug) Debug.Log("IndexB: " + indexB);
            DNA parentA = matingPool[indexA];
            if (parentA == null) Debug.Log("ParentA is null");
            if (debug)
            {
                Debug.Log("ParentA genes length: " + parentA.genes.Count);
                Debug.Log("ParentA genes: " + parentA.genes[0] + " " + parentA.genes[1]);
            }
            DNA parentB = matingPool[indexB];
            if (parentB == null) Debug.Log("ParentB is null");
            if (debug)
            {
                Debug.Log("ParentB genes length: " + parentB.genes.Count);
                Debug.Log("ParentB genes: " + parentB.genes[0] + " " + parentB.genes[1]);
            }
            DNA child = parentA.Crossover(parentB);
            if (child == null && debug) Debug.Log("Child is null");
            child.Mutate();
            if (debug) Debug.Log("Mutation done");
            GameObject thisCar = Instantiate(car, new Vector3(0.5f, 10.038f, 0f), Quaternion.identity);
            CarEngine thisEngine = thisCar.GetComponent<CarEngine>();
            thisEngine.setDna(child);
            thisEngine.path = path;
            thisEngine.verbose = verbose;
            if (thisCar == null && debug) Debug.Log("This car is null");
            if (debug) Debug.Log("Control reaches here");
            cars.Add(thisCar);
            if (debug) Debug.Log("Cars count in Select function: " + cars.Count);
            GetComponent<Camera>().car = cars[index].transform;
        }
        fitness = new List<float>();
        matingPool = new List<DNA>();
        index = 0;
        if (debug) Debug.Log("Generation: " + generation);
    }

    private void Kill()
    {
        Debug.Log("In kill");
    }

    /* private void GUIData()
    {
        float maxFit = 0f;
        for (int i = 0; i < fitness.Count; ++i)
        {
            if (fitness[i] < 0) fitness[i] = 1000;
            if (fitness[i] > maxFit) maxFit = fitness[i];
        }
        if (fitness.Count > 0) maxFitnessText.text = "Minimum: " + (120000 - maxFit) / 1000f + "s";
    } */
}