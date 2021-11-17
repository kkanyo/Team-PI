using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSkillEffectDestroy : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Process());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Process()
    {
        if(GameController.SKILL == 1)
        {
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        }
        else
        {
            GameController.SKILL = 0;
            yield return new WaitForSeconds(1.5f);
            Destroy(gameObject);
        }
    }
}
