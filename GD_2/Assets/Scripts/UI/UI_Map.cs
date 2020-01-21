using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Map : MonoBehaviour
{

    private GameManager _gameManager;

    [System.NonSerialized]
    public GameObject _book;

    private GameObject _versus;

    [SerializeField]
    private GameObject _versusButton;

    [SerializeField]
    private GameObject _story;

    [SerializeField]
    private GameObject _storyButton;

    [SerializeField]
    private List<GameObject> _turnStoryList;

    [SerializeField]
    private ProgressBars _player1ProgressBar;
    [SerializeField]
    private ProgressBars _player2ProgressBar;

    [SerializeField]
    private Text _player1Name;
    [SerializeField]
    private Text _player1Score;
    [SerializeField]
    private Text _player1Reput;

    [SerializeField]
    private Text _player2Name;
    [SerializeField]
    private Text _player2Score;
    [SerializeField]
    private Text _player2Reput;

    [System.NonSerialized]
    public Text _loreText;
    [System.NonSerialized]
    public Text _choice1Text;
    [System.NonSerialized]
    public Text _choice2Text;
    [System.NonSerialized]
    public Text _choice3Text;

    [System.NonSerialized]
    public Image _loreImage;



    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _book = GameObject.Find("Book");
        _versus = GameObject.Find("Versus_Screen");

         _loreText = GameObject.Find("Lore_Text").GetComponent<Text>();
         _loreImage = GameObject.Find("Lore_Image").GetComponent<Image>();
        _choice1Text = GameObject.Find("Choix1_Text").GetComponent<Text>();
        _choice2Text = GameObject.Find("Choix2_Text").GetComponent<Text>();
        _choice3Text = GameObject.Find("Choix3_Text").GetComponent<Text>();


        GameObject[] tmp_list = GameObject.FindGameObjectsWithTag("Story_Text");
        foreach (var story in tmp_list)
        {
            _turnStoryList.Add(story);
        }

        _versus.SetActive(false);
        _book.SetActive(false);
        _story.SetActive(false);
        
        UpdatePlayerinfo();
        OpenStory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Open and Close the Story screen + anims
    public void OpenStory()
    {
        _storyButton.SetActive(false);
        _versusButton.SetActive(false);
        StartCoroutine(OpenStoryAnim());
    }

    public IEnumerator OpenStoryAnim()
    {
        _story.SetActive(true);
        GameObject[] tmp_list = GameObject.FindGameObjectsWithTag("Story_Text");
        for(int i=0; i<tmp_list.Length; i++)
        {
            if(i+1 != _gameManager.localWorldData.currentTurn){
                tmp_list[i].SetActive(false);
            }
        }
        yield return new WaitForSeconds(0.1f);
    }

    public void CloseStory()
    {
        _storyButton.SetActive(true);
        _versusButton.SetActive(true);
        StartCoroutine(CloseStoryAnim());
    }

    public IEnumerator CloseStoryAnim()
    {
        _story.SetActive(false);
        yield return new WaitForSeconds(0.1f);  
    }



    //Open and Close the Versus screen + anims
    public void OpenVersus()
    {
        _storyButton.SetActive(false);
        _versusButton.SetActive(false);
        StartCoroutine(OpenVersusAnim());
    }

    public IEnumerator OpenVersusAnim()
    {
        _versus.SetActive(true);
        yield return new WaitForSeconds(0.1f);
    }

    public void CloseVersus()
    {
        _storyButton.SetActive(true);
        _versusButton.SetActive(true);
        StartCoroutine(CloseVersusAnim());
    }
    public IEnumerator CloseVersusAnim()
    {
        _versus.SetActive(false);  
        yield return new WaitForSeconds(0.1f);     
    }


    

    //Open and Close the Selection Book + anim
     public IEnumerator LoadBook(string lore, string choice1, string choice2, string choice3, Sprite loresprite)
    {
        _storyButton.SetActive(false);
        _versusButton.SetActive(false);
        _loreText.text = lore;
        _choice1Text.text = choice1;
        _choice2Text.text = choice2;
        _choice3Text.text = choice3;
        _loreImage.sprite = loresprite;
        yield return new WaitForSeconds(1f);
        _book.SetActive(true);
    }
    
       
    public void CloseBook()
    {
        _versusButton.SetActive(true);
        _storyButton.SetActive(true);
        StartCoroutine(CloseBookAnim());
    }

    public IEnumerator CloseBookAnim()
    {
        _book.SetActive(false);
        yield return new WaitForSeconds(0.1f);     

    }
    

    //Update all Player stats
    public void UpdatePlayerinfo()
    {
        for(int i =0; i<2; i++)
        {
            UpdateCharacter(i);
            UpdateControl(i);
            UpdateRep(i);
        }
    }



    public void UpdateCharacter(int player)
    {
        if(player == 1)
        {
            _player1Name.text = _gameManager.player1Data.PlayerCharacter;
        }
        else
        {
            _player2Name.text = _gameManager.player2Data.PlayerCharacter;
        }
    }

    public void UpdateControl(int player)
    {
        if(player == 1)
        {
            _player1Score.text = "Territoires Contrôlés:\n" + _gameManager.player1Data.PlayerControl.Count;
        }
        else
        {
            _player2Score.text = "Territoires Contrôlés:\n" + _gameManager.player2Data.PlayerControl.Count;
        }
    }

    public void UpdateRep(int player)
    {
      if(player ==1)
        {
            _player1ProgressBar.current = _gameManager.player1Data.PlayerRep;
            if(_gameManager.player1Data.PlayerRep > 50)
            {
                _player1Reput.text = "Influence:\n Positive" ;
            }
            else if(_gameManager.player1Data.PlayerRep == 50)
            {
                _player1Reput.text = "Influence:\n Neutre" ;   
            }
            else
            {
                _player1Reput.text = "Influence:\n Négative" ;
            }
        }
        else
        {
            _player2ProgressBar.current = _gameManager.player2Data.PlayerRep;
           if(_gameManager.player2Data.PlayerRep > 50)
            {
                _player2Reput.text = "Influence:\n Positive" ;
            }
            else if(_gameManager.player2Data.PlayerRep == 50)
            {
                _player2Reput.text = "Influence:\n Neutre" ;   
            }
            else
            {
                _player2Reput.text = "Influence:\n Négative" ;
            }
        }  
    }


    
}
