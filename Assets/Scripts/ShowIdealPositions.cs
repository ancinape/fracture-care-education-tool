using UnityEngine;
using UnityEngine.UI;

public class ShowIdealPositions : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Toggle GhostToggle;
    public GameObject[] ghost_screws;
    bool m_Activate;

    void Start()
    {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        GhostToggle.onValueChanged.AddListener((value) =>
        {
            TaskOnToggle(value);
        });

        // Make screws invisible at beginning
        foreach (GameObject g_screw in ghost_screws) {
                g_screw.GetComponentInChildren<Renderer>().enabled=false;
                //g_screw.gameObject.SetActive( false );
        }
    }

    void TaskOnToggle(bool value)
    {
        

        if (value)
        {
            foreach (GameObject g_screw in ghost_screws) {
                g_screw.GetComponentInChildren<Renderer>().enabled=true;
                //g_screw.gameObject.SetActive( true );
            }
        }
        else
        {
            foreach (GameObject g_screw in ghost_screws) {
                g_screw.GetComponentInChildren<Renderer>().enabled=false;
                //g_screw.gameObject.SetActive( false );
            }
        }

    }
}