using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldData 
{

    public int currentTurn = 1;

    public int playerTurn = 1;

    public Dictionary<string, CaseData> caseList = new Dictionary<string, CaseData>();

}
