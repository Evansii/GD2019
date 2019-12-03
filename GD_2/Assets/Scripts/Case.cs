using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour
{

    public CaseData localCaseData = new CaseData();

    private Transition _transiScript;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
        localCaseData.caseName = gameObject.name;
        _transiScript = this.gameObject.GetComponent<Transition>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(localCaseData.caseName);
            activateCase();
        }

        if (localCaseData.isActive == true)
        {
            _transiScript.enabled = true;
            // _gameManager.localWorldData.activeCases.Add(localCaseData);

        }
    }

    void activateCase()
    {
        localCaseData.isActive = true;

    }

    


}
