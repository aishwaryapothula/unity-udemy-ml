using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;


//sits between the manager and the character listening from the manager
// and telling the character what to do

//adds required components as dependencies
[RequireComponent(typeof (ThirdPersonCharacter))]
public class Brain : MonoBehaviour
{
    public int DNALength = 1;
    public float timeAlive;
    public float distanceTravelled;
    Vector3 startPosition;
    //sequence of dna
    public DNA dna;

    //to access third person character script
    private ThirdPersonCharacter m_Character;
    private Vector3 m_Move;
    private bool m_Jump;
    bool alive = true;

    void OnCollisionEnter(Collision obj)
    {
        if(obj.gameObject.tag == "dead")
        {
            alive = false;
        }
    }

    public void Init()
    {
        // Intitialize DNA
        // 0 forward
        // 1 back
        // 2 left
        // 3 right
        // 4 jump
        // 5 crouch
        // uses one int but has 6 values..DNA length has maxValue of 6
        dna = new DNA(DNALength, 6);
        m_Character = GetComponent<ThirdPersonCharacter>();
        timeAlive = 0;
        alive = true;
    }

    private void FixedUpdate()
    {
        // read DNA
        float h = 0;
        float v = 0;
        bool crouch = false;
        if (dna.GetGene(0) == 1) v = 1;//forward
        else if (dna.GetGene(0) == 1) v = -1;//backward
        else if (dna.GetGene(0) == 2) v = -1;//left
        else if (dna.GetGene(0) == 3) v = 1;//right
        else if (dna.GetGene(0) == 4) m_Jump=true;
        else if (dna.GetGene(0) == 1) crouch=true;

        m_Move = v * Vector3.forward + h * Vector3.right;
        m_Character.Move(m_Move, crouch, m_Jump);
        m_Jump = false;
        if (alive)
        {
            timeAlive += Time.fixedDeltaTime;
            distanceTravelled = Vector3.Distance(this.transform.position, startPosition);
        }
       

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

}
