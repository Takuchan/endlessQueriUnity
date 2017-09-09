using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infinityRoad : MonoBehaviour {


    public GameObject road1;
    public GameObject road2;

    int border = 1000;

    void Update()
    {
        if (transform.position.z > border)
        {
            CreateMap();
        }
    }

    void CreateMap()
    {
        if (road1.transform.position.z < border)
        {
            border += 2000;
            Vector3 temp = new Vector3(0, 0.05f, border);
            road1.transform.position = temp;
        }
        else if (road2.transform.position.z < border)
        {
            border += 2000;
            Vector3 temp = new Vector3(0, 0.05f, border);
            road2.transform.position = temp;
        }
    }
}