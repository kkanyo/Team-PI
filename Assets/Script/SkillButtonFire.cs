using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButtonFire : MonoBehaviour {

    public Button SkillButton;

    //private Sprite idleSprite;
    //private Sprite activeSprite;

    // Use this for initialization
    void Start () {
        Button btn = SkillButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        //idleSprite = Resources.Load<Sprite>("UI/Buttons/FireUI");
        //activeSprite = Resources.Load<Sprite>("UI/Buttons/FireActiveUI");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void TaskOnClick()
    {
        if (GameController.skillLock == 0 && GameController.fireSkillOn == true)
        {
            GameController.SKILL = 1;
            GameController.skillLock = 1;
            StartCoroutine(Process());
        }
    }

    IEnumerator Process()
    {
        //SkillButton.GetComponent<Image>().sprite = activeSprite;

        yield return new WaitForSeconds(3f);

        GameController.SKILL = 0;
        //SkillButton.GetComponent<Image>().sprite = idleSprite;
        GameController.skillLock = 0;
    }

}
