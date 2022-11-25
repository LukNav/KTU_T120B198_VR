using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    [Min(5)]
    private float enemyHealth;

    [SerializeField]
    [Min(5)]
    private int moveSpeed;

    [SerializeField]
    GameObject[] waypoints;

    private int currentWaypoint = 0;
    float lastTime = 0;
    Rigidbody rb;
    NavMeshAgent agent;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Debug.Log("move forward");
        lastTime += Time.deltaTime;
        if (waypoints.Length > 0)
        {
            //Vector3 pos = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, moveSpeed * Time.deltaTime);
            //rb.MovePosition(pos);
            agent.SetDestination(waypoints[currentWaypoint].transform.position);

            if (lastTime < 1)
            {
                return;
            }
            if (Math.Abs(gameObject.transform.position.x - waypoints[currentWaypoint].transform.position.x) < 3 && Math.Abs(gameObject.transform.position.z - waypoints[currentWaypoint].transform.position.z) < 3)
            {
                //Debug.Log($"Current waypoint is {currentWaypoint}");
                currentWaypoint++;
                currentWaypoint = currentWaypoint == waypoints.Length ? currentWaypoint - 1 : currentWaypoint;
                lastTime = 0;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            enemyHealth -= collision.gameObject.GetComponent<Bullet>().Damage;
            if(enemyHealth <= 0)
            {
                Death();
            }
        }
    }
    void Death()
    {
        Destroy(gameObject);
    }
}
