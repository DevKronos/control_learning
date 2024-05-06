using System;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private List<Lock> locks;
    private bool isPressed = false;
    private void OnTriggerStay(Collider other)
    {
        PlayerMovement player = other.transform.GetComponent<PlayerMovement>();
        if (player == null) return;
        if (!isPressed) MessageWindow.instance.SendMessage("Press F to open door");
        if (Input.GetKey(KeyCode.F))
        {
            CheckKeys();
        }
    }
    
    public void CheckKeys()
    {
        string colors = "";
        for(int i = 0; i < locks.Count; i++)
        {
            if (PlayerManager.instance.HasKey(locks[i].Color))
            {
                PlayerManager.instance.LoseKey(locks[i].Color);
                Destroy(locks[i].gameObject);
                locks.Remove(locks[i]);
            }
            else
            {
                colors += locks[i].Color + " ";
            }
        }
        if (colors != "")
        {
            MessageWindow.instance.SendMessage($"You don't have {colors} key :(");
            isPressed = true;
            MessageWindow.instance.emptyHandler = OpenSendMessage;
        }
        if (locks.Count == 0) OpenDoor();
    }

    public void OpenDoor()
    {
        Destroy(this.gameObject);
    }

    public void OpenSendMessage()
    {
        isPressed = false;
    }
}
