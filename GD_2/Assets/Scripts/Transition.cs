using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    private GameManager _gameManager;
    private Animator _anim;

    [SerializeField]
    private string _sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
       _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();    
      _anim = GameObject.Find("clouds").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0))
        {

            Debug.Log("You are at Turn "+ _gameManager.localWorldData.currentTurn);
            Debug.Log(_gameManager.localWorldData.activeCases[0]);
            Debug.Log("test");
            StartCoroutine(LoadSceneTransition());
        }
    }

    IEnumerator LoadSceneTransition()
    {
        _anim.SetTrigger("go");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(_sceneToLoad);
    }
}

