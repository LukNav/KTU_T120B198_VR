using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class WeaponScript : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    protected float shootingForce;
    [SerializeField]
    protected Transform bulletSpawn;
    [SerializeField]
    private float recoilForce;
    [SerializeField]
    private float damage;

    private Rigidbody rigidBody;
    private XRGrabInteractable interactableWeapon;

    private void Awake()
    {
        interactableWeapon = GetComponent<XRGrabInteractable>();
        rigidBody = GetComponent<Rigidbody>();
        SetupInteractableWeaponEvents();
    }

    void SetupInteractableWeaponEvents()
    {
        interactableWeapon.onActivate.AddListener(Shoot);
    }

    private void Shoot(XRBaseInteractor interactor)
    {
        GameObject projectileInstance = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        projectileInstance.GetComponent<Rigidbody>().velocity = transform.forward * shootingForce;
    }
}
