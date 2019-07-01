using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeLocator : MonoBehaviour
{
    public GameObject forest;
    public GameObject desert;
    public GameObject plains;
    public GameObject rocks;
    Collider forest_coll;
    Collider desert_coll;
    Collider plains_coll;
    Collider rocks_coll;

    enum biomeType {
        forest = 0,
        desert = 1,
        plains = 2,
        rocks = 3
    };
    biomeType biome;


    // Start is called before the first frame update
    void Start()
    {
        forest_coll = forest.GetComponent<Collider>();
        desert_coll = desert.GetComponent<Collider>();
        plains_coll = plains.GetComponent<Collider>();
        rocks_coll = rocks.GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = gameObject.transform.position;
        if (forest_coll.bounds.Contains(pos)) 
        {
            biome = biomeType.forest;
        } else if (desert_coll.bounds.Contains(pos))
        {
            biome = biomeType.desert;
        } else if (plains_coll.bounds.Contains(pos))
        {
            biome = biomeType.plains;
        } else if (rocks_coll.bounds.Contains(pos))
        {
            biome = biomeType.rocks;
        } else {
            //Debug.Log("In unknown biome");
        }
        //Debug.Log("Current Biome: " + biome);
    }

    public int getCurrentBiome() 
    {
        return (int)biome;
    }
}
