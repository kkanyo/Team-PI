using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMonsterLAnimatorController : MonoBehaviour
{

    Animator animator;
    private int increasedMonsterIndex;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
        increasedMonsterIndex = GameController.monsterIndex + 1;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Outrage.freezeMonster == true)
        {
            animator.enabled = false;
        }
        else
        {
            animator.enabled = true;
            if (Outrage.monsterSkill == true)
                animator.runtimeAnimatorController = Resources.Load("Monster/Making/Mo" + increasedMonsterIndex + "L_6") as RuntimeAnimatorController;
            else if (Outrage.speed == 0)
                animator.runtimeAnimatorController = Resources.Load("Monster/Making/Mo" + increasedMonsterIndex + "L_0") as RuntimeAnimatorController;
            else
                animator.runtimeAnimatorController = Resources.Load("Monster/Making/Mo" + increasedMonsterIndex + "L_2") as RuntimeAnimatorController;
        }
    }
}
