using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {  
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
        Debug.Log("test");
        StartCoroutine(LoadSceneTransition());
        }
    }

    IEnumerator LoadSceneTransition()
    {
        _anim.SetTrigger("go");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Transi_test");
    }
}

