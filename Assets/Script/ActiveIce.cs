using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class ActiveIce : MonoBehaviour
{

    public GameObject guideMessage;
    public Text guideMessageText;

    Button m_Button;

    // Use this for initialization
    void Start()
    {
        m_Button = GetComponent<Button>();
        if (m_Button) m_Button.onClick.AddListener(ActiveIceSkill);

        if (GameController.iceSkillOn == true)
            m_Button.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActiveIceSkill()
    {
        if (GameController.marble >= 4)
        {
            GameController.marble = GameController.marble - 4;
            GameController.iceSkillOn = true;
            m_Button.interactable = false;
            guideMessageText.text = "얼어 스킬 획득!";
            guideMessage.SetActive(true);
        }
        else
        {
            guideMessageText.text = "광고 시청이\n" + "필요합니다.";
            guideMessage.SetActive(true);
        }
    }
}
