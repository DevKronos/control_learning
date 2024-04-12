using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item: MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player == null) return;
        TakeAction();
    }

    private void Update()
    {
        Vector3 rotateAngel = new Vector3(0, 0.1f, 0);
        this.transform.Rotate(rotateAngel);
    }
    public abstract void TakeAction();
}
