using UnityEngine;
using UnityEngine.UI;

public class ShowIdealPositions : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Toggle GhostToggle;
    public GameObject Ghost_Screws;
    bool m_Activate;

    void Start()
    {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        GhostToggle.onValueChanged.AddListener((value) =>
        {
            TaskOnToggle(value);
        });
    }

    void TaskOnToggle(bool value)
    {
        

        if (value) {
        Ghost_Screws.SetActive(true);
        Debug.Log("Ghost Screws visible");
        } else {
        Ghost_Screws.SetActive(false);
        Debug.Log("Ghost Screws invisible");
        }

    }
}