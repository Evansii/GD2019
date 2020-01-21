using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cardgame : MonoBehaviour
{

    private GameManager _gameManager;

    [SerializeField]
    private GameObject[] _playerslots;
    [SerializeField]
    private GameObject[] _opponentslots;

    private BoxCollider2D _playerAbyss;
    private GameObject _playerAbyssCard;
    
    private BoxCollider2D _opponentAbyss;
    private GameObject _opponentAbyssCard;

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

    public List<GameObject> playerhand = new List<GameObject>();
    public List<GameObject> opponenthand = new List<GameObject>();

    private Vector3[] _rows = {new Vector3(-4.03f,-0.35f,0), new Vector3(-0.02f,-0.35f,0), new Vector3(4.01f,-0.35f,0) };

    public GameObject winDirection;
    public GameObject drawDirection;

    //TO DO : Load an instance of all decks
    public List<GameObject> _playerDeck;
    public List<GameObject> _enemyDeck;
    public List<GameObject> _riverDeck;

    public int player_actRemaining = 1;
    public int opponent_actRemaining = 1;

    public bool playerturn = false;

    public int _lpPlayer;
    public int _lpOpponent;

    private Sprite _sprite1toWin;
    public Button _button1toWin;
    private GameObject card1_to_win;

    private Sprite _sprite2toWin;
    public Button _button2toWin;
    private GameObject card2_to_win;

    private Sprite _sprite3toWin;
    public Button _button3toWin;
    private GameObject card3_to_win;

    private UI_Card ui; 

    private GameObject _cardchoice;



    //Clean the scene of GameObjects containing the tag
    private void CleanThings(string thing_tag)
    {
        GameObject[] garbage = GameObject.FindGameObjectsWithTag(thing_tag);
        foreach(GameObject trash in garbage)
        {
            if(thing_tag == "Card_Hand")
            {
                playerhand.Remove(trash);
                opponenthand.Remove(trash);
            }
            Destroy(trash);
        }
    }


    // Initialize the first cards or replace missing cards 
    public void InitTurn()
    {
        if(_eActiveCard1 == null && _enemyDeck.Count != 0)
        {
            _eActiveCard1 = DrawCards(1, _enemyDeck, _pActiveCard1, _cardSlot1);
        }
        if(_pActiveCard1 == null && _playerDeck.Count != 0)
        {
            _pActiveCard1 = DrawCards(1, _playerDeck, _pActiveCard1, _cardSlot2);
        }

        if(_eActiveCard2 == null && _enemyDeck.Count != 0)
        {
            _eActiveCard2 = DrawCards(1, _enemyDeck, _eActiveCard2, _cardSlot3);
        }
        if(_pActiveCard2 == null && _playerDeck.Count != 0)
        {
            _pActiveCard2 = DrawCards(1, _playerDeck, _pActiveCard2, _cardSlot4);
        }

        if(_eActiveCard3 == null && _enemyDeck.Count != 0)
        {
           _eActiveCard3 = DrawCards(1, _enemyDeck, _eActiveCard3, _cardSlot5);
        }
        if(_pActiveCard3 == null && _playerDeck.Count != 0)
        {
           _pActiveCard3 = DrawCards(1, _playerDeck, _pActiveCard3, _cardSlot6);
        }

        if(playerturn == false)
        {
            DrawCards(3, _riverDeck, hand_card, new Vector3(0,0,0));
            playerturn = true;
            DrawCards(3, _riverDeck, hand_card, new Vector3(0,0,0));
            playerturn = false;
        }
        else if(playerturn)
        {
            DrawCards(3, _riverDeck, hand_card, new Vector3(0,0,0));
            playerturn = false;
            DrawCards(3, _riverDeck, hand_card, new Vector3(0,0,0));
            playerturn = true;
        }
    }


    //Check the state of the row (win/lose/draw)
    public void CheckRowState(GameObject Playercard, GameObject Opponentcard, int row)
    {
        if(Playercard != null && Opponentcard != null)
        {
            Playercard.tag = "Card";
            Opponentcard.tag = "Card";
            if(Playercard.GetComponent<GodCardData>().victoryList.Contains(Opponentcard.GetComponent<GodCardData>().GodName))
            {
                Instantiate(winDirection, _rows[row], Quaternion.Euler(0f,0f,-90f));
                Opponentcard.tag = "Destroyed";
            }
            else if(Playercard.GetComponent<GodCardData>().defeatList.Contains(Opponentcard.GetComponent<GodCardData>().GodName))
            {
                Instantiate(winDirection, _rows[row], Quaternion.Euler(0f,0f,90f));
                Playercard.tag = "Destroyed";
            
            }
            else 
            {   
                Instantiate(drawDirection, _rows[row], Quaternion.identity);
            }
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

    //Activate or Desactive things in function of who is it to play
    public void CheckPlayerTurn()
    {
        if(playerturn == true)
        {
            foreach(GameObject card in playerhand)
            {
                card.GetComponent<GodCardData>().isMoveable = true;
            }
            foreach(GameObject card in opponenthand)
            {
                card.GetComponent<GodCardData>().isMoveable = false;
            }

            foreach(GameObject slot in _playerslots)
            {
               slot.GetComponent<BoxCollider2D>().enabled = true;
            }
            foreach(GameObject slot in _opponentslots)
            {
                slot.GetComponent<BoxCollider2D>().enabled = false;
            }
            // _opponentAbyss.enabled = false;
            // _playerAbyss.enabled = true;
        }
        else
        { 
            foreach(GameObject card in opponenthand)
            {
                card.GetComponent<GodCardData>().isMoveable = true;
            }
            foreach(GameObject card in playerhand)
            {
                card.GetComponent<GodCardData>().isMoveable = false;
            } 
            foreach(GameObject slot in _playerslots)
            {
                slot.GetComponent<BoxCollider2D>().enabled = false;
            }
            foreach(GameObject slot in _opponentslots)
            {
                slot.GetComponent<BoxCollider2D>().enabled = true;
            }
            // _opponentAbyss.enabled = true;
            // _playerAbyss.enabled = false;
        }

    }

    //Prevent a Hand Card from being destroyed at the end of a turn
    public void PutCardinAbyss(GameObject card)
    {
        card.GetComponent<SpriteRenderer>().sortingOrder = 2;
        if(playerturn == true)
        {
            if(_playerAbyssCard != null)
            {
                playerhand.Remove(_playerAbyssCard);
                Destroy(_playerAbyssCard);

            }
            _playerAbyssCard = card;
            player_actRemaining -= 1;
            _playerAbyss.enabled = false;
            ChangePlayerPriority(false);
        }
        else
        {
            if(_opponentAbyssCard != null)
            {
                opponenthand.Remove(_opponentAbyssCard);
                Destroy(_opponentAbyssCard);
            }
            _opponentAbyssCard = card;
            opponent_actRemaining -= 1;
            _opponentAbyss.enabled = false;
            ChangePlayerPriority(true);
        }

    }

    //Resolve the turn, count the score and destroy all losing cards
    public void ResolveTurn()
    {
        GameObject[] pointCollector = GameObject.FindGameObjectsWithTag("Destroyed");
        foreach(GameObject point in pointCollector)
        {
            if (point.transform.position.y == 1.63f)
            {
                _lpOpponent -= 1;
                ui.UpdateEnemyLp(_lpOpponent);
            }
            else if(point.transform.position.y == -2.26f)
            {
                _lpPlayer -= 1;
                ui.UpdatePlayerLp(_lpPlayer);
            }
        }
        CleanThings("Destroyed");
    }

    public void ChangePlayerPriority(bool turn)
    {
        playerturn = turn;
        if(playerturn == true)
        {
            player_actRemaining += 1;
        }
        else
        {
            opponent_actRemaining += 1;
        }
        ui.WhoIsPlaying(playerturn);
        CheckPlayerTurn();
    }

    //Change the active card
    public void ChangeActiveCard(GameObject oldcard, GameObject newcard)
    {
        Destroy(oldcard);
        playerhand.Remove(newcard);
        opponenthand.Remove(newcard);
        newcard.GetComponent<SpriteRenderer>().sortingOrder = 2;
        if(newcard.transform.position == _cardSlot1)
        {
            _eActiveCard1 = newcard;
        }
        else if(newcard.transform.position == _cardSlot2)
        {
            _pActiveCard1 = newcard;
        }
        else if(newcard.transform.position == _cardSlot3)
        {
            _eActiveCard2 = newcard;
        }
        else if(newcard.transform.position == _cardSlot4)
        {
            _pActiveCard2 = newcard;
        }
        else if(newcard.transform.position == _cardSlot5)
        {
            _eActiveCard3 = newcard;
        }
        else
        {
            _pActiveCard3 = newcard;
        }
        if(playerturn == true)
        {
            player_actRemaining -= 1;
            ChangePlayerPriority(false);
        }
        else
        {  
            opponent_actRemaining -= 1;
            ChangePlayerPriority(true);
        }
        CheckBoardState();
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
            if (deck == _riverDeck)
            {
                if(playerturn == true)
                {
                    card = Instantiate(deck[rand],new Vector3(i + -0.02f, -5.26f, 0), Quaternion.identity);
                    playerhand.Add(card);
                }
                else
                {
                    card = Instantiate(deck[rand],new Vector3(i + -0.02f, 4.71f, 0), Quaternion.identity);
                    opponenthand.Add(card);
                }

                card.GetComponent<SpriteRenderer>().sortingOrder = 3;
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

    public void PassTurn()
    {
        if(playerturn == true)
        {
            player_actRemaining = 0;
            playerturn = false;
            CheckPlayerTurn();
        }
        else
        {
            opponent_actRemaining = 0;
            playerturn = true;
            CheckPlayerTurn();
        }
        ui.WhoIsPlaying(playerturn);
    }

    //Do one Turn
    private IEnumerator Turn()
    {
        while((_lpPlayer != 0) && (_lpOpponent != 0))
        {
            yield return WaitForAction(1);
            InitTurn();
            ui.WhoIsPlaying(playerturn);
            CheckBoardState();
            CheckPlayerTurn();
            yield return WaitForAction(0);
            CleanThings("Card_Hand");
            yield return WaitForKeyPress(KeyCode.Space);
            ResolveTurn();
            player_actRemaining = 1;
            opponent_actRemaining = 1;
            if(playerturn == false)
            {
                playerturn = true;
            }
            else
            {
                playerturn = false;
            }

        }
        if(_lpPlayer == 0)
        {
            _gameManager.localWorldData.isPlayerWinner = false;
            Debug.Log("You Lose");
            SceneManager.LoadScene("Map");

        }
        else if(_lpOpponent == 0)
        {
            _gameManager.localWorldData.isPlayerWinner = true;
            Debug.Log("You Won");
            _cardchoice.SetActive(true);
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
        while((player_actRemaining > nb) || (opponent_actRemaining > nb)) 
        {
            yield return null;
        }
    }


    public void AddCardtoDeck(GameObject cardtowin)
    {
        if(_gameManager.localWorldData.playerPlaying == 1)
        {
            _gameManager.player1Data.PlayerDeck.Add(cardtowin);
        }
        else
        {
            _gameManager.player2Data.PlayerDeck.Add(cardtowin);
        }
        SceneManager.LoadScene("Map");
    }

    public void CheckCardToWin()
    {
        _button1toWin.onClick.AddListener(delegate{AddCardtoDeck(card1_to_win);});
        _button2toWin.onClick.AddListener(delegate{AddCardtoDeck(card2_to_win);});
        _button3toWin.onClick.AddListener(delegate{AddCardtoDeck(card3_to_win);});
    }




    // Start is called before the first frame update
    void Start()
    {
        //Gather game Elements
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        ui = GameObject.Find("UI").GetComponent<UI_Card>();
        _playerAbyss = GameObject.Find("PAbyss").GetComponent<BoxCollider2D>();
        _opponentAbyss = GameObject.Find("EAbyss").GetComponent<BoxCollider2D>();
        _cardchoice = GameObject.Find("CardChoice");

        _cardchoice.SetActive(false);

        //Load Decks
        _playerDeck = _gameManager.player1Data.PlayerDeck;
        _enemyDeck = _gameManager.localWorldData.currentEnnemyDeck;
        _riverDeck = _gameManager.localWorldData.currentRiverDeck;

        //Shuffle Decks
        _playerDeck = Shuffle(_playerDeck);
        _enemyDeck = Shuffle(_enemyDeck);

        //Display Life Points        
        _lpPlayer = _playerDeck.Count;
        ui.UpdatePlayerLp(_lpPlayer);

        _lpOpponent = _enemyDeck.Count;  
        ui.UpdateEnemyLp(_lpOpponent);

        //Determine rewards
        int rand1Reward = Random.Range(0,_enemyDeck.Count);
        card1_to_win = _enemyDeck[rand1Reward];
        int rand2Reward = Random.Range(0,_enemyDeck.Count);
        card2_to_win = _enemyDeck[rand2Reward];
        int rand3Reward = Random.Range(0,_enemyDeck.Count);
        card3_to_win = _enemyDeck[rand3Reward];

        //Start a turn
        StartCoroutine(Turn());
    }

    // Update is called once per frame
    void Update()
    {

    }


}
