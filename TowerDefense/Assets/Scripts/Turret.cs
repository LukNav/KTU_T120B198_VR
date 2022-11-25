using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class Turret : MonoBehaviour
{
    [SerializeField]
    private Transform bulletSpawn;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    [Min(1)]
    private int maxDistance;

    [SerializeField]
    [Min(1)]
    private int fireRate;

    [SerializeField]
    [Min(1)]
    private int shootingForce;

    [SerializeField]
    [Min(1)]
    private int damage;

    Transform currentEnemy;
    float lastEnemyUpdate = 0;
    float currentTime = 0;

    float lastShootTime = 0;

    XRGrabInteractable interactableTurret;
    bool canShoot = false;

    void Awake()
    {
        interactableTurret = GetComponent<XRGrabInteractable>();
        maxDistance = maxDistance ^ 2;
        SetupInteractableWeaponEvents();
    }

    private void SetupInteractableWeaponEvents()
    {
        interactableTurret.onSelectEntered.AddListener(SetAbleToShoot);
        interactableTurret.onSelectExited.AddListener(SetUnableToShoot);
    }

    private void SetAbleToShoot(XRBaseInteractor interactor)
    {
        canShoot = true;
    }

    private void SetUnableToShoot(XRBaseInteractor interactor)
    {
        canShoot = false;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > lastEnemyUpdate + 0.1)
        {
            UpdateEnemy();
            lastEnemyUpdate = currentTime;
        }
        if(currentEnemy != null)
        {
            //Debug.Log("yes enemy yes shoot");
            Shoot();
        }
    }

    private void UpdateEnemy()
    {
        //Debug.Log("updating enemy");
        Transform closestEnemy = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Vector3 directionToTarget = enemy.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < maxDistance && dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestEnemy = enemy.transform;
            }
        }
        if(closestEnemy != null)
        {
            currentEnemy = closestEnemy;
        }
        else
        {
            currentEnemy = null;
        }
    }

    private void Shoot()
    {
        if(currentEnemy == null)
        {
            return;
        }
        gameObject.transform.LookAt(currentEnemy);
        if ((currentTime - lastShootTime) > (float)(1 / fireRate))
        {
            GameObject projectileInstance = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            projectileInstance.GetComponent<Bullet>().Damage = this.damage;
            projectileInstance.GetComponent<Rigidbody>().velocity = transform.forward * shootingForce;

            lastShootTime = currentTime;
        }
    }
}
