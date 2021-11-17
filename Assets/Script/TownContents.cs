using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownContents : MonoBehaviour {
	private int contentsIndex;
	private float[,] contentsInfo = 
		{ { 200f, 20f, 20f },
			{300f, 30f, 30f },
			{400f, 40f, 40f},
			{0, 0, 0},
            {0, 0, 0},
            {0, 0, 0}
        }; 

	public Button Sleep;		public GameObject UnActive1;
	public Button EatFood;		public GameObject UnActive5;
	public Button DrinkBear;	public GameObject UnActive10;
	public Button Dating;		public GameObject UnActive11;

	public Slider patienceBar;
	public Slider sincerityBar;

	public GameObject[] PopUps;
	public GameObject guideMessage;
	public Text guideMessageText;

	private bool OnClickButton;
	// Use this for initialization
	void Start () {
		OpenContents();

		OnClickButton = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ActiveContents(int contentsIndex)
	{
		if (OnClickButton && GameController.moneyOnHand >= contentsInfo[contentsIndex, 0])
		{
			OnClickButton = false;
			this.contentsIndex = contentsIndex;

            if(contentsIndex == 3)
            {
                contentsIndex += GameController.choice;
            } 
            GameObject popUp = (GameObject)Instantiate(PopUps[contentsIndex]);
			popUp.transform.parent = transform;
			popUp.transform.localPosition = new Vector3(0, 0, 0);

			GameController.moneyOnHand = GameController.moneyOnHand - (int)contentsInfo[contentsIndex, 0];
			patienceBar.value = Mathf.MoveTowards(patienceBar.value, 100, contentsInfo[contentsIndex, 1]);
			sincerityBar.value = Mathf.MoveTowards(sincerityBar.value, 100, -1 * contentsInfo[contentsIndex, 2]);

			Invoke("ActiveMessage", 2f);
			Destroy(popUp, 2f);
		}
		else if (OnClickButton && GameController.moneyOnHand < contentsInfo[contentsIndex, 0])
		{
			OnClickButton = false;
			guideMessageText.text = "소지금이 부족합니다.";
			guideMessage.SetActive(true);
		}
		else if (OnClickButton && patienceBar.value >= patienceBar.maxValue && sincerityBar.value >= sincerityBar.maxValue)
		{
			OnClickButton = false;
			guideMessageText.text = "인내심과 성실함이 가득 찬 상태입니다.";
			guideMessage.SetActive(true);
		}
	}

	public void OpenContents() {
		int monsterIndex = GameController.monsterIndex;

		if (monsterIndex >= 0) {
			Sleep.interactable = true;
			UnActive1.SetActive(false);
		}
		if (monsterIndex >= 5) {
			EatFood.interactable = true;
			UnActive5.SetActive(false);
		}
		if (monsterIndex >= 8) {
			DrinkBear.interactable = true;
			UnActive10.SetActive(false);
		}
        if (monsterIndex >= 10)
        {
            Dating.interactable = true;
            UnActive11.SetActive(false);
        }
    }

	public void OnClickedSleep() { OnClickButton = true; ActiveContents(0); }
	public void OnClickedDinner() { OnClickButton = true; ActiveContents(1); }
	public void OnClickedDrinkBear() { OnClickButton = true; ActiveContents(2);}
	public void OnClickedDating() { OnClickButton = true; ActiveContents(3); }

    public void ActiveMessage()
	{
		guideMessageText.text = "돈 -" + (int)contentsInfo[contentsIndex, 0]
						+ "\n인내심 +" + (int)contentsInfo[contentsIndex, 1]
						+ "\n성실함 -" + (int)contentsInfo[contentsIndex, 2];
		guideMessage.SetActive(true);
	}

    public void UnActiveMessage() { guideMessage.SetActive(false); }

}