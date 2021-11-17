using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Serialization
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour{
	public const int numMonster = 15;
	public Slider patienceBar;
	public Slider sincerityBar;
	public static int weaponIndex;
	public static int damageWeapon;
	public static int monsterIndex;
	public static int moneyOnHand;
	public static int marble;

    public static int choice;
    public static int loopTimes;

	public Slider monsterHPBar;
	public static int[] reward
	 = { 600, 700, 800, 900, 1000,
        1050, 1100, 1150, 1200, 1500,
        1600, 1700, 1800, 2000, 5000};
	public static int[] damageWeaponList
	 = { 1, 2, 4, 6, 8, 10, 15 };
	public static int[] costUpgrade
	 = { 100, 400, 1000, 1500, 2000, 2500 };
	public static float[] monsterHPList
	 = { 35, 70, 80, 140, 180,
        270, 300, 360, 440, 530,
        600, 700, 800, 1100, 1300};
	public static bool gameStart = false;
	public static bool isGameOver = false;
	public static bool isFirstGame = false;

    public static bool fireSkillOn = false;
    public static bool iceSkillOn = false;
    public static int SKILL = 0;
    public static int skillLock = 0;

    [Serializable]
	public class PlayerData
	{
		public float patience;
		public float sincerity;
		public int weaponIndex;
		public int damageWeapon;
		public int monsterIndex;
		public int moneyOnHand;
		public int marble;

        public int choice;
        public int loopTimes;

        public bool fireSkillOn;
        public bool iceSkillOn;

		public bool isFirstGame;
	}

	// Use this for initialization
	void Start () {
		if (gameStart == true)
			LoadData();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void InitData()
	{
		string _Filestr = Application.persistentDataPath + "/playerInfo.data";
		FileInfo f = new FileInfo(_Filestr);

		if (!f.Exists || isGameOver) {
			isFirstGame = true;
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(_Filestr);

			PlayerData data = new PlayerData();

			data.patience = 50f;
			data.sincerity = 50f;
			data.weaponIndex = 0;
			data.damageWeapon = 1;
			data.monsterIndex = 0;
			data.moneyOnHand = 100;
			data.marble = 0;

            data.choice = 0;
            data.loopTimes = 0;

            data.fireSkillOn = false;
            data.iceSkillOn = false;

            data.isFirstGame = true;

			bf.Serialize(file, data);
			file.Close();

			isGameOver = false;
		}
	}

	public void SaveData()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.data");

		PlayerData data = new PlayerData();

		data.patience = patienceBar.value;
		data.sincerity = sincerityBar.value;
		data.weaponIndex = weaponIndex;
		data.damageWeapon = damageWeapon;
		data.monsterIndex = monsterIndex;
		data.moneyOnHand = moneyOnHand;
		data.marble = marble;

        data.choice = choice;
        data.loopTimes = loopTimes;

        data.fireSkillOn = fireSkillOn;
        data.iceSkillOn = iceSkillOn;

        data.isFirstGame = isFirstGame;
		
		bf.Serialize(file, data);
		file.Close();
	}

	public void LoadData()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.data", FileMode.Open);

        if (file != null && file.Length > 0) {
			PlayerData data = (PlayerData)bf.Deserialize(file);

			patienceBar.value = data.patience;
			sincerityBar.value = data.sincerity;
			weaponIndex = data.weaponIndex;
			damageWeapon = data.damageWeapon;
			monsterIndex = data.monsterIndex;
			moneyOnHand = data.moneyOnHand;
			marble = data.marble;

            choice = data.choice;
            loopTimes = data.loopTimes;

            fireSkillOn = data.fireSkillOn;
            iceSkillOn = data.iceSkillOn;

            isFirstGame = data.isFirstGame;
		}

		file.Close();
	}

	public static void GetRewardBattle()
	{
        moneyOnHand += reward[monsterIndex];
    }

    public static void GetRewardAds()
    {
        marble += 2;
    }

    public static void SetCoice(int select)
    {
        choice = select;
    }

    public static void increaseLoopTimes()
    {
        loopTimes += 1;
    }

    public void OnClickStartButton() { gameStart = true; isGameOver = false; }
}
