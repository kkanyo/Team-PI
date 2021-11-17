using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Outrage : MonoBehaviour
{
    public static float speed = 1f;
    private float fixedSpeed;

    private float moveX = 9.2f;
    private float moveY = 3.2f;
    private float scaleIncrease = 0.03f;

    public static bool ANI = false;

    public static bool freezeMonster = false;

    public static bool monsterSkill = false;

    public Slider monsterHPBar;
    public float[] monsterSkillLifesteal = new float[14]
    {
        0.5f, 0.6f, 0.7f, 0.8f, 1f,
        1f, 1.1f, 1.2f, 1.3f, 1.5f,
        1.5f, 1.6f, 1.7f, 2f
    };
    private float lifestealAmount;
    public int[] monsterSkillRate = new int[15]
    {
        5, 5, 5, 5, 6,
        6, 6, 6, 6, 7,
        7, 7, 7, 8, 9
    };
    private float skillRateAmount;

    public float rotationScale = 0.5f;
    public float screenQuake = 1f;

    private int[,] monsterAction = new int[,]
    {
        // Action times
        // 4 4 4 5 6
        // 6 6 7 7 8
        // 8 8 9 10 12

        // index 0 4
        {
            1,1,1,-1,-1,
            1,1,1,0,-1,
            1,0,1,1,1,
            1,1,1,0,1,
            0,-1,1,1,1,
            1,1,1,1,1
        },
        // index 1 4
        {
            1,0,0,-1,-1,
            1,1,1,0,-1,
            1,1,1,1,1,
            1,1,1,0,1,
            -1,-1,1,1,1,
            1,1,1,1,1
        },
        // index 2 4
        {
            1,1,0,-1,-1,
            1,1,0,-1,-1,
            1,1,1,1,1,
            1,0,1,1,1,
            1,1,1,1,0,
            1,-1,1,1,1
        },
        // index 3 5
        {
            1,1,1,-1,-1,
            1,1,1,-1,0,
            1,0,1,1,1,
            1,-1,1,0,1,
            0,1,1,1,0,
            1,1,1,1,1
        },
        // index 4 6
        {
            1,1,0,1,-1,
            1,1,0,0,0,
            1,1,-1,-1,1,
            1,0,1,1,1,
            1,1,1,1,-1,
            1,1,0,1,1
        },
        // index 5 6
        {
            0,0,1,-1,-1,
            1,1,1,1,-1,
            0,1,1,1,1,
            1,0,1,0,1,
            1,-1,1,1,0,
            1,1,1,1,1
        },
        // index 6 6
        {
            1,1,1,-1,-1,
            0,1,0,1,0,
            1,0,1,0,1,
            1,1,0,1,1,
            -1,1,1,1,-1,
            1,1,-1,1,1
        },
        // index 7 7
        {
            1,1,1,-1,-1,
            1,1,-1,1,1,
            1,1,0,1,1,
            1,1,0,1,1,
            -1,0,1,0,-1,
            0,1,0,1,0
        },
        // index 8 7
        {
            0,1,1,1,1,
            1,0,1,1,1,
            0,1,0,1,1,
            1,0,-1,1,-1,
            0,1,-1,1,1,
            0,1,1,1,1
        },
        // index 9 8
        {
            1,0,0,-1,-1,
            1,1,0,-1,-1,
            1,1,0,1,0,
            1,0,1,1,0,
            1,1,1,1,1,
            1,1,1,0,1
        },
        // index 10 8
        {
            1,-1,1,-1,-1,
            1,0,1,1,0,
            1,0,1,1,1,
            1,0,0,0,0,
            -1,1,1,1,0,
            1,1,1,1,1
        },
        // index 11 8
        {
            1,1,-1,0,0,
            1,1,-1,0,0,
            1,1,-1,0,0,
            1,1,-1,0,0,
            1,1,-1,1,1,
            1,1,-1,1,1
        },
        // index 12 9
        {
            1,1,1,1,0,
            1,1,0,1,-1,
            0,1,0,-1,1,
            1,-1,1,0,0,
            0,0,0,-1,1,
            1,1,1,1,1
        },
        // index 13 10
        {
            1,1,1,-1,-1,
            1,1,0,0,0,
            1,0,1,1,1,
            1,0,1,0,1,
            0,0,-1,1,0,
            1,1,0,1,1
        },
        // index 14 12
        {
            0,0,1,1,1,
            1,1,0,1,1,
            1,0,1,1,1,
            1,1,-1,0,0,
            -1,0,0,-1,0,
            0,-1,0,0,1
        }
    };

    //private SpriteRenderer spriteRenderer;
    //private string monsterSprite;

    //public float frontMoveFormardY = 30f;
    //public float frontRageScaleI = 0.2f;

    //public float moveForwardY = 3f;
    //public float rageScaleIncrease = 0.02f;

    // direction: 0 front, 1 remaining
    //private int direction = 0;
    //private float moveFormardX;
    //private float frontMoveFormardX;

    // Use this for initialization
    void Start()
    {
        GameController.SKILL = 0;
        GameController.skillLock = 0;

        monsterHPBar.maxValue = GameController.monsterHPList[GameController.monsterIndex];
        monsterHPBar.value = monsterHPBar.maxValue;

        //animator = transform.Find("Monster" + (GameController.monsterIndex+1) +"Top").gameObject.GetComponent<Animator>();
        int bugFixMIndex;
        if (GameController.monsterIndex == 14)
            bugFixMIndex = 13;
        else bugFixMIndex = GameController.monsterIndex;

        lifestealAmount = GameController.damageWeapon * monsterSkillLifesteal[bugFixMIndex];
        skillRateAmount = monsterSkillRate[GameController.monsterIndex];

        StartCoroutine(SpeedControl());

        //moveFormardX = moveForwardY * 2;
        //frontMoveFormardX = frontMoveFormardY * 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (freezeMonster == false)
            MonsterMove(speed);

        /*
        monsterSprite = GetComponent<SpriteRenderer>().sprite.texture.name;
        if (monsterSprite.EndsWith("F") == true)
            direction = 0;
        else
            direction = 1;
        */
    }

    void InitMonster()
    {
        transform.position = new Vector3(340, 1400, 0);
        transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }

    void MonsterMove(float speed)
    {
            fixedSpeed = speed * Time.deltaTime;
            transform.localScale += new Vector3(scaleIncrease * fixedSpeed, scaleIncrease * fixedSpeed, 0);
            transform.position += new Vector3(moveX * fixedSpeed, -moveY * fixedSpeed, 0);
    }
    // Random.Range(0, 10) < (GameController.monsterIndex + 2)
    void MonsterHPDecrease()
    {
        // Action
        if(monsterSkill)
            monsterHPBar.value = Mathf.MoveTowards(monsterHPBar.value, monsterHPBar.maxValue, lifestealAmount);
        // Normal
        else
            monsterHPBar.value = Mathf.MoveTowards(monsterHPBar.value, monsterHPBar.maxValue, -1 * GameController.damageWeapon);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Weapon")
        {
            StartCoroutine(BasicCoroutine());
        }
        if (col.gameObject.tag == "Weapon" && GameController.SKILL == 2)
        {
            StartCoroutine(IceCoroutine());
        }

        MonsterHPDecrease();

    }

    IEnumerator BasicCoroutine()
    {
        ANI = true;

        transform.Rotate(0, 0, rotationScale);
        //spriteRenderer.color = new Color(255, 50, 50, 255);
        GameObject.FindGameObjectWithTag("BattleScreen").transform.position += new Vector3(0, -screenQuake, 0);
        yield return new WaitForSeconds(0.05f);

        transform.Rotate(0, 0, -rotationScale);
        //spriteRenderer.color = new Color(255, 0, 0, 255);
        GameObject.FindGameObjectWithTag("BattleScreen").transform.position += new Vector3(0, screenQuake, 0);
        yield return new WaitForSeconds(0.05f);

        GameObject.FindGameObjectWithTag("BattleScreen").transform.position += new Vector3(0, -screenQuake, 0);
        yield return new WaitForSeconds(0.05f);

        transform.Rotate(0, 0, -rotationScale);
        //spriteRenderer.color = new Color(255, 255, 255, 255);
        GameObject.FindGameObjectWithTag("BattleScreen").transform.position += new Vector3(0, screenQuake, 0);
        yield return new WaitForSeconds(0.05f);

        transform.Rotate(0, 0, rotationScale);

        ANI = false;
    }

    IEnumerator IceCoroutine()
    {
        freezeMonster = true;

        GameObject.FindGameObjectWithTag("BattleScreen").transform.position += new Vector3(0, -screenQuake, 0);
        yield return new WaitForSeconds(0.05f);

        GameObject.FindGameObjectWithTag("BattleScreen").transform.position += new Vector3(0, screenQuake, 0);
        yield return new WaitForSeconds(0.05f);

        GameObject.FindGameObjectWithTag("BattleScreen").transform.position += new Vector3(0, -screenQuake, 0);
        yield return new WaitForSeconds(0.05f);

        GameObject.FindGameObjectWithTag("BattleScreen").transform.position += new Vector3(0, screenQuake, 0);
        yield return new WaitForSeconds(1.35f);

        freezeMonster = false;
    }

    IEnumerator SpeedControl()
    {
        for(int i = 0; i < 30; i++)
        {
            speed = monsterAction[GameController.monsterIndex, i];
            if (speed == 0 && Random.Range(0, 10) < (skillRateAmount))
                monsterSkill = true;
            yield return new WaitForSeconds(1f);
            monsterSkill = false;
        }
    }
}
