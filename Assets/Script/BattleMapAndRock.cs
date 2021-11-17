using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMapAndRock : MonoBehaviour {

    public GameObject BattleMap;
    public GameObject Rock;

    // Use this for initialization
    void Start () {
        int nowStages = GameController.monsterIndex;
        UpdateMapAndRock(nowStages);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateMapAndRock(int nowStages)
    {
        int nowDays;

        if (nowStages < 2)
            nowDays = 1;
        else if (nowStages < 5)
            nowDays = 3;
        else if (nowStages < 10)
            nowDays = 6;
        else if (nowStages < 14)
            nowDays = 11;
        else
            nowDays = 15;

        BattleMap.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("UI/Background/Battle/Map" + nowDays);
        Rock.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("UI/Background/Battle/Rock" + nowDays);
    }
}
