using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop_Card : MonoBehaviour
{
    private Vector3 offset;

    [System.NonSerialized]
    public bool collided = false;
    [System.NonSerialized]
    public Vector3 slotPosition;
    [System.NonSerialized]
    public Vector3 startPosition;
    
    [System.NonSerialized]
    public GameObject otherCard;

    private GodCardData _cardData; 
    private Cardgame _gameData;


    void OnMouseDown()
    {
        startPosition = transform.position;
        if (_cardData.isMoveable == true)
        {
            offset = gameObject.transform.position -
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        }
    }

    void OnMouseDrag()
    {
        if(_cardData.isMoveable == true)
        {
            Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
        }
    }

    void OnMouseUp() 
    {
        if (_cardData.isMoveable == true)
        {
            if(collided == true)
            {
                this.gameObject.transform.position = slotPosition;
                _cardData.isMoveable = false;
                gameObject.tag = "Card";
                _gameData.ChangeActiveCard(otherCard , this.gameObject);
            }   
            else
            {
                transform.position = startPosition;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D obj) 
    {
        if (_cardData.isMoveable == true)
        {
            Debug.Log("Card in Slot");
            if(obj.gameObject.tag == "CardSlot")
            {
                collided = true;
                slotPosition = obj.transform.position;
            }
            if(obj.gameObject.tag == "Card" || obj.gameObject.tag == "Destroyed")
            {
                Debug.Log(obj.gameObject.name + " hit");
                otherCard = obj.gameObject;
            }  
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        collided = false;
    }

    void Start() 
    {
        _cardData = this.gameObject.GetComponent<GodCardData>(); 
        _gameData = GameObject.Find("CardManager").GetComponent<Cardgame>();
    }
    
}
