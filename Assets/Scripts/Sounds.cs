using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour {
    public AudioSource[] sources;
    NejikoController nejiko;

    // Use this for initialization
    void Start () {
        sources = gameObject.GetComponents<AudioSource>();
        nejiko = GetComponent<NejikoController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
           
            sources[0].Play();



        }
        if (Input.GetKeyDown("right"))
        {
            sources[1].Play();
                }
        if (Input.GetKeyDown("left"))
        {
            sources[2].Play() ;
        }


    }
}
