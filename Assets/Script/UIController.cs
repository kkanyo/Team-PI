using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIController : MonoBehaviour {
	
	public GameObject gameOverPop;
    public GameObject gameOverSelectPop;
    public GameObject winBattlePop;
	public GameObject giveUpPop;
	public GameObject toonBack;
	public GameObject book;
    public GameObject days;
    public Slider patienceBar;
	public Slider sincerityBar;
	public Slider monsterHPBar;
	public Text moneyText;
	public Text rewardText;
    public Text marbleText;
	//Stage Progress
	private int nowStages;
	public GameObject[] battleButtons;
	public GameObject[] progress;

    public GameObject choice;
    public GameObject guideMessage;
    public Text guideMessageText;

    Animator animator;
    public static bool stopTimer = false;
    public static bool backToTown = false;

    // Use this for initialization
    void Start () {
		/*gameOverPop.SetActive(false);
		winBattlePop.SetActive(false);
		giveUpPop.SetActive(false);*/

		UpdateMoneyText();
        UpdateMarbleText();
        UpdateDays();
        nowStages = GameController.monsterIndex;

        if (backToTown == true)
            StartCoroutine(BackToTownCoroutine());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateMoneyText()
	{
		moneyText.text = GameController.moneyOnHand.ToString();
	}

    public void UpdateMarbleText()
    {
        marbleText.text = GameController.marble.ToString();
    }

    void UpdateDays()
    {
        int nowDays = GameController.monsterIndex;
        Sprite[] allDayWeapon = Resources.LoadAll<Sprite>("UI/DayWeapon");
        Sprite daysText = allDayWeapon.Single(s => s.name == "D" + nowDays);

        SpriteRenderer SR = days.GetComponent<SpriteRenderer>();
        SR.sprite = daysText;
    }

    public void ActiveWinBattle()
	{
        if (monsterHPBar.value <= 0) {
            StartCoroutine(ActiveWinBattleCoroutine());
        }
	}
	public void NonActiveWinBattle() { winBattlePop.SetActive(false); }

    public void ActiveGameOver()
	{
		if (patienceBar.value <= 0 || sincerityBar.value <= 0) {
			gameOverPop.SetActive(true);
			GameController.isGameOver = true;
			GameController.gameStart = false;
		}
	}
	public void NonActiveGameOver() { gameOverPop.SetActive(false); }

    public void ActiveGameOverSelect()
    {
        gameOverSelectPop.SetActive(true);
        GameController.isGameOver = true;
        GameController.gameStart = false;
    }
    public void NonActiveGameOverSelect()
    {
        gameOverSelectPop.SetActive(false);
        GameController.isGameOver = false;
        GameController.gameStart = true;
    }

    public void ActiveGiveUp() { giveUpPop.SetActive(true); }
	public void NonActiveGiveUp() { giveUpPop.SetActive(false); }

	public void ActiveBook() { book.SetActive(true); }
	public void UnActiveBook() { book.SetActive(false); }

	public void ActiveStartToon() { 
		//Debug.Log(GameController.isFirstGame);
		if (GameController.isFirstGame == true)
			toonBack.SetActive(true);
		GameController.isFirstGame = false;
		//Debug.Log(GameController.isFirstGame);
	}

	IEnumerator ActiveWinBattleCoroutine()
    {
		int monsterIndex = GameController.monsterIndex;
        /*
        Rigidbody2D monsterR = GameObject.FindGameObjectWithTag("Monster").GetComponent<Rigidbody2D>();
        Transform monsterT = GameObject.FindGameObjectWithTag("Monster").GetComponent<Transform>();
        monsterR.AddForce(monsterT.up * 1500f, ForceMode2D.Impulse);
        monsterR.AddForce(-monsterT.right * 1400f, ForceMode2D.Impulse);
        */
        stopTimer = true;
        GameObject monster = GameObject.FindGameObjectWithTag("Monster");
        monster.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        monster.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        monster.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        Outrage.ANI = false;

        backToTown = true;

        toonBack.SetActive(true);
        animator = gameObject.GetComponentInChildren<Animator>(toonBack);
        if (monsterIndex < 14)
        {
            animator.runtimeAnimatorController = Resources.Load("Toons/Toon" + monsterIndex + "0") as RuntimeAnimatorController;
            while (animator.runtimeAnimatorController != null && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                yield return new WaitForSeconds(2f);
            }
            toonBack.SetActive(false);

            GameController.GetRewardBattle();
            rewardText.text = "승리!\n" +
                "금화 + " + GameController.reward[monsterIndex];
            if (monsterIndex < GameController.numMonster) { GameController.monsterIndex++; }
            winBattlePop.SetActive(true);
        }

        else if(GameController.loopTimes < 2)
        {
            int ending;
            if (GameController.choice == 1)
                ending = 1;
            else
                ending = 0;
            animator.runtimeAnimatorController = Resources.Load("Toons/Toon" + monsterIndex + ending) as RuntimeAnimatorController;
            while (animator.runtimeAnimatorController != null && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                yield return new WaitForSeconds(2f);
            }
            toonBack.SetActive(false);

            GameController.GetRewardBattle();
            rewardText.text = "마내구는 3가지 엔딩이 있습니다.";
            GameController.loopTimes += 1;
            GameController.monsterIndex = 10;
            winBattlePop.SetActive(true);
        }
        else
        {
            animator.runtimeAnimatorController = Resources.Load("Toons/Toon" + monsterIndex + 2) as RuntimeAnimatorController;
            while (animator.runtimeAnimatorController != null && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                yield return new WaitForSeconds(2f);
            }
            toonBack.SetActive(false);

            GameController.GetRewardBattle();
            rewardText.text = "지금까지 마내구를 즐겨주셔서 감사합니다.";
            winBattlePop.SetActive(true);
        }
    }
    IEnumerator BackToTownCoroutine()
    {
        toonBack.SetActive(true);
        animator = gameObject.GetComponentInChildren<Animator>(toonBack);
        animator.runtimeAnimatorController = Resources.Load("Toons/Toon" + (GameController.monsterIndex - 1) + "1") as RuntimeAnimatorController;
        while (animator.runtimeAnimatorController != null && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return new WaitForSeconds(2f);
        }

        stopTimer = false;

        toonBack.SetActive(false);
        backToTown = false;

        if (GameController.monsterIndex == 10)
        {
            guideMessageText.text = "나도\n 연애할 수 있다!";
            guideMessage.SetActive(true);
            yield return new WaitForSeconds(2f);

            guideMessage.SetActive(false);
            choice.SetActive(true);
        }
    }

	public void StageProgress()
	{
        int rotateNowStages = (nowStages % 5);
		battleButtons[rotateNowStages].SetActive(true);

		for (int i = 0; i <= rotateNowStages; i++)
		{
			progress[i].SetActive(true);
		}
	}

    public void OnClickedW0() { GameController.SetCoice(0); choice.SetActive(false); }
    public void OnClickedW1() { GameController.SetCoice(1); choice.SetActive(false); }
    public void OnClickedW2() { GameController.SetCoice(2); choice.SetActive(false); }

}
