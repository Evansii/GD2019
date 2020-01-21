using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{       

    public static GameManager Instance;

    public WorldData localWorldData = new WorldData();
    
    public PlayerData player1Data = new PlayerData();

    public PlayerData player2Data = new PlayerData();

    public GameObject thecase;

    public Case thecase_data;

    private UI_Map _ui; 

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
        _ui = GameObject.Find("UI").GetComponent<UI_Map>();
    }




    public void LoadDeck(string type, List<GameObject> deck)
    {
        if(type == "enemy")
        {
            localWorldData.currentEnnemyDeck = deck;
        }
        else if(type == "river")
        {
            localWorldData.currentRiverDeck = deck;
        }
        else
        {
            Debug.Log("Bad type of deck. Nothing loaded");
        }
    }

    void OnEnable() 
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
 
    void OnDisable() 
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
 
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        if(scene.name == "Map")
        {   
            localWorldData.currentTurn += 1;
            CheckWinner();
            ChangeActivePlayer();
        }
    }


    private void ChangeActivePlayer()
    {
        if(localWorldData.playerPlaying == 1)
        {
            localWorldData.playerPlaying = 2;
        }
        else
        {
            localWorldData.playerPlaying = 1;
        }
    }

    //Check if the player won the round and Add the case to the control list
    private void CheckWinner()
    {
        if(localWorldData.isPlayerWinner == true)
        {
            thecase = GameObject.Find(localWorldData.currentCaseFight);
            thecase_data = thecase.GetComponent<Case>();
            if(localWorldData.playerPlaying == 1)
            {
                UpdateCaseControl(thecase_data,player1Data);
                player1Data.PlayerControl.Add(thecase);
            }
            else if(localWorldData.playerPlaying == 2)
            {
                UpdateCaseControl(thecase_data, player2Data);
                player2Data.PlayerControl.Add(thecase);
            }
            else
            {
                Debug.Log("Error in player playing");
            }
        }
        else
        {
           Debug.Log("No territory change");
        }
        localWorldData.isPlayerWinner = false;
    }

    //Update the case Control + Sprite
    //TO DO: ADD THE SPRITE
    public void UpdateCaseControl(Case obj, PlayerData player)
    {
       if(player.PlayerRep > 50)
        {
            obj.localCaseData.caseControl="positive";
        }
        else if(player.PlayerRep < 50)
        {
            obj.localCaseData.caseControl="negative";
        }
        else
        {
            obj.localCaseData.caseControl="neutral";
        } 
    }

}


