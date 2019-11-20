using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelGeneration : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _tiles;

    // Start is called before the first frame update
    void Start()
    {
        int rand=Random.Range(0, _tiles.Length);
        Instantiate(_tiles[rand], transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
