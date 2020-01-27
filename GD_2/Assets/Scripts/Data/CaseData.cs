
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CaseData
{
    public string caseName;

    public bool isCorrupted = false;

    public string caseControl = "neutral";

    public bool isActive = false;

    public Text loreTextToLoad;

    public string choice1ToLoad;
    public string choice2ToLoad;
    public string choice3ToLoad;

    public string Scene1ToLoad;
    public string Scene2ToLoad;
    public string Scene3ToLoad;

    public List<GameObject> riverDeck = new List<GameObject>();

    public List<GameObject> enemyDeck = new List<GameObject>();

    public Sprite spriteToLoad;

    public Sprite CardBackground;

}