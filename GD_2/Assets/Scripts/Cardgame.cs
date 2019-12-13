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

    public bool isMoveable = false;

}



public class Cardgame : MonoBehaviour
{
    
    private Vector3 _cardSlot1 = new Vector3(-4.03f, 1.63f, 0); 
    private Vector3 _cardSlot2 = new Vector3(-4.03f,-2.26f, 0);
    private Vector3 _cardSlot3 = new Vector3(-0.02f, 1.63f, 0);
    private Vector3 _cardSlot4 = new Vector3(-0.02f, -2.26f, 0); 
    private Vector3 _cardSlot5 = new Vector3(4.01f, 1.63f, 0); 
    private Vector3 _cardSlot6 = new Vector3(4.01f, -2.26f, 0);

    [System.NonSerialized]
    public GameObject _pActiveCard1;
    [System.NonSerialized]
    public GameObject _pActiveCard2;
    [System.NonSerialized]
    public GameObject _pActiveCard3;

    [System.NonSerialized]
    public GameObject _eActiveCard1;
    [System.NonSerialized]
    public GameObject _eActiveCard2;
    [System.NonSerialized]
    public GameObject _eActiveCard3;    

    private Vector3[] _rows = {new Vector3(-4.03f,-0.35f,0), new Vector3(-0.02f,-0.35f,0), new Vector3(4.01f,-0.35f,0) };

    public GameObject winDirection;
    public GameObject drawDirection;

    //TO DO : Load an instance of all decks
    public List<GameObject> _playerDeck = new List<GameObject>();
    public List<GameObject> _enemyDeck = new List<GameObject>();
    public List<GameObject> _riverDeck = new List<GameObject>();

    [SerializeField]
    public GodCardData GodCard = new GodCardData();





    // Iniatialise the first cards
    void InitGame()
    {
        _pActiveCard1 = Instantiate(_playerDeck[0], _cardSlot1, Quaternion.identity) as GameObject;
        _eActiveCard1 = Instantiate(_enemyDeck[0], _cardSlot2, Quaternion.identity) as GameObject;

        _pActiveCard2 = Instantiate(_playerDeck[1], _cardSlot3, Quaternion.identity) as GameObject;
        _eActiveCard2 = Instantiate(_enemyDeck[1], _cardSlot4, Quaternion.identity) as GameObject;
        
        _pActiveCard3 = Instantiate(_playerDeck[2], _cardSlot5, Quaternion.identity) as GameObject;
        _eActiveCard3 = Instantiate(_enemyDeck[2], _cardSlot6, Quaternion.identity) as GameObject;
    }


    //Check the state of the row (win/lose/draw)
    public void CheckRowState(GameObject Playercard, GameObject Opponentcard, int row)
    {
        if(Playercard.GetComponent<Cardgame>().GodCard.victoryList.Contains(Opponentcard.GetComponent<Cardgame>().GodCard.GodName))
        {
            Instantiate(winDirection, _rows[row], Quaternion.Euler(0f,0f,90f));
            Opponentcard.tag = "Destroyed";

        }
        else if(Playercard.GetComponent<Cardgame>().GodCard.defeatList.Contains(Opponentcard.GetComponent<Cardgame>().GodCard.GodName))
        {
            Instantiate(winDirection, _rows[row], Quaternion.Euler(0f,0f,-90f));
            Playercard.tag = "Destroyed";
        
        }
        else 
        {   
            Instantiate(drawDirection, _rows[row], Quaternion.identity);

        }
    }

    //Check all the board
    public void CheckBoardState()
    {
        GameObject[] garbage = GameObject.FindGameObjectsWithTag("Direction");
        foreach(GameObject trash in garbage)
        {
            Destroy(trash);
        }

        CheckRowState(_pActiveCard1, _eActiveCard1,0);
        CheckRowState(_pActiveCard2, _eActiveCard2, 1);
        CheckRowState(_pActiveCard3, _eActiveCard3, 2);
    }

    //Resolve the turn and destroy all losing cards
    void ResolveTurn()
    {
        GameObject[] garbage = GameObject.FindGameObjectsWithTag("Destroyed");
        foreach(GameObject trash in garbage)
        {
            Destroy(trash);
        }
    }

    //Change the active card
    public void ChangeActiveCard(GameObject oldcard, GameObject newcard)
    {
        Destroy(oldcard);
        if(newcard.transform.position.x == -4.03f)
        {
            _pActiveCard1 = newcard;
        }
        else if(newcard.transform.position.x == -0.02f)
        {
            _pActiveCard2 = newcard;
        }
        else
        {
            _pActiveCard3 = newcard;
        }
        CheckBoardState();
    }



    //Draw River Deck's card
    void DrawCards(int nb)
    {
        GameObject card;
        for(int i =0; i<nb; i++)
        {
            card = Instantiate(_riverDeck[i], new Vector3(i*-2.46f,-5.35f,0),Quaternion.identity);
            card.GetComponent<Cardgame>().GodCard.isMoveable = true;

        }
    }


    //Do one Turn
    private IEnumerator Turn()
    {
        InitGame();
        CheckBoardState();
        DrawCards(3);
        yield return WaitForKeyPress(KeyCode.Space);
        ResolveTurn();

    }

    private IEnumerator WaitForKeyPress(KeyCode key)
    {
        while (!Input.GetKeyDown(key)){
                yield return null;
            }

    }



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Turn());
    }

    // Update is called once per frame
    void Update()
    {

    }


}
