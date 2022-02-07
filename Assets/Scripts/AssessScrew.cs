using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AssessScrew : MonoBehaviour
{
    public GameObject[] ghost_screws;

    public GameObject[] real_screws;

    public Button assess;

    public Text assessResult;

    public float tolerancePercent;

    public int assessLimit = 3;

    private int assessCounter = 0;

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
 

    /*
    if (region.Contains(obj.min) 
    && region.Contains(obj.center)
    && region.Contains(obj.max))
    {
        for ( var i = 0; i < 3; i++)
        {
            total *= Mathf.Clamp01(obj.center[i] / region.center[i]);
        }
    }

    else
    {
        total = 0;
    }
    */

    for ( var i = 0; i < 3; i++)
    {
        total *= Mathf.Clamp01(obj.center[i] / region.center[i]);
    }

    Debug.Log("Overlap by" + total);
    return total;
 }

    void TaskOnClick()
    {

        if (!StartGame.isPreTest)
        {
            assessCounter += 1;
        }
        // Write to assessResult.txt
        StreamWriter writer = new StreamWriter("Assets/assessResult.txt", true);
        if (StartGame.isPreTest)
            { writer.WriteLine("PRE-TEST"); }
        else
            { writer.WriteLine("POST-TEST"); }
        writer.WriteLine("Assessment #" + assessCounter);
        writer.WriteLine("Tolerance at " + tolerancePercent);
        int correct_screws = 0;
        foreach(GameObject screw in real_screws)
        {
            foreach(GameObject g_screw in ghost_screws)
            {
                if (checkScrewInPlace(screw,g_screw))
                {
                    if (BoundsContainedPercentage(screw, g_screw) >= tolerancePercent)
                    {
                        correct_screws += 1;
                    }
                assessResult.text = (correct_screws + " out of 3 screws are correctly placed");
                writer.WriteLine("Placement accuracy of " + screw + " compared to " + g_screw + " is  " + BoundsContainedPercentage(screw, g_screw));
                continue;
                }

            writer.WriteLine("Placement accuracy of " + screw + " compared to " + g_screw + " is  " + BoundsContainedPercentage(screw, g_screw));
            
            }
        }
    writer.WriteLine(correct_screws + " out of 3 screws are correctly placed");
    writer.WriteLine("-----------------------");
    writer.WriteLine("-----------------------");
    writer.Close();
    // End write

    if (assessCounter >= assessLimit)
    {
            foreach (GameObject g_screw in ghost_screws) {
            g_screw.GetComponentInChildren<Renderer>().enabled=true;
            //g_screw.gameObject.SetActive( false );
            }
    }

        Debug.Log(correct_screws);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
