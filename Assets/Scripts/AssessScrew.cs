using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssessScrew : MonoBehaviour
{
    public GameObject[] ghost_screws;

    public GameObject[] real_screws;

    public Button assess;

    public Text assessResult;

    void Start()
    {
        assess.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }
    /*
    string returnClosestScrew(GameObject[] real_screws, GameObject[] ghost_screws)
    {
        float smallest_distance_between_screws  = 99999;
        GameObject closest_screw;
        GameObject closest_g_screw;

        foreach(GameObject screw in real_screws)
        {
            foreach(GameObject g_screw in ghost_screws)
            {
                float dist = Vector3.Distance(screw.transform.position,g_screw.transform.position);
                if (dist < smallest_distance_between_screws)
                {
                    smallest_distance_between_screws = dist;
                    closest_screw = screw;
                    closest_g_screw = g_screw;
                    Debug.Log("Closest screws are " + smallest_distance_between_screws + " away between " + closest_screw + " and " + closest_g_screw);
                }

            }  
            
        }
        return ("Closest screws are " + smallest_distance_between_screws + " away ");
    }
    */

    bool checkScrewInPlace(GameObject real_screw, GameObject ghost_screw)
    {
        Collider real_screw_collider = real_screw.GetComponent<Collider>();
        Collider ghost_screw_collider = ghost_screw.GetComponent<Collider>();

        if (ghost_screw_collider.bounds.Contains(real_screw_collider.bounds.center))
        {
            Debug.Log(real_screw + " is within " + ghost_screw + "!");
            return true;
        }
        else
        {
            return false;
        }
    }

    void TaskOnClick()
    {
        foreach(GameObject screw in real_screws)
        {
            foreach(GameObject g_screw in ghost_screws)
            {
                if (checkScrewInPlace(screw,g_screw))
                {
                    assessResult.text = (screw + " is within " + g_screw + "!");
                }
                else
                {
                    //Debug.Log(screw + " is NOT within " + g_screw + "!");
                }
            }
        }   

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
