using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//standard class not monobehaviour attached to brain
public class DNA 
{
    //a list of different properties,different chromozomes of DNA
    List<int> genes = new List<int>();
    //keeps track of the list of attributes
    int dnaLength = 0;
    // limits the number of attribtes when initializing
    int maxValues = 0;

    //constructor DNA
    public DNA(int l, int v)
    {
        dnaLength = l;
        maxValues = v;
        //random values for the length and maxValue
        SetRandom();
    }

    public void SetRandom()
    {
        genes.Clear();
        for(int i=0; i < dnaLength; i++)
        {
            //resets values by looping
            genes.Add(Random.Range(0, maxValues));
        }
    }

    //set int value for some attributes
    public void SetInt(int pos, int value)
    {
        genes[pos] = value;
    }

    //passing on genes to children
    public void Combine (DNA d1, DNA d2)
    {
        for(int i = 0; i < dnaLength; i++)
        {
            if(i < dnaLength / 2.0)
            {
                genes[i] = d1.genes[i];
            }
            else
            {
                genes[i] = d2.genes[i];
            }

        }
    }

    //mutation
    public void Mutate()
    {
        genes[Random.Range(0, dnaLength)] = Random.Range(0, maxValues);
    }

    public int GetGene(int pos)
    {
        return genes[pos];
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
