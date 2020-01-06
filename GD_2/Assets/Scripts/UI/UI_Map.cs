using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Map : MonoBehaviour
{

    [System.NonSerialized]
    public GameObject _book;

    private GameObject _versus;

    [System.NonSerialized]
    public Text _loreText;
    [System.NonSerialized]
    public Text _choice1Text;
    [System.NonSerialized]
    public Text _choice2Text;
    [System.NonSerialized]
    public Text _choice3Text;
    [System.NonSerialized]
    public Text _choice4Text;

    [System.NonSerialized]
    public Image _loreImage;


    // Start is called before the first frame update
    void Start()
    {
        _book = GameObject.Find("Book");
        _versus = GameObject.Find("Versus_Screen");
         _loreText = GameObject.Find("Lore_Text").GetComponent<Text>();
         _loreImage = GameObject.Find("Lore_Image").GetComponent<Image>();
        _choice1Text = GameObject.Find("Choix1_Text").GetComponent<Text>();
        _choice2Text = GameObject.Find("Choix2_Text").GetComponent<Text>();
        _choice3Text = GameObject.Find("Choix3_Text").GetComponent<Text>();
        _choice4Text = GameObject.Find("Choix4_Text").GetComponent<Text>();

        _versus.SetActive(false);
        _book.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public IEnumerator LoadBook(string lore, string choice1, string choice2, string choice3, string choice4, Sprite loresprite)
    {
        _loreText.text = lore;
        _choice1Text.text = choice1;
        _choice2Text.text = choice2;
        _choice3Text.text = choice3;
        _choice4Text.text = choice4;
        _loreImage.sprite = loresprite;
        yield return new WaitForSeconds(1f);
        _book.SetActive(true);
    }

    public IEnumerator CloseVersusAnim()
    {
        _versus.SetActive(false);  
        yield return new WaitForSeconds(0.1f);     
    }

    public IEnumerator CloseBookAnim()
    {
        _book.SetActive(false);
        yield return new WaitForSeconds(0.1f);     

    }



    public void CloseVersus()
    {
        StartCoroutine(CloseVersusAnim());
    }


    public void CloseBook()
    {
        StartCoroutine(CloseBookAnim());
    }


    public void OpenVersus()
    {
        StartCoroutine(OpenVersusAnim());
    }

    public IEnumerator OpenVersusAnim()
    {
        _versus.SetActive(true);
        yield return new WaitForSeconds(0.1f);
    }

    
}
