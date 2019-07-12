using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    public GameObject ingredient;
    public int num;

    // Start is called before the first frame update
    void Start()
    {
        float scale = 1f; // this translates from local bounds coordinates
        float areaX = GetComponent<Collider>().bounds.size.x / 2;
        float areaZ = GetComponent<Collider>().bounds.size.z / 2;
        Vector3 center = GetComponent<Collider>().bounds.center;

        for (int i = 0; i < num; i++) {
            // get random x and z locations within bounds
            float x_loc = center.x + Random.Range(-areaX, areaX);
            float z_loc = center.z + Random.Range(-areaZ, areaZ);

            //set starting point of ray
            Vector3 new_loc = new Vector3(x_loc, 50, z_loc);
            Collider coll = GetComponent<Collider>();
            Ray ray = new Ray(new Vector3(x_loc, 50, z_loc), new Vector3(0, -1, 0));
            RaycastHit hit;

                if (coll.Raycast(ray, out hit, 200.0f))
                {
                    new_loc = hit.point;
                    //Debug.Log("Placing ingredient at ");
                    //Debug.Log(new_loc);
                    Instantiate(ingredient, new_loc, Quaternion.identity);
                }
                
        }
         
    }
}
