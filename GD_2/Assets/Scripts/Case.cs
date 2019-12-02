using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour
{
    
    public bool isCorrupted = false;

    public string caseControl = "neutral";

    public bool isActive = false;

    private Transition _transiScript;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _transiScript = this.gameObject.GetComponent<Transition>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
            _gameManager.localWorldData.activeCases.Add(this.gameObject);
        
        }
    }

    void activateCase()
    {
        isActive = true;

    }

    


}
