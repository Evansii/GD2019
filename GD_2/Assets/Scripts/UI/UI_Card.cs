using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_Card : MonoBehaviour
{

    [SerializeField]
    private Text _playerLpText;

    [SerializeField]
    private Text _enemyLpText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlayerLp(int score)
    {
        _playerLpText.text = ""+score;
    }

    public void UpdateEnemyLp(int score)
    {
        _enemyLpText.text = ""+score;
    }
}
