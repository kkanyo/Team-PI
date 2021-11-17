using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAttack : MonoBehaviour {
	private float done = 30.0f;
    
	public Text timer;
	public GameObject LoseBattlePop;
	// Use this for initialization
	void Start () {
		LoseBattlePop.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (UIController.stopTimer == false)
        {
            if (done > 0f)
            {
                if (done < 11f)
                {
                    timer.color = Color.red;
                }
                done -= Time.deltaTime;
                timer.text = "0:" + (int)done;
            }
            else
            {
                LoseBattlePop.SetActive(true);
            }
        }
	}
}
