using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UpgradeWeapon : MonoBehaviour {
	public GameObject currentWeapon;
    public GameObject pfUpgradeBg;
	public GameObject pfWeaponFrom;
	public GameObject pfWeaponTo;
	public GameObject guideMessage;
	public Text upgradeCostText;
	public Text guideMessageText;

	private Sprite[] allDayWeapon;
	private int maxNumWeapon = 7;
	private bool ActiveUpgrade = false;

    // Use this for initialization
    void Start () {
        allDayWeapon = Resources.LoadAll<Sprite>("UI/DayWeapon");
        Sprite weapon = allDayWeapon.Single(s => s.name == "W" + GameController.weaponIndex);

        currentWeapon.GetComponent<SpriteRenderer>().sprite = weapon;
		upgradeCostText.text = GameController.costUpgrade[GameController.weaponIndex].ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClickUpgradeButton()
	{
		int weaponIndex = GameController.weaponIndex;
		int moneyOnHand = GameController.moneyOnHand;
		int[] costUpgrade = GameController.costUpgrade;
		int[] damageWeaponList = GameController.damageWeaponList;

		if (ActiveUpgrade == false && moneyOnHand >= costUpgrade[weaponIndex] && weaponIndex < maxNumWeapon - 1)
		{
			ActiveUpgrade = true;
			
			moneyOnHand = moneyOnHand - costUpgrade[weaponIndex];
			//Increase level of weapon
			weaponIndex += 1;
			GameController.damageWeapon = damageWeaponList[weaponIndex];

			GameObject upgradeBg = LoadObject(pfUpgradeBg, -7, 0);
			//Call GameObject of pre-weapon and next level weapon, and they move.
			GameObject upWeaponFrom = LoadObject(pfWeaponFrom, 0, 0, (Sprite)allDayWeapon.Single(s => s.name == "W" + (weaponIndex - 1)));
			GameObject upWeaponTo = LoadObject(pfWeaponTo, 900, 0, (Sprite)allDayWeapon.Single(s => s.name == "W" + weaponIndex));
			
			//Destroy GameObject
			Destroy(upgradeBg, 1);
			Destroy(upWeaponFrom, 1);
			Destroy(upWeaponTo, 1);

			//Update Value
			GameController.weaponIndex = weaponIndex;
			GameController.moneyOnHand = moneyOnHand;

			//After 1 seconds, Image of currentWeapon is changed
			Invoke("ChangeWeapon", 1f);
		}
		else if (weaponIndex >= maxNumWeapon - 1)
		{
			guideMessageText.text = "더 이상 강화할 무기가 없습니다.";
			guideMessage.SetActive(true);
		}
		else if (moneyOnHand < costUpgrade[weaponIndex])
		{
			guideMessageText.text = "소지금이 부족합니다.";
			guideMessage.SetActive(true);
		}
	}

	GameObject LoadObject(GameObject prefab, int posX, int posY)
	{
		GameObject obj = GameObject.Instantiate(prefab) as GameObject;
        obj.transform.parent = transform;
		obj.transform.localScale = new Vector3(1f, 1f, 1f);
        obj.transform.localPosition = new Vector3(posX, posY, 0);
		return obj;
	}

	/*Override LoadObject*/
	GameObject LoadObject(GameObject prefab, int posX, int posY, Sprite image)
	{
		GameObject obj = GameObject.Instantiate(prefab) as GameObject;
		obj.GetComponent<SpriteRenderer>().sprite = image;
        obj.transform.parent = transform;
		obj.transform.localScale = new Vector3(2f, 2f, 2f);
        obj.transform.localPosition = new Vector3(posX, posY, 0);
 
		return obj;
	}

	public void ChangeWeapon()
	{
		currentWeapon.GetComponent<SpriteRenderer>().sprite = (Sprite)allDayWeapon.Single(s => s.name == "W" + GameController.weaponIndex);
		upgradeCostText.text = GameController.costUpgrade[GameController.weaponIndex].ToString();
		ActiveUpgrade = false;
	}

	public void NonActiveGuideMessage() { guideMessage.SetActive(false); }
}
