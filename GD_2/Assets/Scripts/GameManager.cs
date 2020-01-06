using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{       

    public static GameManager Instance;

    public WorldData localWorldData = new WorldData();

    [System.NonSerialized]
    public string caseToLoad;


    void Awake() {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } 
        else if (Instance != this)
        {
            Destroy(gameObject);
        }   
    }

    void Update() 
    {
        
    }

    void Start()
    {
        localWorldData.caseList.Add("wood",GameObject.Find("Wood").GetComponent<Case>().localCaseData);
    }

}

