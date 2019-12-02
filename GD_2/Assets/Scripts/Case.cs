using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour
{
    
    public bool isCorrupted = false;

    public string caseControl = "neutral";

    public bool isActive = false;

    private Transition _transiScript;


    // Start is called before the first frame update
    void Start()
    {
        _transiScript = this.gameObject.GetComponent<Transition>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("oui");
            activateCase();
        }

        if (isActive == true)
        {
            _transiScript.enabled = true;
        }
    }

    void activateCase()
    {
        isActive = true;

    }

    


}
