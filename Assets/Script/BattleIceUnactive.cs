using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleIceUnactive : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        test();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void test()
    {
        if (GameController.iceSkillOn == true)
            this.gameObject.SetActive(false);
        else
            this.gameObject.SetActive(true);
    }
}
