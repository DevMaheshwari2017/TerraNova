using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float Speed = 10.0f;
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
            Destroy(gameObject);
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
}
