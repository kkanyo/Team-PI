using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip throwSound;
    public AudioClip weaponSound;
    public AudioClip fireSound;
    public AudioClip iceSound;

    AudioSource myAudio;
    public static SoundManager instance;

    void Awake() {
        if (SoundManager.instance == null)
            SoundManager.instance = this;
    }

	// Use this for initialization
	void Start () {
        myAudio = this.gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaySoundThrow()
    {
        myAudio.PlayOneShot(throwSound);
    }

    public void PlaySoundWeapon()
    {
        myAudio.PlayOneShot(weaponSound);
    }

    public void PlaySoundFire()
    {
        myAudio.PlayOneShot(fireSound);
    }

    public void PlaySoundIce()
    {
        myAudio.PlayOneShot(iceSound);
    }
}
