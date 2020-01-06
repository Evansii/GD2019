using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Case : MonoBehaviour
{

    public CaseData localCaseData = new CaseData();

    private GameManager _gameManager;

    private UI_Map _ui; 

    private Button _choice1Button;
    private Button _choice2Button;
    private Button _choice3Button;
    private Button _choice4Button;

    // Start is called before the first frame update
    void Start()
    {
        
        localCaseData.caseName = gameObject.name;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _ui = GameObject.Find("UI").GetComponent<UI_Map>();
        _choice1Button = GameObject.Find("Choix1_Image").GetComponent<Button>();
        _choice2Button = GameObject.Find("Choix2_Image").GetComponent<Button>();
        _choice3Button = GameObject.Find("Choix3_Image").GetComponent<Button>();
        _choice4Button = GameObject.Find("Choix4_Image").GetComponent<Button>();
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
            // _gameManager.localWorldData.activeCases.Add(localCaseData);

        }
    }

    public void activateCase()
    {
        localCaseData.isActive = true;

    }

    public void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _choice1Button.onClick.RemoveAllListeners();
            _choice1Button.onClick.AddListener(delegate{_gameManager.GetComponent<Transition>().LoadFromButton(localCaseData.Scene1ToLoad);});

            _choice2Button.onClick.RemoveAllListeners();
            _choice2Button.onClick.AddListener(delegate{_gameManager.GetComponent<Transition>().LoadFromButton(localCaseData.Scene2ToLoad);});

            _choice3Button.onClick.RemoveAllListeners();
            _choice3Button.onClick.AddListener(delegate{_gameManager.GetComponent<Transition>().LoadFromButton(localCaseData.Scene3ToLoad);});

            _choice4Button.onClick.RemoveAllListeners();
            _choice4Button.onClick.AddListener(delegate{_gameManager.GetComponent<Transition>().LoadFromButton(localCaseData.Scene4ToLoad);});

            StartCoroutine(_ui.LoadBook(localCaseData.loreTextToLoad,
            localCaseData.choice1ToLoad,
            localCaseData.choice2ToLoad,
            localCaseData.choice3ToLoad,
            localCaseData.choice4ToLoad,
            localCaseData.spriteToLoad));
        }
    }
    

    


}
