using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour, IDamagable
{
    private float force = 200f;
    private float explsRadius = 15f;
    private bool isExplsosed = false;
    public GameObject explsEffect;

    public void TakeDamage(float amount)
    {
        if (isExplsosed)
        {
            return;
        }
        isExplsosed = true;
        GameObject explossion = Instantiate(explsEffect, transform.position, transform.rotation);
        Destroy(explossion, 1f);

       
        Collider[] colliders = Physics.OverlapSphere(transform.position, explsRadius);
        foreach (Collider strucked in colliders)
        {
            //Rigidbody rbody = strucked.GetComponent<Rigidbody>();
            //if (rbody != null)
            //{
            //    rbody.AddExplosionForce(force, transform.position, explsRadius);
            //}

            IDamagable damagable = strucked.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(75f);
            }
        }
        Destroy(gameObject);
    }
}
