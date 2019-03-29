using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{
    //gene for color
    public float r;
    public float g;
    public float b;
    // for the size of the sprites
    public float s;

    //clicked on not, value set to true afte clicking
    bool dead = false;
    //time that they live, fittest are bread
    public float timeToDie = 0;
    //these two are turned off when sprites die
    SpriteRenderer sRenderer;
    Collider2D sCollider;

    private void OnMouseDown()
    {
        dead = true;
        timeToDie = PopulationManager.elapsed;
        //Debug.Log("Dead At: " + timeToDie);
        //need genes so not destreoying but switching off
        sRenderer.enabled = false;
        sCollider.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //object creation for particular instance
        sRenderer = GetComponent<SpriteRenderer>();
        sCollider = GetComponent<Collider2D>();
        //put color component for renderer, values from populationManager
        sRenderer.color = new Color(r, g, b);
        // for the size of sprite, uniform size accross all dimensions
        this.transform.localScale = new Vector3(s, s, s); 
          
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
