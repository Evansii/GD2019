using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GodCardData
{
    public string GodName;

    public List<string> victoryList = new List<string>();

    public List<string> defeatList = new List<string>();

    public List<string> drawList = new List<string>();


}



public class Cardgame : MonoBehaviour
{
    
    private Vector3 _cardSlot1 = new Vector3(-4.03f, 1.63f, 0); 
    private Vector3 _cardSlot2 = new Vector3(-4.03f,-2.26f, 0);


    private GameObject _pActiveCard1;
    private GameObject _pActiveCard2;
    private GameObject _pActiveCard3;

    private GameObject _eActiveCard1;
    private GameObject _eActiveCard2;
    private GameObject _eActiveCard3;    


    //TO DO : Load an instance of both decks
    public List<GameObject> _playerDeck = new List<GameObject>();
    public List<GameObject> _enemyDeck = new List<GameObject>();

    [SerializeField]
    public GodCardData GodCard = new GodCardData();


    void InitGame()
    {
        _pActiveCard1 = Instantiate(_playerDeck[0], _cardSlot1, Quaternion.identity) as GameObject;
        _eActiveCard1 = Instantiate(_enemyDeck[0], _cardSlot2, Quaternion.identity) as GameObject;

        
    }

    void CheckBoardState()
    {
        if(_pActiveCard1.GetComponent<Cardgame>().GodCard.victoryList.Contains(_eActiveCard1.GetComponent<Cardgame>().GodCard.GodName))
        {
            Debug.Log("You Beat it, Beat it");
        }
    }


    // Start is called before the first frame update
    void Start()
    {

       InitGame();
       CheckBoardState();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
