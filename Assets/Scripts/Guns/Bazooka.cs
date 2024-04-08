using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : Gun
{
    private float damage = 50f;
    private float impactForce = 50f;
    private int currentAmmo;

    public override float Damage => damage;

    public override float ImpactForce => impactForce;

    public override int CurrentAmmo { get => currentAmmo; set => currentAmmo = value; }

    public Rocket rocket;
    public Vector3 force;

    protected override void Shoot()
    {
        Rocket rocketGo = Instantiate(rocket, muzzle.position, muzzle.rotation);
        //rocket.gameObject.SetActive(false);
        rocketGo.GetComponent<Rigidbody>().AddForce(force);
        print("Its flying");

        CurrentAmmo--;
        StartCoroutine(Reload());
    }

}
