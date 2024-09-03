using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Movement Player = collision.gameObject.GetComponent<Movement>();
        if (Player != null)
        {
            Player.HealthDamage();
        }
    }
}
