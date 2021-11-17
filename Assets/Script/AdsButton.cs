using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class AdsButton : MonoBehaviour
{

    public GameObject guideMessage;
    public Text guideMessageText;

    Button m_Button;

    public string placementId = "rewardedVideo";

    void Start()
    {
        m_Button = GetComponent<Button>();
        if (m_Button) m_Button.onClick.AddListener(ShowAd);
    }

    void Update()
    {
        if (m_Button) m_Button.interactable = Advertisement.IsReady(placementId);
    }

    void ShowAd()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        Advertisement.Show(placementId, options);
    }

    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Debug.Log("Video completed - Offer a reward to the player");

            ActiveAdsRewardMessage();
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("Video was skipped - Do NOT reward the player");

        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
        }
    }

    public void ActiveAdsRewardMessage()
    {
        GameController.GetRewardAds();
        guideMessageText.text = "마법의 구슬\n" + "2개 획득!";
        guideMessage.SetActive(true);
    }

    public void UnActiveMessage() { guideMessage.SetActive(false); }
}