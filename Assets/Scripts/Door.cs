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
        foreach(Lock locky in locks)
        {
            if (PlayerManager.instance.HasKey(locky.Color))
            {
                PlayerManager.instance.LoseKey(locky.Color);
                locks.Remove(locky);
                Destroy(locky.gameObject);
            }
            else
            {
                colors += locky.Color + " ";
            }
        }
        print("Here");
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
