using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public string PlayerName;

    public string PlayerCharacter;

    public int PlayerRep;

    public List<GameObject> PlayerDeck = new List<GameObject>();

    public List<GameObject> PlayerControl= new List<GameObject>();

}
