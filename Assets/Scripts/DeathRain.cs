using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRain : MonoBehaviour
{
    private bool isCasting;
    public Vector3 stopCastingSize;

    public Transform castPoint;
    public GameObject rainOrigin;
    public Transform hitZone;

    private void OnEnable()
    {
        isCasting = true;
        hitZone.localScale = new Vector3(40, 40, 1);
    }

    void Update()
    {
        if (isCasting)
        {
            transform.localScale += new Vector3(1, 1, 0);
            if (hitZone.localScale == stopCastingSize)
            {
                isCasting = false;
                Rain();
            }
        }
    }

    public void Rain()
    {
        GameObject rain = Instantiate(rainOrigin, this.transform.position, this.transform.rotation);
        Destroy(rain, 10f);

        Collider[] colliders = Physics.OverlapCapsule(castPoint.GetChild(0).position, castPoint.GetChild(1).position, 2f);
        foreach (Collider strucked in colliders)
        {
            PlayerManager player = strucked.GetComponent<PlayerManager>();
            if (player != null)
            {
                player.TakeDamage(15f);
            }
        }
    }
}
