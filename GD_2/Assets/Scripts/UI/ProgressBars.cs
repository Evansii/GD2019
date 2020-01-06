using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBars : MonoBehaviour
{
    public int min;
    public int max;
    public int current;

    public int evilRep;



    public Image mask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    //Control the fill meter of the bar
    public void GetCurrentFill()
    {
        float currentOffset = current - min;
        float maxOffset = max - min;
        float fillAmount = currentOffset / maxOffset;
        mask.fillAmount = fillAmount;
    }

    //Return the evil reputation in number
    public int GetEvilRep()
    {
        int rep = max - current;
        return rep;
    }

    //Return the good reputation in number
    public int GetGoodRep()
    {
        int rep = current;
        return rep;
    }
}
