using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManager : MonoBehaviour
{
    //linking to the person 
    public GameObject personPrefab;
    public int populationSize = 10;
    //keep track of all persons created
    List<GameObject> population = new List<GameObject>();
    //to count the time
    public static float elapsed = 0;
    // Start is called before the first frame update
    //how long we want the game for
    int trialTime = 15;
    //keep track of generation
    int generation = 1;

    //To display generation no on screen during play
    GUIStyle guiStyle = new GUIStyle();
    private void OnGUI()
    {
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10,10,100,20), "Generation: " + generation, guiStyle);
        GUI.Label(new Rect(10, 65, 100, 20), "Trail TIme: " + (int)elapsed, guiStyle);

    }


    void Start()
    {
        //loops for populationSize times
     for (int i=0; i < populationSize; i++)
        {
            //random position
            Vector3 pos = new Vector3(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f), 0);
            //Quaternion means no rotation, perfectly aligned with world axes
            GameObject go = Instantiate(personPrefab, pos, Quaternion.identity);
            //selecting a color with a random combination of r,b,g
            go.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);
            //randomize the size of sprites
            go.GetComponent<DNA>().s = Random.Range(0.1f, 0.3f);
            //adding the person created to the list
            population.Add(go);
        }
    }

    GameObject Breed(GameObject parent1, GameObject Parent2)
    {
        //for children
        Vector3 pos = new Vector3(Random.Range(-8, 8), Random.Range(-4.5f, 4.5f), 0);
        GameObject offspring = Instantiate(personPrefab, pos, Quaternion.identity);
        //assigning DNA component to parents, creating objects to DNA class
        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = Parent2.GetComponent<DNA>();
        //for children
        //pass on parent DNA, make 50% probability of parent 1 or 2 DNA for child including size
        //condition ? consequence : alternative
        //main code bit for genetic algorithms
        //to introduce mutations
        if (Random.Range(0, 1000) < 1)
        {
            offspring.GetComponent<DNA>().r = Random.Range(0, 10) < 5 ? dna1.r : dna2.r;
            offspring.GetComponent<DNA>().g = Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
            offspring.GetComponent<DNA>().b = Random.Range(0, 10) < 5 ? dna1.b : dna2.b;
            offspring.GetComponent<DNA>().s = Random.Range(0, 10) < 5 ? dna1.s : dna2.s;

        }
        else
        {
            offspring.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f); 
            offspring.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
            offspring.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);
            offspring.GetComponent<DNA>().s = Random.Range(0.1f, 0.3f);
        }
        return offspring;
    }


    void BreedNewPopulation()
    {
       //creating a new list to keep track of new generation persons
       List<GameObject> newPopulation = new List<GameObject>();
        //getting rid of unfit
        // => is the lambda operator, o referes to time of (o)
        //fittest are at the botto of the sorted list
        List<GameObject> sortedList = population.OrderByDescending(o => o.GetComponent<DNA>().timeToDie).ToList();

        population.Clear();
        //to breed upper half of sorted list. i = (int)(sortedList.Count / 2.0f) -1 statrting at middle of list to bottom
        for (int i = (int)(sortedList.Count / 2.0f) -1; i < sortedList.Count -1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            //doing two times to maintain population size
            population.Add(Breed(sortedList[i+1], sortedList[i]));
            
        }

        //destroy all parents and previous population
        for (int i =0; i < sortedList.Count; i++)
        {
            //Destroy(object) removes a gameobject, component or asset
            Destroy(sortedList[i]);
        }
        generation++;



    }

    // Update is called once per frame
    void Update()
    {
        //for evert fram timer
        elapsed += Time.deltaTime;
        if(elapsed > trialTime)
        {
            //restarting new game 
            BreedNewPopulation();
            elapsed = 0;
        }
    }
}
