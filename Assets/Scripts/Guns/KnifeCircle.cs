using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCircle : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        PlayerManager player = other.GetComponent<PlayerManager>();
        if (player == null) return;
        player.TakeDamage(damage);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotateAngel = new Vector3(0, 0.3f, 0);
        this.transform.Rotate(rotateAngel);
    }
}
