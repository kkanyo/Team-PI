using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBackground : MonoBehaviour {

    public GameObject chapter1;
    public GameObject chapter2;
    public GameObject chapter3;

    // Use this for initialization
    void Start () {
        chapter1.SetActive(false);
        chapter2.SetActive(false);
        chapter3.SetActive(false);

        if (GameController.monsterIndex < 5)
            chapter1.SetActive(true);
        else if (GameController.monsterIndex < 10)
            chapter2.SetActive(true);
        else
            chapter3.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
