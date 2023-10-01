using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float Speed = 10.0f;
    private Transform target;
    private int waypointindex = 0;
    [SerializeField]
    private int enemyhealth = 100;
    [SerializeField]
    private int MoneyGain = 15;
    [SerializeField]
    private GameObject DeathEffect;
    private void Start()
    {
        target = Waypoints.Waypoint[0];       
    }

    private void ToNextWaypoint()
    {
        if(waypointindex >= Waypoints.Waypoint.Length - 1)
        {
            EnemyHitCastle();
            return;
        }
        waypointindex++;
        target = Waypoints.Waypoint[waypointindex];
    }
    private void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * Speed * Time.deltaTime,Space.World);
         if(Vector3.Distance(transform.position, target.position) <= 0.2f){
            ToNextWaypoint();
        }

    }
    public void TakeDamage(int dmg)
    {
        enemyhealth -= dmg;
        Debug.Log("Enemy have taken an dmg of " + dmg);
        Debug.Log("Enemy remaning health is " + enemyhealth);
        if(enemyhealth <= 0)
        {
            Debug.Log("Enemy is dead " + enemyhealth);
            EnemyDead();
        }
    }

    private void EnemyDead()
    {
        PlayerStats.Money += MoneyGain;
        GameObject Effects = Instantiate(DeathEffect, transform.position, Quaternion.identity);
        Destroy(Effects, 4f);        
        Destroy(gameObject);
    }
    private void EnemyHitCastle()
    {
        PlayerStats.lives--;
        Destroy(gameObject);
    }
}
