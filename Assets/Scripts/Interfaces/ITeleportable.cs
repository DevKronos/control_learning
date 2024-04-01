using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITeleportable
{
    public bool TeleportState { get; set; }

    public void Teleport(Vector3 teleportPoint);
}
