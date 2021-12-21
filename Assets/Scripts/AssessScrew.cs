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
 /// <summary>
 /// Returns the percentage of obj contained by region. Both obj and region are calculated as quadralaterals for performance purposes.
 /// </summary>
 /// <param name="obj"></param>
 /// <param name="region"></param>
 /// <returns></returns>
 private float BoundsContainedPercentage( GameObject real_screw, GameObject ghost_screw )
 {
    var total = 1f;

    Bounds obj = real_screw.GetComponent<Collider>().bounds;
    Bounds region = ghost_screw.GetComponent<Collider>().bounds;
 
    for ( var i = 0; i < 3; i++ )
    {
        var dist = obj.min[i] > region.center[i] ?
            obj.max[i] - region.max[i] :
            region.min[i] - obj.min[i];

        total *= Mathf.Clamp01(1f - dist / obj.size[i]);
    }

    return total;
 }

    void TaskOnClick()
    {
        int correct_screws = 0;
        foreach(GameObject screw in real_screws)
        {
            foreach(GameObject g_screw in ghost_screws)
            {
                if (checkScrewInPlace(screw,g_screw))
                {
                    Debug.Log(BoundsContainedPercentage(screw, g_screw));
                    if (BoundsContainedPercentage(screw, g_screw) > 0.9)
                    {
                        correct_screws += 1;
                        assessResult.text = (correct_screws + " out of 3 screws are correctly placed");
                    }
    
                    continue;
                }
                else
                {
                    //Debug.Log(screw + " is NOT within " + g_screw + "!");
                }
            }
        }

        Debug.Log(correct_screws);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
