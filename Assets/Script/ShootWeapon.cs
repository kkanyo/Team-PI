using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShootWeapon : MonoBehaviour
{

    public Object weaponEffectPrefab;
    public Object weaponSkillFireEffectPrefab;
    public Object weaponSkillIceEffectPrefab;

    [Tooltip("Horizontal speed, in units/sec")]
    public float speed = 1000f;

    [Tooltip("How high the arc should be, in units")]
    public float arcHeight = 300f;

    Vector3 startPos;
    Vector3 originalTargetPos;
    Vector3 targetPos;
    Vector2 randWithinCircle;
    float colliderRandius;

    Object EffectPrefab;

    private Sprite[] allDayWeapon;
    private Sprite IceSprite;

    void Start()
    {
        // Cache our start position, which is really the only thing we need
        // (in addition to our current position, and the target).
        startPos = transform.position;

        originalTargetPos = GameObject.FindGameObjectWithTag("MTargetPoint").transform.position;
        targetPos = originalTargetPos;

        colliderRandius = GameObject.FindGameObjectWithTag("MTargetPoint").GetComponent<CircleCollider2D>().radius;

        randWithinCircle = Random.insideUnitCircle * colliderRandius;
        targetPos.x += randWithinCircle.x;
        targetPos.y += randWithinCircle.y;

        allDayWeapon = Resources.LoadAll<Sprite>("UI/DayWeapon");
        IceSprite = (Sprite)allDayWeapon.Single(s => s.name == "IceE");
    }

    void Update()
    {

        // Compute the next position, with arc added in
        float x0 = startPos.x;
        float x1 = targetPos.x;
        float dist = x1 - x0;
        float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
        float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - x0) / dist);
        float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
        Vector3 nextPos = new Vector3(nextX, baseY + arc, transform.position.z);

        // Rotate to face the next position, and then move there
        transform.rotation = LookAt2D(nextPos - transform.position);
        transform.position = nextPos;

        // Do something when we reach the target
        if (nextPos == targetPos) Arrived(targetPos);

    }

    void Arrived(Vector3 destination)
    {
        Destroy(gameObject);

        if (GameController.SKILL == 0)
        {
            EffectPrefab = weaponEffectPrefab;

            randWithinCircle = Random.insideUnitCircle * 35;
            destination.x += randWithinCircle.x;
            destination.y += randWithinCircle.y;

            GameObject weaponEffect = (GameObject)Instantiate(
               EffectPrefab,
               destination,
               Quaternion.identity
            );
            SoundManager.instance.PlaySoundWeapon();
        }
        else if (GameController.SKILL == 1)
        {
            EffectPrefab = weaponSkillFireEffectPrefab;

            GameObject weaponEffect = (GameObject)Instantiate(
                EffectPrefab
                );
            weaponEffect.transform.parent = GameObject.FindGameObjectWithTag("Monster").transform;
            weaponEffect.transform.localPosition = new Vector3(40, 10, 0);
            SoundManager.instance.PlaySoundFire();
        }
        else
        {
            EffectPrefab = weaponSkillIceEffectPrefab;
            
            GameObject weaponEffect = (GameObject)Instantiate(
                EffectPrefab
                );

            weaponEffect.GetComponent<SpriteRenderer>().sprite = IceSprite;
            weaponEffect.transform.parent = GameObject.FindGameObjectWithTag("Monster").transform;
            weaponEffect.transform.localPosition = new Vector3(0, -60, 0);
            weaponEffect.transform.localScale = new Vector3(5, 5, 0);
            SoundManager.instance.PlaySoundIce();
        }
    }

    /// 
    /// This is a 2D version of Quaternion.LookAt; it returns a quaternion
    /// that makes the local +X axis point in the given forward direction.
    /// 
    /// forward direction
    /// Quaternion that rotates +X to align with forward
    static Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }

}
