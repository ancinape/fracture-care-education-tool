/*
POST-TEST,,
Assessment #1,Tolerance at 0.95,
Red Screw,Trans.,Rot.
Ghost Red Screw,0,0.8286155
Ghost Green Screw,0,0.8286155
Ghost Blue Screw,0,0.8286155
Green Screw,Trans.,Rot.
Ghost Red Screw,0,0.8286155
Ghost Green Screw,0,0.8286155
Ghost Blue Screw,0,0.8286155
Blue Screw,Trans.,Rot.
Ghost Red Screw,0,0.8286155
Ghost Green Screw,0,0.8286155
Ghost Blue Screw,0,0.8286155
0 out of 3 screws are correctly placed,,
*/
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

    public Text assessResultEnd;

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
 private List<float> BoundsContainedPercentage( GameObject real_screw, GameObject ghost_screw )
 {
    var total = new List<float>(2);
    total.Add(1f);
    total.Add(1f);

    Bounds obj = real_screw.GetComponent<Collider>().bounds;
    Bounds region = ghost_screw.GetComponent<Collider>().bounds;

    for ( var i = 0; i < 3; i++)
    {
        total[0] *= Mathf.Clamp01(obj.center[i] / region.center[i]);
        //Debug.Log(real_screw.transform.eulerAngles);
        //total[1] *= Mathf.Clamp01(real_screw.transform.eulerAngles[i] / ghost_screw.transform.eulerAngles[i]);
    }
    total[1] = 1 - (Quaternion.Angle(real_screw.transform.rotation, ghost_screw.transform.rotation) / 180);
    return total;
 }

    void TaskOnClick()
    {
            assessCounter += 1;
            // Write to assessResult.txt
            StreamWriter writer = new StreamWriter((Application.dataPath + "/assessResult.csv"), true);
            Debug.Log("Wrote to" + Application.dataPath + "/assessResult.csv");
            if (StartGame.isPreTest)
                { writer.WriteLine("PRE-TEST" + ",,"); }
            else
                { writer.WriteLine("POST-TEST" + ",,"); }
            writer.WriteLine("Assessment #" + assessCounter + "," + "Tolerance at " + tolerancePercent + ",");
            int correct_screws = 0;
            foreach(GameObject screw in real_screws)
            {
                writer.WriteLine(screw.name + "," + "Translational Accuracy" + "," + "Rotational Accuracy");
                foreach(GameObject g_screw in ghost_screws)
                {
                    if (checkScrewInPlace(screw,g_screw))
                    {
                        if ((BoundsContainedPercentage(screw, g_screw)[0] >= tolerancePercent) && (BoundsContainedPercentage(screw, g_screw)[1] >= tolerancePercent))
                        {
                            Debug.Log(g_screw.name + "," + BoundsContainedPercentage(screw, g_screw)[0] + "," + BoundsContainedPercentage(screw, g_screw)[1]);
                            correct_screws += 1;
                        }
                    assessResult.text = (correct_screws + " out of 3 screws are correctly placed");
                    writer.WriteLine(g_screw.name + "," + BoundsContainedPercentage(screw, g_screw)[0] + "," + BoundsContainedPercentage(screw, g_screw)[1]);
                    continue;
                    }
                assessResult.text = (correct_screws + " out of 3 screws are correctly placed");
                writer.WriteLine(g_screw.name + "," + BoundsContainedPercentage(screw, g_screw)[0] + "," + BoundsContainedPercentage(screw, g_screw)[1]);
                }
            }
            writer.WriteLine(correct_screws + " out of 3 screws are correctly placed" + "," + ",");
            writer.Close();
            // End write
        if (assessCounter >= assessLimit)
        {
            if (!StartGame.isPreTest)
            {
                foreach (GameObject g_screw in ghost_screws) { g_screw.GetComponentInChildren<Renderer>().enabled=true; };
            };
            assess.gameObject.SetActive(false);
            assessResultEnd.gameObject.SetActive(true);
        };
    }
}
