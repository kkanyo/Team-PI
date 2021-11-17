using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public GameObject t1;
    public GameObject toonBack;
    public GameObject startText;
    public Text text;

    // Use this for initialization
    void Start () {
        StartCoroutine(StartTextCoroutine());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void t1Active()
    {
        t1.SetActive(true);
    }

    public void t1InActive()
    {
        t1.SetActive(false);
    }

    public void toonBackInActive()
    {
        toonBack.SetActive(false);
    }

    IEnumerator StartTextCoroutine()
    {
        while(true)
        {
            startText.SetActive(false);
            yield return new WaitForSeconds(0.5f);

            text.text = "클릭하여 자동저장";
            startText.SetActive(true);
            yield return new WaitForSeconds(0.5f);

            startText.SetActive(false);
            yield return new WaitForSeconds(0.5f);

            text.text = "클릭하여 시작하기";
            startText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
