using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRain : MonoBehaviour
{
    private bool isCasting = false;
    public Vector3 stopCastingSize;

    public Transform castPoint;
    public GameObject rainOrigin;
    public Transform hitZone;

    private GameObject rain;

    private void OnEnable()
    {
        isCasting = true;
        hitZone.localScale = new Vector3(40, 40, 1);
    }

    void Update()
    {
        if (isCasting)
        {
            hitZone.localScale += new Vector3(1, 1, 0);
            if (hitZone.localScale == stopCastingSize)
            {
                isCasting = false;
                CastRain();
            }
        }
        else
        {
            Damaging();
        }
    }

    public void CastRain()
    {
        rain = Instantiate(rainOrigin, this.transform.position, this.transform.rotation, this.transform);
    }

    public void Damaging()
    {
        Collider[] colliders = Physics.OverlapCapsule(castPoint.GetChild(0).position, castPoint.GetChild(1).position, 7f);
        foreach (Collider strucked in colliders)
        {
            PlayerManager player = strucked.GetComponent<PlayerManager>();
            if (player != null)
            {
                player.TakeDamage(0.02f);
            }
        }
    }

    public void DestroyRain()
    {
        Destroy(rain);
    }
}
