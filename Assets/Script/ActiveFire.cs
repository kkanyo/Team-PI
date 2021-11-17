using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class ActiveFire : MonoBehaviour {

    public GameObject guideMessage;
    public Text guideMessageText;

    Button m_Button;

    // Use this for initialization
    void Start () {
        m_Button = GetComponent<Button>();
        if (m_Button) m_Button.onClick.AddListener(ActiveFireSkill);

        if (GameController.fireSkillOn == true)
            m_Button.interactable = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActiveFireSkill()
    {
        if (GameController.marble >= 2)
        {
            GameController.marble = GameController.marble - 2;
            GameController.fireSkillOn = true;
            m_Button.interactable = false;
            guideMessageText.text = "불타 스킬 획득!";
            guideMessage.SetActive(true);
        }
        else
        {
            guideMessageText.text = "광고 시청이\n" + "필요합니다.";
            guideMessage.SetActive(true);
        }
    }
}
