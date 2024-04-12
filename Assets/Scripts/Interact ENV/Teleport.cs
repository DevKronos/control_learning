using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform placeToTeleport;

    private void OnTriggerEnter(Collider other)
    {
        ITeleportable player = other.transform.GetComponent<ITeleportable>();
        if (player == null) return;
        if (player.TeleportState) player.Teleport(placeToTeleport.position + new Vector3(0, 0.3f, 0));
        else player.TeleportState = true;
    }
}
