using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffectDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Process());
    }
	
	// Update is called once per frame
	void Update () {
    }

    IEnumerator Process()
    {
        yield return new WaitForSeconds(0.35f);
        Destroy(gameObject);
    }
}
