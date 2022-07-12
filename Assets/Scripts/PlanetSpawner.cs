using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public GameObject[] planets;

    Queue<GameObject> availablePlanets = new Queue<GameObject>();
    void Start()
    {
        availablePlanets.Enqueue(planets[0]);
        availablePlanets.Enqueue(planets[1]);
        availablePlanets.Enqueue(planets[2]);

        InvokeRepeating("MovePlanetDown", 0, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MovePlanetDown()
    {
        EnqueuePlanets();
        if (availablePlanets.Count == 0)
            return;

        GameObject aPlanet = availablePlanets.Dequeue();
        aPlanet.GetComponent<Planet>().isMoving = true;
    }

    void EnqueuePlanets()
    {
        foreach (GameObject aPlanet in planets)
        {
            if ((aPlanet.transform.position.y < 0) && (!aPlanet.GetComponent<Planet>().isMoving))
            {
                aPlanet.GetComponent<Planet>().ResetPosition();
                availablePlanets.Enqueue(aPlanet);
            }
        }
    }
}
