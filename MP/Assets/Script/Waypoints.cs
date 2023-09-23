using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] Waypoint;
    void Awake()
    {
        Waypoint = new Transform[transform.childCount];
        for (int i = 0; i  < Waypoint.Length; i++)
        {
            Waypoint[i] = transform.GetChild(i);
        }
    }
}
