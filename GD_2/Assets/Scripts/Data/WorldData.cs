using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WorldData 
{

    public int playerPlaying; 
    
    public int currentTurn = 1;

    public int playerTurn = 1;

    public Dictionary<string, CaseData> caseList = new Dictionary<string, CaseData>();

    public List<GameObject> currentEnnemyDeck;

    public List<GameObject> currentRiverDeck;

    public string currentCaseFight;

    public bool isPlayerWinner;

    public GameObject card1_To_win;
    public GameObject card2_To_win;
    public GameObject card3_To_win;


}
