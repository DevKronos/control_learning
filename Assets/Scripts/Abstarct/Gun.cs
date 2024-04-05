using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public abstract float Damage { get; }
    public abstract float ImpactForce { get; }
    public abstract int CurrentAmmo { get; set; }
    /*public float damage = 10f;
    public float range = 100f;
    public float firerate = 15f;
    public float impactForce;
    public bool isAutomatic = false;
    public int maxAmmo = 10;
    public int currentAmmo;*/
    

    public Camera fpsCam;
    public Transform muzzle;
    public GameObject muzzleFlash;
    public GameObject impactEffect;

    public float range = 100f;
    public float firerate = 15f;
    private bool isReloading = false;
    private bool isAiming = false;
    public bool isAutomatic = false;
    public int maxAmmo = 15;
    public float reloadTime = 1f;
    private float nextTimeToFire = 0f;

    public Animator animator;
    public AmountAmmo amountAmmo;

    void Start()
    {
        CurrentAmmo = maxAmmo;
        amountAmmo.SetMaxAmmo(maxAmmo);
        amountAmmo.SetAmount(CurrentAmmo);
    }

    void OnEnable()
    {
        isReloading = false;
        isAiming = true;
        SetAimMode();
        animator.SetBool("Reloading", false);
        amountAmmo.SetMaxAmmo(CurrentAmmo);
    }

    public virtual void SetAimMode()
    {
        if (isAiming)
        {
            animator.SetBool("Aiming", false);
            fpsCam.fieldOfView = 60;
        }
        else
        {
            animator.SetBool("Aiming", true);
            fpsCam.fieldOfView = 30;
        }
        isAiming = !isAiming;
    }

    public virtual void TryReload()
    {
        if(CurrentAmmo <= maxAmmo)
        {
            StartCoroutine(Reload());
        }
    }

    protected IEnumerator Reload()
    {
        isReloading = true;
        if (isAiming)
        {
            SetAimMode();
        }

        animator.SetBool("Reloading", true);
        amountAmmo.SetReload();

        yield return new WaitForSeconds(reloadTime - .25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(.25f);

        CurrentAmmo = maxAmmo;
        amountAmmo.SetAmount(CurrentAmmo);
        isReloading = false;
    }

    public virtual void TryShoot()
    {
        if (Time.time >= nextTimeToFire && !isAutomatic && !isReloading && Input.GetMouseButtonDown(0))
        {
            nextTimeToFire = Time.time + 1f / firerate;
            Shoot();
        }

        if (Time.time >= nextTimeToFire && isAutomatic && !isReloading)
        {
            nextTimeToFire = Time.time + 1f / firerate;
            Shoot();
        }
    }
    protected virtual void Shoot()
    {
        GameObject muzzleGo = Instantiate(muzzleFlash, muzzle.position, muzzle.rotation);
        Destroy(muzzleGo, 0.6f);

        CurrentAmmo--;
        if (CurrentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
        else
        {
            amountAmmo.SetAmount(CurrentAmmo);
        }
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            IDamagable damagable = hit.transform.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(Damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * ImpactForce);
            }

            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 1f);
        }
    }
}
