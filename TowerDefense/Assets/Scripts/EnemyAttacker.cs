using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAttacker : MonoBehaviour
{
    [SerializeField]
    private Transform bulletSpawn;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private AudioClip bulletAudio;

    [SerializeField]
    [Min(5)]
    private float enemyHealth;

    [SerializeField]
    [Min(1)]
    private int shootingForce;

    [SerializeField]
    [Min(1)]
    private float damage;

    [SerializeField]
    [Min(1)]
    private float fireRate;

    [SerializeField]
    [Min(1)]
    private int minDistance;

    int shootingDistance;

    Rigidbody rb;
    NavMeshAgent agent;
    GameObject player;
    float lastTime = 0;
    float currentTime = 0;

    private void Awake()
    {
        minDistance = (int)MathF.Pow(minDistance, 2);
        shootingDistance = minDistance + (minDistance / 2);
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        //Debug.Log("move forward");
        var currentPosition = transform.position;
        Vector3 directionToTarget = player.transform.position - currentPosition;
        float dSqrToTarget = directionToTarget.sqrMagnitude;
        agent.SetDestination(player.transform.position);
        if (dSqrToTarget > minDistance)
        {
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
        }
        Shoot((int)dSqrToTarget);
    }

    private void Shoot(int dSqrToTarget)
    {
        Debug.Log("shoot method");
        if (dSqrToTarget > shootingDistance)
        {
            Debug.Log("TOO FAR: Current dist " + dSqrToTarget + "; minDist " + shootingDistance);
            return;
        }
        Debug.Log("proceed shoot");
        if ((currentTime - lastTime) > (float)(1 / fireRate))
        {
            Debug.Log("good with firerate");
            GameObject projectileInstance = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            projectileInstance.GetComponent<Bullet>().Damage = this.damage;
            projectileInstance.GetComponent<Rigidbody>().velocity = transform.forward * shootingForce;
            //shooting sound
            AudioSource.PlayClipAtPoint(bulletAudio, bulletSpawn.position);

            lastTime = currentTime;
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
