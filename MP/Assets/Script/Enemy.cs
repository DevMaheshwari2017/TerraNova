using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy properties")]
    [SerializeField]
    private float enemyhealth = 100;
    [SerializeField]
    private int MoneyGain = 15;
    [SerializeField]
    private GameObject DeathEffect;

    //private Bullets bullet;

   [Header("Bleeding Effects")]
    [SerializeField]
    private int bleedingovertime;
    [SerializeField]
    private GameObject bleedinglighteffect;
    [SerializeField]
    private bool isbleeding = false;
    public GameObject arrowThatHitMe;

    private void Start()
    {       
        bleedinglighteffect.SetActive(false);       
    }
    public void TakeDamage(float dmg)
    {
        enemyhealth -= dmg;
        if (enemyhealth <= 0)
        {
            Debug.Log("Enemy is dead " + enemyhealth);
            EnemyDead();
        }
    }
    private void Bleeding()
    {
        if (arrowThatHitMe != null)
        {
            Debug.Log("Arrow component has been found");
            Bullets bullet = arrowThatHitMe.GetComponent<Bullets>();
            if (bullet != null)
            {
                isbleeding = true;
                Debug.Log("Enemy bleeding effect is active");
                bleedinglighteffect.SetActive(true);
            }
        }
    }

    private void DamageOverTime()
    {
        if (arrowThatHitMe != null)
        {
            TakeDamage(bleedingovertime * Time.deltaTime);
            Debug.Log("Enemy health is depleting over time " + bleedingovertime);
            Debug.Log("EnemyHeal is " + enemyhealth);
        }
    }


    private void EnemyDead()
    {
        PlayerStats.Money += MoneyGain;
        GameObject Effects = Instantiate(DeathEffect, transform.position, Quaternion.identity);
        Destroy(Effects, 4f);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (!isbleeding)
        {
            Debug.Log("Bleeding is on");
            Bleeding();
        }
        if (isbleeding)
        {
            DamageOverTime();
        }
    }
}
