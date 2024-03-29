using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float delay = 3f;
    public float explsRadius = 5f;
    private float force = 500f;
    private float countdown;

    public GameObject explossionEffect;

    void Start()
    {
        countdown = delay;
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0)
        {
            Explode();
        }
    }

    private void Explode()
    {
        GameObject explossion = Instantiate(explossionEffect, transform.position, transform.rotation);
        Destroy(explossion, 2f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explsRadius);
        foreach(Collider strucked in colliders)
        {
            Rigidbody rbody = strucked.GetComponent<Rigidbody>();
            if(rbody != null)
            {
                rbody.AddExplosionForce(force, transform.position, explsRadius);
            }

            IDamagable damagable = strucked.GetComponent<IDamagable>();
            if(damagable != null)
            {
                damagable.TakeDamage(15f);
            }
        }
        Destroy(gameObject);
    }
}
