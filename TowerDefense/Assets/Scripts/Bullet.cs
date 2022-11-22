using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    [Min(1)]
    public float Damage;

    [SerializeField]
    private GameObject particles;
    private void Start()
    {
        Destroy(gameObject, 2);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        //particles of hitting
        ApplyParticles();
    }
    private IEnumerator ApplyParticles()
    {
        if (particles != null)
        {
            particles.SetActive(true);
            yield return new WaitForSeconds(5f);
            particles.SetActive(false);
        }
    }
}
