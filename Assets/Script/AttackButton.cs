using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AttackButton: MonoBehaviour
{
    public Object weaponPrefab;
    private Sprite[] allDayWeapon;

    public GameObject shootingPoint;
    public Button attackButton;

    public Slider patienceBar;
    public Slider sincerityBar;
    
    // Unit of increasing/decreasing
    private float decreasePatience = 1f;
    private float increaseSincerity = 1f;

    // Use this for initialization
    void Start()
    {
        Button btn = attackButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

        // Load Image of Weapon, then make list
        allDayWeapon = Resources.LoadAll<Sprite>("UI/DayWeapon");
        // Get level of weapon upgraded
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TaskOnClick()
    {
        int weaponIndex = GameController.weaponIndex;

        GameObject weapon = (GameObject)Instantiate(
                    weaponPrefab,
                    shootingPoint.transform.position,
                    Quaternion.identity
                );

        // Change Sprite of weaponPrefab
        switch (GameController.SKILL) {
            case 0:
                weapon.GetComponent<SpriteRenderer>().sprite = (Sprite)allDayWeapon.Single(s => s.name == "W" + GameController.weaponIndex);
                break;
            case 1:
                weapon.GetComponent<SpriteRenderer>().sprite = (Sprite)allDayWeapon.Single(s => s.name == "F" + GameController.weaponIndex);
                break;
            case 2:
                weapon.GetComponent<SpriteRenderer>().sprite = (Sprite)allDayWeapon.Single(s => s.name == "I" + GameController.weaponIndex);
                break;
        }

        SoundManager.instance.PlaySoundThrow();

        // Decrease amount of patience
        patienceBar.value = Mathf.MoveTowards(patienceBar.value, 100, -1 * decreasePatience);
        // Increase amount of sincerity
        sincerityBar.value = Mathf.MoveTowards(sincerityBar.value, 100, increaseSincerity);
    }
}
