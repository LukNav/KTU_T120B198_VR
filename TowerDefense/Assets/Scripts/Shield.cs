using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    [Min(1)]
    private float health;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            health -= collision.gameObject.GetComponent<Bullet>().Damage;
            if(health <= 0)
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
