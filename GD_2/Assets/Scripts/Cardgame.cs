using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Cardgame : MonoBehaviour
{
    
    private Vector3 _cardSlot1 = new Vector3(-4.03f, 1.63f, 0); 
    private Vector3 _cardSlot2 = new Vector3(-4.03f,-2.26f, 0);
    private Vector3 _cardSlot3 = new Vector3(-0.02f, 1.63f, 0);
    private Vector3 _cardSlot4 = new Vector3(-0.02f, -2.26f, 0); 
    private Vector3 _cardSlot5 = new Vector3(4.01f, 1.63f, 0); 
    private Vector3 _cardSlot6 = new Vector3(4.01f, -2.26f, 0);


    public GameObject _pActiveCard1;

    public GameObject _pActiveCard2;

    public GameObject _pActiveCard3;

    public GameObject _eActiveCard1;
  
    public GameObject _eActiveCard2;

    public GameObject _eActiveCard3;    

    [System.NonSerialized]
    public GameObject hand_card = null;

    private Vector3[] _rows = {new Vector3(-4.03f,-0.35f,0), new Vector3(-0.02f,-0.35f,0), new Vector3(4.01f,-0.35f,0) };

    public GameObject winDirection;
    public GameObject drawDirection;

    //TO DO : Load an instance of all decks
    public List<GameObject> _playerDeck = new List<GameObject>();
    public List<GameObject> _enemyDeck = new List<GameObject>();
    public List<GameObject> _riverDeck = new List<GameObject>();

    public int _actRemaining = 1;

    public int _lpPlayer;
    public int _lpOpponent; 



    //Clean the scene of GameObjects containing the tag
    private void CleanThings(string thing_tag)
    {
        GameObject[] garbage = GameObject.FindGameObjectsWithTag(thing_tag);
        foreach(GameObject trash in garbage)
        {
            Destroy(trash);
        }
    }


    // Iniatialise the first cards or replace missing cards 
    public void InitTurn()
    {
        if(_eActiveCard1 == null)
        {
            _eActiveCard1 = DrawCards(1, _enemyDeck, _pActiveCard1, _cardSlot1);
        }
        if(_pActiveCard1 == null)
        {
            _pActiveCard1 = DrawCards(1, _playerDeck, _pActiveCard1, _cardSlot2);
        }

        if(_eActiveCard2 == null)
        {
            _eActiveCard2 = DrawCards(1, _enemyDeck, _eActiveCard2, _cardSlot3);
        }
        if(_pActiveCard2 == null)
        {
            _pActiveCard2 = DrawCards(1, _playerDeck, _pActiveCard2, _cardSlot4);
        }

        if(_eActiveCard3 == null)
        {
           _eActiveCard3 = DrawCards(1, _enemyDeck, _eActiveCard3, _cardSlot5);
        }
        if(_pActiveCard3 == null)
        {
           _pActiveCard3 = DrawCards(1, _playerDeck, _pActiveCard3, _cardSlot6);
        }
    }


    //Check the state of the row (win/lose/draw)
    public void CheckRowState(GameObject Playercard, GameObject Opponentcard, int row)
    {
        Playercard.tag = "Card";
        Opponentcard.tag = "Card";
        if(Playercard.GetComponent<GodCardData>().victoryList.Contains(Opponentcard.GetComponent<GodCardData>().GodName))
        {
            Instantiate(winDirection, _rows[row], Quaternion.Euler(0f,0f,-90f));
            Opponentcard.tag = "Destroyed";

        }
        if(Playercard.GetComponent<GodCardData>().defeatList.Contains(Opponentcard.GetComponent<GodCardData>().GodName))
        {
            Instantiate(winDirection, _rows[row], Quaternion.Euler(0f,0f,90f));
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
        CleanThings("Direction");
        CheckRowState(_pActiveCard1, _eActiveCard1, 0);
        CheckRowState(_pActiveCard2, _eActiveCard2, 1);
        CheckRowState(_pActiveCard3, _eActiveCard3, 2);
        
    }

    //Resolve the turn, count the score and destroy all losing cards
    void ResolveTurn()
    {
        GameObject[] pointCollector = GameObject.FindGameObjectsWithTag("Destroyed");
        foreach(GameObject point in pointCollector)
        {
            if (point.transform.position.y == 1.63f)
            {
                _lpOpponent -= 1;
            }
            else if(point.transform.position.y == -2.26f)
            {
                _lpPlayer -= 1;
            }
        }
        CleanThings("Destroyed");
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
        _actRemaining -= 1; 
    }

    //Shuffle a deck of cards
    public List<GameObject> Shuffle(List<GameObject> deck)
    {
        for(int i =0; i<deck.Count-1; i++)
        {
            int rand = Random.Range(0, deck.Count);
            GameObject tmp = deck[i];
            deck[i] = deck[rand];
            deck[rand] = tmp;
        }
        return deck;
    }

    //Draw a number of card (card in hand : slot = Vector3 (0,0,0))
    public GameObject DrawCards(int nb, List<GameObject> deck, GameObject activeCard, Vector3 slot)
    {
        GameObject card = null;
        for(int i =0; i<nb; i++)
        {
            int rand = Random.Range(0,deck.Count);
            if (slot == new Vector3(0,0,0))
            {
                card = Instantiate(deck[rand],new Vector3(i + -0.02f, -5.26f, 0), Quaternion.identity);
                card.GetComponent<GodCardData>().isMoveable = true;
                card.GetComponent<SpriteRenderer>().sortingOrder = 2;
                card.tag = "Card_Hand";
            }
            else
            {
                card = Instantiate(deck[0], slot, Quaternion.identity);
                deck.RemoveAt(0);
                
            }


            
        }
        return card;
    }


    //Do one Turn
    private IEnumerator Turn()
    {
        while((_lpPlayer != 0) || (_lpOpponent != 0))
        {
            yield return WaitForAction(1);
            InitTurn();
            CheckBoardState();
            DrawCards(3, _riverDeck, hand_card, new Vector3(0,0,0));
            yield return WaitForAction(0);
            CleanThings("Card_Hand");
            yield return WaitForKeyPress(KeyCode.Space);
            ResolveTurn();
            _actRemaining = 1;
            Debug.Log("You have " + _lpPlayer + " lives");
        }
        if(_lpPlayer != 0)
        {
            Debug.Log("You Lose");
        }
        else if(_lpOpponent != 0)
        {
            Debug.Log("You Won");
        }
    }

    //Wait for a Key to be press before running the rest of the script (Confirmation)
    private IEnumerator WaitForKeyPress(KeyCode key)
    {
        while (!Input.GetKeyDown(key))
        {
                yield return null;
            }

    }

    //Wait for actions to be perfomed by the player
    private IEnumerator WaitForAction(int nb)
    {
        while(_actRemaining > nb)
        {
            yield return null;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        _playerDeck = Shuffle(_playerDeck);
        _enemyDeck = Shuffle(_enemyDeck);
        _lpPlayer = _playerDeck.Count;
        _lpOpponent = _enemyDeck.Count;  
        StartCoroutine(Turn());
    }

    // Update is called once per frame
    void Update()
    {

    }


}
