using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed = 10.0f;
    private Transform target;
    private int waypointindex = 0;

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
         if(Vector3.Distance(transform.position, target.position) <= 0.2f)
         {
            ToNextWaypoint();
         }

    }
    private void EnemyHitCastle()
    {
        PlayerStats.lives--;
        Destroy(gameObject);
    }
}
