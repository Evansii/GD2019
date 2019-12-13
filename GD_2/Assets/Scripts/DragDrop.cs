using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
    private Vector3 offset;

    public bool collided = false;
    public Vector3 slotPosition;
    public Vector3 startPosition;
    
    public GameObject otherCard;

    private Cardgame _cardData; 


    void OnMouseDown()
    {
        startPosition = transform.position;
        if (_cardData.GodCard.isMoveable == true)
        {
            offset = gameObject.transform.position -
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        }
    }

    void OnMouseDrag()
    {
        if(_cardData.GodCard.isMoveable == true)
        {
            Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
        }
    }

    void OnMouseUp() 
    {
        if (_cardData.GodCard.isMoveable == true)
        {
            if(collided == true)
            {
                this.gameObject.transform.position = slotPosition;
                _cardData.GodCard.isMoveable = false;
                _cardData.ChangeActiveCard(otherCard ,this.gameObject);
            }   
            else
            {
                transform.position = startPosition;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D obj) 
    {
        Debug.Log("Card in Slot");
        if(obj.gameObject.tag == "CardSlot")
        {
        
            collided = true;
            slotPosition = obj.transform.position;
        }
        if(obj.gameObject.tag == "Card")
        {
            Debug.Log(obj.gameObject + "hit");
            otherCard = obj.gameObject;
        }   
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        collided = false;
    }

    void Start() 
    {
        _cardData = this.gameObject.GetComponent<Cardgame>(); 
    }
    
}
