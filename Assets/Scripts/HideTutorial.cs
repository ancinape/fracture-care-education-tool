using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideTutorial : MonoBehaviour
{

    public Text instructions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.J)) {
            Debug.Log(instructions.gameObject.activeSelf);
            if (instructions.gameObject.activeSelf) {
                instructions.gameObject.SetActive( false );
            }
            else {
                instructions.gameObject.SetActive( true );
            }
        }
    }
}
