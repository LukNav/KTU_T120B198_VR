using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform bulletSpawn;

    [SerializeField]
    private TextMeshProUGUI bulletUI;

    [SerializeField]
    [Min(10)]
    protected float shootingForce;

    [SerializeField]
    private float recoilForce;

    [SerializeField]
    [Min(1)]
    private float damage;

    [SerializeField]
    private bool automatic;

    [SerializeField]
    [Min(1)]
    private int fireRate;

    [SerializeField]
    [Min(1)]
    private int bullets;

    bool isShooting = false;
    float currentTime = 0;
    float lastShootTime = 0;

    private Rigidbody rigidBody;
    private XRGrabInteractable interactableWeapon;

    private void Awake()
    {
        interactableWeapon = GetComponent<XRGrabInteractable>();
        rigidBody = GetComponent<Rigidbody>();
        SetupInteractableWeaponEvents();
        UpdateBulletsUI();
    }

    private void SetupInteractableWeaponEvents()
    {
        interactableWeapon.onActivate.AddListener(StartShooting);
        interactableWeapon.onDeactivate.AddListener(StopShooting);
    }

    private void UpdateBulletsUI()
    {
        if(bulletUI != null)
        {
            bulletUI.text = bullets.ToString();
        }
    }

    private void StartShooting(XRBaseInteractor interactor)
    {
        if(bullets > 0)
        {
            isShooting = true;

        }
        else
        {
            //empty magazine sound
        }
    }
    private void StopShooting(XRBaseInteractor interactor)
    {
        isShooting = false;
    }
    private void Shoot()
    {
        //shoot sound

        //recoil
        rigidBody.AddRelativeForce(Vector3.back * recoilForce, ForceMode.Impulse);

        //bullet
        GameObject projectileInstance = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        projectileInstance.GetComponent<Bullet>().Damage = this.damage;
        projectileInstance.GetComponent<Rigidbody>().velocity = transform.forward * shootingForce;
    }
    private void Update()
    {
        currentTime += Time.deltaTime;
        if (isShooting)
        {
            if (automatic)
            {
                if ((currentTime - lastShootTime) > (float)(1 / fireRate))
                {
                    Shoot();
                    lastShootTime = currentTime;
                    bullets--;
                    UpdateBulletsUI();
                    if (bullets <= 0)
                    {
                        isShooting = false;
                    }
                }
            }
            else
            {
                Shoot();
                lastShootTime = currentTime;
                isShooting = false;
                bullets--;
                UpdateBulletsUI();
            }
            
        }
    }
}
