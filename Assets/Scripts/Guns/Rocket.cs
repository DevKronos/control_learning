using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float explsRadius = 5f;
    [SerializeField] private float force;
    [SerializeField] private float damage;

    private bool isFired = false;
    public bool IsFired { set => isFired = value; }

    private GameObject explsEffect;
    public GameObject ExplsEffect { set => explsEffect = value; }

    private void OnCollisionEnter(Collision collision)
    {
        if (isFired) Explode();
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (isFired) Explode();
    }*/

    public void Explode()
    {
        GameObject explossion = Instantiate(explsEffect, transform.position, transform.rotation);
        Destroy(explossion, 0.5f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explsRadius);
        foreach (Collider strucked in colliders)
        {
            Rigidbody rbody = strucked.GetComponent<Rigidbody>();
            if (rbody != null)
            {
                rbody.AddExplosionForce(force, transform.position, explsRadius);
            }

            IDamagable damagable = strucked.GetComponent<IDamagable>();
            PlayerManager player = strucked.GetComponent<PlayerManager>();
            if (damagable != null && player == null)
            {
                damagable.TakeDamage(damage);
            }
        }
        Destroy(this.gameObject);
    }
}
