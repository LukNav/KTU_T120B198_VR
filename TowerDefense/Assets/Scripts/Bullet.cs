using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2);
    }
    public virtual void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
