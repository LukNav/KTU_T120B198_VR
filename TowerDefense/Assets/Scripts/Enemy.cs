using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    [Min(5)]
    int enemyHealth;
    [SerializeField]
    [Min(5)]
    int moveSpeed;
    Rigidbody rb;
    [SerializeField]
    GameObject[] waypoints;
    int currentWaypoint = 0;
    float lastTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("move forward");
        lastTime += Time.deltaTime;
        if (waypoints.Length > 0)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, moveSpeed * Time.deltaTime);
            rb.MovePosition(pos);

            if (lastTime < 1)
            {
                return;
            }
            if (Math.Abs(gameObject.transform.position.x - waypoints[currentWaypoint].transform.position.x) < 3 && Math.Abs(gameObject.transform.position.z - waypoints[currentWaypoint].transform.position.z) < 3)
            {
                Debug.Log($"Current waypoint is {currentWaypoint}");
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
            enemyHealth--;
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
