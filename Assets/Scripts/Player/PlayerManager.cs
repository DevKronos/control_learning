using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour , IDamagable
{
    public static PlayerManager instance;

    void Awake()
    {
        instance = this;
    }

    public float health = 100f;

    public GameObject player;
    public HealthBar healthbar;
    public Inventory inventory;
    public Camera playerCam;
    public AmountAmmo playerAmmo;

    public void Start()
    {
        healthbar.SetMaxHealth(health);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthbar.SetHealth(health);

        if (health <= 0f)
        {
            KillPlayer();
        }
    }

    void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void FindKey(Key key)
    {
        inventory.AddItem(key);
    }

    public bool HasKey(string color)
    {
        return inventory.HasItem(color);
    }

    public void LoseKey(string color)
    {
        inventory.RemoveItem(color);
    }

    public void Health(float amount)
    {
        health += amount;
        if (health > 100) health = 100;
        healthbar.SetHealth(health);
    }
}
